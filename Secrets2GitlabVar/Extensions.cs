using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secrets2GitlabVar
{
    public static class Extensions
    {
        public static T ToEnum<T>(this string value, T defaultValue)
            where T : struct, Enum
        {
            if (string.IsNullOrEmpty(value)) { return defaultValue; }
            T result;
            return Enum.TryParse<T>(value, true, out result) ? result : defaultValue;
        }
    }
}
