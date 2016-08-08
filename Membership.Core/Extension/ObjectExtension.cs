using System;
using Newtonsoft.Json;

namespace Membership.Core.Extension
{
    public static class ObjectExtension
    {
        public static string ToJsonString(this object value)
        {
            return value == null ? string.Empty : JsonConvert.SerializeObject(value);
        }

        public static bool IsNumericAndGreaterThenZero(this object value)
        {
            try
            {
                return Convert.ToDouble(value) > 0;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsNumeric(this object value)
        {
            try
            {
                if (value == null) return false;
                var temp = Convert.ToDouble(value);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}