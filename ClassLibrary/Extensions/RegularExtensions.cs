using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace ClassLibrary.Extensions
{
    public static class RegularExtensions
    {
        private static string illegalCharacterReplacePattern = @"[^\w]";

        public static string SanitizeString(this string str)
        {
            string sanitizedString = string.Empty;
            if (!string.IsNullOrEmpty(str))
            {
                sanitizedString = Regex.Replace(str.Trim(), illegalCharacterReplacePattern, "-");
                sanitizedString = sanitizedString.Replace("---", "-").Replace("--", "-");
                sanitizedString = sanitizedString.TrimStart('-').TrimEnd('-');
            }

            return sanitizedString;
        }

        public static string SanitizeLowerString(this string str)
        {
            return str.SanitizeString().ToLower();
        }

        private static string ToShortGuid(this Guid newGuid)
        {
            string modifiedBase64 = Convert.ToBase64String(newGuid.ToByteArray())
                .Replace('+', '-').Replace('/', '_') // avoid invalid URL characters
                .Substring(0, 22);
            return modifiedBase64;
        }

        private static Guid ParseShortGuid(string shortGuid)
        {
            string base64 = shortGuid.Replace('-', '+').Replace('_', '/') + "==";
            Byte[] bytes = Convert.FromBase64String(base64);
            return new Guid(bytes);
        }
        public static string ToSiteURL(this string pageURL)
        {
            var request = HttpContext.Current.Request;

            return string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, pageURL);
        }

        public static string ToAuthorizeNetProductName(this string productName)
        {
            if (!string.IsNullOrEmpty(productName))
            {
                if (productName.Length > 31)
                {
                    return productName.Substring(0, 30);
                }
                else return productName;
            }
            else return string.Empty;
        }
    }
}
