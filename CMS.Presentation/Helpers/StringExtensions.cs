using System.Text.RegularExpressions;


namespace CMS.Presentation.Helpers
{
    public static class StringExtensions
    {
        /// <summary>
        /// Insert spaces before capital letter in the string. I.e. "HelloWorld" turns into "Hello World"
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToSeparatedWords(this string value)
        {
            if (value != null)
            {
                return Regex.Replace(value, "([A-Z][a-z]?)", " $1").Trim();
            }
            return null;
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}