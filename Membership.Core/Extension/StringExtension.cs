namespace Membership.Core.Extension
{
    public static class StringExtension
    {
        /// <summary>
        ///     İlk karakteri büyük harf yapar
        /// </summary>
        public static string UppercaseFirstChar(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            return value.Length > 1
                ? char.ToUpper(value[0]) + value.Substring(1).ToLower()
                : char.ToUpper(value[0]).ToString();
        }

        /// <summary>
        ///     String içindeki her kelimenin ilk karakterini büyük harf yapar
        /// </summary>
        public static string UppercaseFirstCharWords(this string value)
        {
            var array = value.ToLower().ToCharArray();

            if (array.Length >= 1)
                if (char.IsLower(array[0]))
                    array[0] = char.ToUpper(array[0]);

            for (var i = 1; i < array.Length; i++)
            {
                if (array[i - 1] != ' ') continue;
                if (char.IsLower(array[i]))
                    array[i] = char.ToUpper(array[i]);
            }
            return new string(array);
        }

        public static bool IsNullOrWhitespace(this string value)
        {
            return (value == null) || (value.Trim().Length == 0);
        }     
    }
}
