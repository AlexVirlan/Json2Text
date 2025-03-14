using Newtonsoft.Json;
using Secrets2GitlabVar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Secrets2GitlabVar.Utilities
{
    public class Settings
    {
        public static uint Runs = 0;
        public static uint Conversions = 0;
        public static bool AutoConvert = false;
        public static bool AutoCopy = false;
        public static bool RememberInput = false;
        public static ConvertOptions ConvertOptions = new();
        public static string InputData = "";
    }

    [Serializable]
    public class AppSettings : Settings
    {
        #region Properties
        [JsonProperty("Runs")]
        public uint runs { get { return Runs; } set { Runs = value; } }

        [JsonProperty("Conversions")]
        public uint conversions { get { return Conversions; } set { Conversions = value; } }

        [JsonProperty("AutoConvert")]
        public bool autoConvert { get { return AutoConvert; } set { AutoConvert = value; } }

        [JsonProperty("AutoCopy")]
        public bool autoCopy { get { return AutoCopy; } set { AutoCopy = value; } }

        [JsonProperty("RememberInput")]
        public bool rememberInput { get { return RememberInput; } set { RememberInput = value; } }

        [JsonProperty("ConvertOptions")]
        public ConvertOptions convertOptions { get { return ConvertOptions; } set { ConvertOptions = value; } }

        [JsonProperty("InputData")]
        public string inputData { get { return InputData; } set { InputData = value; } }
        #endregion

        #region Methods
        public static FunctionResponse Save(string fileName = "App.set", bool inAppData = true)
        {
            try
            {
                (bool exists, string path) = GetSetFilePath(fileName, inAppData);

                if (!exists)
                {
                    string? folders = Path.GetDirectoryName(path);
                    Directory.CreateDirectory(folders);
                }

                string settingsData = JsonConvert.SerializeObject(new AppSettings(), Formatting.Indented);
                File.WriteAllText(path, settingsData);
                return new FunctionResponse(error: false, message: "Settings saved successfully.");
            }
            catch (Exception ex)
            {
                return new FunctionResponse(ex);
            }
        }

        public static FunctionResponse Load(string fileName = "App.set", bool fromAppData = true)
        {
            try
            {
                (bool exists, string path) = GetSetFilePath(fileName, fromAppData);
                if (!exists)
                {
                    return new FunctionResponse(error: true, message: $"The settings file ({path}) is missing.");
                }

                string settingsData = File.ReadAllText(path);
                JsonConvert.DeserializeObject<AppSettings>(settingsData,
                    new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace });
                return new FunctionResponse(error: false, message: "Settings loaded successfully.");
            }
            catch (Exception ex)
            {
                return new FunctionResponse(ex);
            }
        }

        public static (bool exists, string path) GetSetFilePath(string fileName, bool inOrFromAppData = true)
        {
            if (fileName.Trim().INOE()) { fileName = "App.set"; }
            if (inOrFromAppData)
            {
                string appName = Assembly.GetEntryAssembly()?.GetName()?.Name ?? "Secrets2GitlabVar";
                string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                    $@"\AvA.Soft\{appName}\{fileName}";
                return (File.Exists(path), path);
            }
            else
            {
                return fileName.CombineWithStartupPath();
            }
        }
        #endregion
    }
}
