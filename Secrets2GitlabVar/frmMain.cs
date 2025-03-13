using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Secrets2GitlabVar
{
    public partial class frmMain : Form
    {
        #region Variables
        private bool _loading = false;
        private List<string>? _args = null;
        #endregion

        public frmMain(List<string>? args = null)
        {
            InitializeComponent();
            _args = args;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            _loading = true;
            cmbES.SelectedIndex = 0;
            cmbChildBeh.SelectedIndex = 0;
            cmbChildSep.SelectedIndex = 0;
            cmbArrayBeh.SelectedIndex = 0;
            cmbArrayBrack.SelectedIndex = 1;

            if (_args is not null && _args.Count > 0)
            {
                string? filePath = _args.FirstOrDefault();
                if (File.Exists(filePath))
                {
                    txtIn.Text = File.ReadAllText(filePath);
                    TriggerConvert(sender, e);
                }
            }

            // Load demo vals from resources
            // LoadSettings
            _loading = false;
        }

        private string Convert(string data)
        {
            try
            {
                ConvertOptions convertOptions = new ConvertOptions()
                {
                    EqualitySymbol = (EqualitySymbol)cmbES.SelectedIndex,
                    ChildBehaviour = (ChildBehaviour)cmbChildBeh.SelectedIndex,
                    ChildSeparator = (ChildSeparator)cmbChildSep.SelectedIndex,
                    ArrayBehaviour = (ArrayBehaviour)cmbArrayBeh.SelectedIndex,
                    ArrayBrackets = (ArrayBrackets)cmbArrayBrack.SelectedIndex,
                    SpacesInEqualitySymbol = chkSpaceInES.Checked,
                    TrimProperties = chkTrimProp.Checked,
                    TrimValues = chkTrimVal.Checked
                };

                return ProcessJson(data, convertOptions);

                //JToken token = JToken.Parse(txtIn.Text);
                //IEnumerable<string> jtData = ProcessJToken(token, convertOptions);
                //return string.Join(Environment.NewLine, jtData);

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
            (string abL, string abR) = GetArrayBrackets(convertOptions.ArrayBrackets);
            char esChar = convertOptions.EqualitySymbol == EqualitySymbol.Equal ? '=' : ':';
            string fullES = convertOptions.SpacesInEqualitySymbol ? $" {esChar} " : $"{esChar}";

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
            txtIn.Text = @"{
                ""Full Name"": ""Alex Virlan"",
                ""Age"": 26,
                ""Address"": {
                    ""Street"": ""123 Main St"",
                    ""City"": ""New York""
                },
                ""Hobbies"": [""Coding"", ""Sleeping""]
            }";
            TriggerConvert(sender, e);
        }

        private void TriggerConvert(object sender, EventArgs e)
        {
            if (_loading) { return; }
            if (chkAutoConvert.Checked) { txtOut.Text = Convert(txtIn.Text); }
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
