using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ClassLibrary.Helpers
{
    public static class AppConfigurationsHelper
    {
        private static string GetConfigValue(string key)
        {
            try
            {
                return ConfigurationManager.AppSettings[key];
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string ApplicationName { get { return GetConfigValue("ApplicationName"); } }
        public static string ApplicationIntro { get { return GetConfigValue("ApplicationIntro"); } }
        public static string Address { get { return GetConfigValue("Address"); } }
        public static string EmailAddress { get { return GetConfigValue("EmailAddress"); } }
        public static string PhoneNumber { get { return GetConfigValue("PhoneNumber"); } }
        public static string MobileNumber { get { return GetConfigValue("MobileNumber"); } }
        public static string FacebookURL { get { return GetConfigValue("FacebookURL"); } }
        public static string TwitterUsername { get { return GetConfigValue("TwitterUsername"); } }
        public static string TwitterURL { get { return GetConfigValue("TwitterURL"); } }
        public static string WhatsAppNumber { get { return GetConfigValue("WhatsAppNumber"); } }
        public static string InstagramURL { get { return GetConfigValue("InstagramURL"); } }
        public static string YouTubeURL { get { return GetConfigValue("YouTubeURL"); } }
        public static string LinkedInURL { get { return GetConfigValue("LinkedInURL"); } }
        public static string SendGridAPIKey { get { return GetConfigValue("SendGridAPIKey"); } }
        public static string FromEmailAddress { get { return GetConfigValue("FromEmailAddress"); } }
        public static string FromEmailAddressName { get { return GetConfigValue("FromEmailAddressName"); } }
    }
}
