using DrinkDotCom.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace DrinkDotCom.Utilities.Code.Helpers
{
    public static class ConfigurationsHelper
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


        private static int? _DashboardRecordsSizePerPage { get; set; }
        public static int DashboardRecordsSizePerPage {
            get {
                if(!_DashboardRecordsSizePerPage.HasValue)
                {
                    var config = ConfigurationsService.Instance.GetConfigurationByKey("DashboardRecordsSizePerPage");

                    if (config != null)
                    {
                        _DashboardRecordsSizePerPage = int.Parse(config.Value);
                    }
                    else _DashboardRecordsSizePerPage = 10;
                }

                return _DashboardRecordsSizePerPage.Value;
            }
        }

        private static int? _FeaturedRecordsSizePerPage { get; set; }
        public static int FeaturedRecordsSizePerPage
        {
            get
            {
                if (!_FeaturedRecordsSizePerPage.HasValue)
                {
                    var config = ConfigurationsService.Instance.GetConfigurationByKey("FeaturedRecordsSizePerPage");

                    if (config != null)
                    {
                        _FeaturedRecordsSizePerPage = int.Parse(config.Value);
                    }
                    else _FeaturedRecordsSizePerPage = 8;
                }

                return _FeaturedRecordsSizePerPage.Value;
            }
        }

        private static int? _RelatedProductsRecordsSizePerPage { get; set; }
        public static int RelatedProductsRecordsSizePerPage
        {
            get
            {
                if (!_RelatedProductsRecordsSizePerPage.HasValue)
                {
                    var config = ConfigurationsService.Instance.GetConfigurationByKey("RelatedProductsRecordsSizePerPage");

                    if (config != null)
                    {
                        _RelatedProductsRecordsSizePerPage = int.Parse(config.Value);
                    }
                    else _RelatedProductsRecordsSizePerPage = 4;
                }

                return _RelatedProductsRecordsSizePerPage.Value;
            }
        }

        private static int? _FrontendRecordsSizePerPage { get; set; }
        public static int FrontendRecordsSizePerPage
        {
            get
            {
                if (!_FrontendRecordsSizePerPage.HasValue)
                {
                    var config = ConfigurationsService.Instance.GetConfigurationByKey("FrontendRecordsSizePerPage");

                    if (config != null)
                    {
                        _FrontendRecordsSizePerPage = int.Parse(config.Value);
                    }
                    else _FrontendRecordsSizePerPage = 9;
                }

                return _FrontendRecordsSizePerPage.Value;
            }
        }

        private static string _CurrencySymbol { get; set; }
        public static string CurrencySymbol
        {
            get
            {
                if (string.IsNullOrEmpty(_CurrencySymbol))
                {
                    var config = ConfigurationsService.Instance.GetConfigurationByKey("CurrencySymbol");

                    if (config != null)
                    {
                        _CurrencySymbol = config.Value;
                    }
                    else _CurrencySymbol = "£"; //Default CurrencySymbol is set to  £
                }

                return _CurrencySymbol;
            }
        }

        private static string _PriceCurrencyPosition { get; set; }
        public static string PriceCurrencyPosition
        {
            get
            {
                if (string.IsNullOrEmpty(_PriceCurrencyPosition))
                {
                    var config = ConfigurationsService.Instance.GetConfigurationByKey("PriceCurrencyPosition");

                    if (config != null)
                    {
                        _PriceCurrencyPosition = config.Value;
                    }
                    else _PriceCurrencyPosition = "{currency}{price}"; 
                }

                return _PriceCurrencyPosition;
            }
        }

        private static bool? _EnableCreditCardPayment { get; set; }
        public static bool EnableCreditCardPayment
        {
            get
            {
                if (!_EnableCreditCardPayment.HasValue)
                {
                    var config = ConfigurationsService.Instance.GetConfigurationByKey("EnableCreditCardPayment");

                    if (config != null)
                    {
                        _EnableCreditCardPayment = bool.Parse(config.Value);
                    }
                    else _EnableCreditCardPayment = true;
                }

                return _EnableCreditCardPayment.Value;
            }
        }

        private static bool? _EnableCashOnDeliveryMethod { get; set; }
        public static bool EnableCashOnDeliveryMethod
        {
            get
            {
                if (!_EnableCashOnDeliveryMethod.HasValue)
                {
                    var config = ConfigurationsService.Instance.GetConfigurationByKey("EnableCashOnDeliveryMethod");

                    if (config != null)
                    {
                        _EnableCashOnDeliveryMethod = bool.Parse(config.Value);
                    }
                    else _EnableCashOnDeliveryMethod = true;
                }

                return _EnableCashOnDeliveryMethod.Value;
            }
        }

        private static decimal? _FlatDeliveryCharges { get; set; }
        public static decimal FlatDeliveryCharges
        {
            get
            {
                if (!_FlatDeliveryCharges.HasValue)
                {
                    var config = ConfigurationsService.Instance.GetConfigurationByKey("FlatDeliveryCharges");

                    if (config != null)
                    {
                        _FlatDeliveryCharges = decimal.Parse(config.Value);
                    }
                    else _FlatDeliveryCharges = 0;
                }

                return _FlatDeliveryCharges.Value;
            }
        }
        public static decimal Discount
        {
            get
            {
                return 0;
            }
        }
    }
}