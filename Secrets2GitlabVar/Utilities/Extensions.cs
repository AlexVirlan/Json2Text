using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secrets2GitlabVar.Utilities
{
    public static class Extensions
    {
        #region String
        public static bool INOE(this string? str) => string.IsNullOrEmpty(str);

        public static T ToEnum<T>(this string value, T defaultValue)
            where T : struct, Enum
        {
            if (string.IsNullOrEmpty(value)) { return defaultValue; }
            T result;
            return Enum.TryParse(value, true, out result) ? result : defaultValue;
        }

        public static (bool exists, string path) CombineWithStartupPath(this string fileName)
        {
            if (fileName.INOE()) { return (File.Exists(fileName), fileName); }
            string file = Path.GetFileName(fileName);
            string fullPath = Path.Combine(Application.StartupPath, file);
            return (File.Exists(fullPath), fullPath);
        }
        #endregion
    }
}
