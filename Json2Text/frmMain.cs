using DeviceId;
using Json2Text.Entities;
using Json2Text.Utilities;
using Newtonsoft.Json.Linq;
using System.Text;

namespace Json2Text
{
    public partial class frmMain : Form
    {
        #region Variables
        private bool _loading = false;
        private List<string>? _args = null;
        private string _deviceFingerprint = "";
        #endregion

        public frmMain(List<string>? args = null)
        {
            InitializeComponent();
            _args = args;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                _loading = true;

                FunctionResponse frLoadSet = AppSettings.Load();
                Settings.Runs++;
                GetDeviceFingerprint();

                if (_args is not null && _args.Count > 0)
                {
                    string? filePath = _args.FirstOrDefault();
                    (bool readSuccess, string readResult) = Helpers.TryToReadTextFile(filePath, returnExceptionString: true);
                    Settings.InputData = readResult;
                    if (readSuccess) { TriggerConvert(sender, e); }
                }
                else
                {
                    DecodeSavedInputData();
                }

                ApplySettingsToUI();

                _loading = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message + Environment.NewLine + "The application will exit.",
                    "Json2Text - Startup error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
        }

        private void GetDeviceFingerprint()
        {
            string tokenPath = Path.Combine(Application.StartupPath, "Json2Text.token");
            _deviceFingerprint = "@" + new DeviceIdBuilder()
                .AddUserName()
                .AddMachineName()
                //.OnWindows(winDevBuilder => winDevBuilder
                //    .AddMotherboardSerialNumber()
                //    .AddSystemDriveSerialNumber())
                .AddFileToken(tokenPath)
                .UseFormatter(DeviceIdFormatters.DefaultV6)
                .ToString() + "@";
        }

        private void ApplySettingsToUI()
        {
            chkAutoConvert.Checked = Settings.AutoConvert;
            chkAutoCopy.Checked = Settings.AutoCopy;
            chkRememberInput.Checked = Settings.RememberInput;
            chkInWW.Checked = Settings.InWordWrap;
            chkOutWW.Checked = Settings.OutWordWrap;
            chkSpaceInES.Checked = Settings.ConvertOptions.SpacesInEqualitySymbol;
            chkTrimProp.Checked = Settings.ConvertOptions.TrimProperties;
            chkTrimVal.Checked = Settings.ConvertOptions.TrimValues;
            cmbES.SelectedIndex = (int)Settings.ConvertOptions.EqualitySymbol;
            cmbChildBeh.SelectedIndex = (int)Settings.ConvertOptions.ChildBehaviour;
            cmbChildSep.SelectedIndex = (int)Settings.ConvertOptions.ChildSeparator;
            cmbArrayBeh.SelectedIndex = (int)Settings.ConvertOptions.ArrayBehaviour;
            cmbArrayBrack.SelectedIndex = (int)Settings.ConvertOptions.ArrayBrackets;
            txtIn.Text = Settings.InputData;
            UpdateStats();
        }

        private void EncodeSavedInputData()
        {
            string result = string.Empty;
            try
            {
                if (Settings.RememberInput &&
                    !txtIn.Text.INOE() &&
                    !_deviceFingerprint.INOE())
                {
                    result = AesEncryption.EncryptString(txtIn.Text, _deviceFingerprint);
                }
            }
            catch (Exception) { }
            finally { Settings.InputData = result; }
        }

        private void DecodeSavedInputData()
        {
            string result = string.Empty;
            try
            {
                if (Settings.RememberInput &&
                    !Settings.InputData.INOE() &&
                    !_deviceFingerprint.INOE())
                {
                    result = AesEncryption.DecryptString(Settings.InputData, _deviceFingerprint);
                }
            }
            catch (Exception) { }
            finally { Settings.InputData = result; }
        }

        private string Convert(string data)
        {
            try
            {
                if (data.INOE()) { return string.Empty; }

                ConvertOptions convertOptions = new ConvertOptions()
                {
                    EqualitySymbol = (EqualitySymbol)cmbES.SelectedIndex,
                    ChildBehaviour = (ChildBehaviour)cmbChildBeh.SelectedIndex,
                    ChildSeparator = (ChildSeparator)cmbChildSep.SelectedIndex,
                    ArrayBehaviour = (ArrayBehaviour)cmbArrayBeh.SelectedIndex,
                    ArrayBrackets = (ArrayBrackets)cmbArrayBrack.SelectedIndex,
                    SpacesInEqualitySymbol = chkSpaceInES.Checked,
                    TrimProperties = chkTrimProp.Checked,
                    TrimValues = chkTrimVal.Checked,
                    IgnorePropertiesWithEmptyValues = chkIgnorePropWNoVal.Checked
                };

                string result = ProcessJson(data, convertOptions);
                if (chkAutoCopy.Checked && !result.INOE()) { Clipboard.SetText(result); }
                Settings.Conversions++;
                UpdateStats();

                return result;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private string ProcessJson(string jsonString, ConvertOptions convertOptions)
        {
            if (string.IsNullOrEmpty(jsonString)) { return string.Empty; }

            string cs = GetChildSeparator(convertOptions.ChildSeparator);
            string es = GetEqualitySymbol(convertOptions.EqualitySymbol);
            string fullES = convertOptions.SpacesInEqualitySymbol ? $" {es} " : $"{es}";
            (string abL, string abR) = GetArrayBrackets(convertOptions.ArrayBrackets);

            List<string> resultLines = new();
            JToken root = JToken.Parse(jsonString);
            Stack<(JToken token, string path, int depth)> stack = new();
            stack.Push((root, string.Empty, 0));

            while (stack.Count > 0)
            {
                (JToken token, string path, int depth) = stack.Pop();

                if (token is JValue value)
                {
                    string valueStr = value.ToString();
                    if (convertOptions.IgnorePropertiesWithEmptyValues && valueStr.INOE()) { continue; }
                    if (convertOptions.TrimProperties) { path = path.Replace(" ", string.Empty); }
                    if (convertOptions.TrimValues) { valueStr = valueStr.Replace(" ", string.Empty); }

                    resultLines.Add($"{path}{fullES}{valueStr}");
                }
                else if (token is JObject obj)
                {
                    if (convertOptions.ChildBehaviour == ChildBehaviour.Include || depth == 0)
                    {
                        List<JProperty> properties = obj.Properties().ToList();
                        for (int i = properties.Count - 1; i >= 0; i--)
                        {
                            JProperty property = properties[i];
                            string newPath = string.IsNullOrEmpty(path) ? property.Name : $"{path}{cs}{property.Name}";
                            stack.Push((property.Value, newPath, depth + 1));
                        }
                    }
                }
                else if (convertOptions.ArrayBehaviour == ArrayBehaviour.Include && token is JArray array)
                {
                    for (int i = array.Count - 1; i >= 0; i--)
                    {
                        string newPath = $"{path}{abL}{i}{abR}";
                        stack.Push((array[i], newPath, depth + 1));
                    }
                }
            }

            return string.Join(Environment.NewLine, resultLines);
        }

        private string GetEqualitySymbol(EqualitySymbol equalitySymbol)
        {
            switch (equalitySymbol)
            {
                case EqualitySymbol.Equal: return "=";
                case EqualitySymbol.Colon: return ":";
                case EqualitySymbol.GreaterThan: return ">";
                case EqualitySymbol.Arrow: return "->";
                default: return "=";
            }
        }

        private string GetChildSeparator(ChildSeparator childSeparator)
        {
            switch (childSeparator)
            {
                case ChildSeparator.Dot: return ".";
                case ChildSeparator.Underscore: return "_";
                case ChildSeparator.Dash: return "-";
                case ChildSeparator.None: return "";
                default: return "";
            }
        }

        private (string, string) GetArrayBrackets(ArrayBrackets arrayBrackets)
        {
            switch (arrayBrackets)
            {
                case ArrayBrackets.Round: return ("(", ")");
                case ArrayBrackets.Square: return ("[", "]");
                case ArrayBrackets.Curly: return ("{", "}");
                case ArrayBrackets.None: return ("", "");
                default: return ("", "");
            }
        }

        private void txtIn_TextChanged(object sender, EventArgs e)
        {
            if (chkAutoConvert.Checked) { txtOut.Text = Convert(txtIn.Text); }
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            txtOut.Text = Convert(txtIn.Text);
        }

        private void chkAutoConvert_CheckedChanged(object sender, EventArgs e)
        {
            btnConvert.Enabled = !chkAutoConvert.Checked;
            TriggerConvert(sender, e);
        }

        private void lblPaste_Click(object sender, EventArgs e)
        {
            txtIn.Text = Clipboard.GetText();
        }

        private void lblDemoVal_Click(object sender, EventArgs e)
        {
            byte[] dataBytes = Properties.Resources.DemoValues;
            txtIn.Text = Encoding.UTF8.GetString(dataBytes);
            TriggerConvert(sender, e);
        }

        private void TriggerConvert(object sender, EventArgs e)
        {
            if (_loading) { return; }
            if (chkAutoConvert.Checked) { txtOut.Text = Convert(txtIn.Text); }
        }

        private void lblCopy2CB_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtOut.Text);
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.AutoConvert = chkAutoConvert.Checked;
            Settings.AutoCopy = chkAutoCopy.Checked;
            Settings.RememberInput = chkRememberInput.Checked;
            Settings.InWordWrap = chkInWW.Checked;
            Settings.OutWordWrap = chkOutWW.Checked;
            Settings.ConvertOptions.SpacesInEqualitySymbol = chkSpaceInES.Checked;
            Settings.ConvertOptions.TrimProperties = chkTrimProp.Checked;
            Settings.ConvertOptions.TrimValues = chkTrimVal.Checked;
            Settings.ConvertOptions.EqualitySymbol = (EqualitySymbol)cmbES.SelectedIndex;
            Settings.ConvertOptions.ChildBehaviour = (ChildBehaviour)cmbChildBeh.SelectedIndex;
            Settings.ConvertOptions.ChildSeparator = (ChildSeparator)cmbChildSep.SelectedIndex;
            Settings.ConvertOptions.ArrayBehaviour = (ArrayBehaviour)cmbArrayBeh.SelectedIndex;
            Settings.ConvertOptions.ArrayBrackets = (ArrayBrackets)cmbArrayBrack.SelectedIndex;
            EncodeSavedInputData();
            AppSettings.Save();
        }

        private void lblResetSet_Click(object sender, EventArgs e)
        {
            Settings.AutoConvert = false;
            Settings.AutoCopy = false;
            Settings.RememberInput = false;
            Settings.InWordWrap = true;
            Settings.OutWordWrap = true;
            Settings.ConvertOptions.SpacesInEqualitySymbol = false;
            Settings.ConvertOptions.TrimProperties = true;
            Settings.ConvertOptions.TrimValues = true;
            Settings.ConvertOptions.EqualitySymbol = EqualitySymbol.Equal;
            Settings.ConvertOptions.ChildBehaviour = ChildBehaviour.Include;
            Settings.ConvertOptions.ChildSeparator = ChildSeparator.Dot;
            Settings.ConvertOptions.ArrayBehaviour = ArrayBehaviour.Include;
            Settings.ConvertOptions.ArrayBrackets = ArrayBrackets.Square;
            Settings.InputData = "";
            ApplySettingsToUI();
        }

        private void chkInWW_CheckedChanged(object sender, EventArgs e)
        {
            txtIn.WordWrap = chkInWW.Checked;
        }

        private void chkOutWW_CheckedChanged(object sender, EventArgs e)
        {
            txtOut.WordWrap = chkOutWW.Checked;
        }

        public void UpdateStats()
        {
            lblStats.Text = $"Runs: {Settings.Runs}, Conversions: {Settings.Conversions}";
        }

        [Obsolete("Replaced by the new 'ProcessJson' method that can handle more convert options.")]
        public IEnumerable<string> ProcessJToken(JToken token, ConvertOptions convertOptions, string parentPath = "")
        {
            if (token == null) { yield break; }

            if (token.Type == JTokenType.Object)
            {
                foreach (JProperty property in token.Children<JProperty>())
                {
                    string cs = GetChildSeparator(convertOptions.ChildSeparator);
                    string fullPath = string.IsNullOrEmpty(parentPath) ? property.Name : $"{parentPath}{cs}{property.Name}";
                    foreach (var line in ProcessJToken(property.Value, convertOptions, fullPath))
                    {
                        yield return line;
                    }
                }
            }
            else if (convertOptions.ArrayBehaviour == ArrayBehaviour.Include && token.Type == JTokenType.Array)
            {
                int index = 0;
                (string abL, string abR) = GetArrayBrackets(convertOptions.ArrayBrackets);

                foreach (JToken item in token.Children())
                {
                    string fullPath = $"{parentPath}{abL}{index}{abR}";
                    foreach (var line in ProcessJToken(item, convertOptions, fullPath))
                    {
                        yield return line;
                    }
                    index++;
                }
            }
            else
            {
                string tokenStr = token.ToString();
                char esChar = convertOptions.EqualitySymbol == EqualitySymbol.Equal ? '=' : ':';
                string fullES = convertOptions.SpacesInEqualitySymbol ? $" {esChar} " : $"{esChar}";

                if (convertOptions.TrimProperties) { parentPath = parentPath.Replace(" ", string.Empty); }
                if (convertOptions.TrimValues) { tokenStr = tokenStr.Replace(" ", string.Empty); }

                yield return $"{parentPath}{fullES}{tokenStr}";
            }
        }
    }
}
