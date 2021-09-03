using DrinkDotCom.Utilities.Code.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrinkDotCom.Utilities.Code.Commons
{
    public static class Extensions
    {
        public static string WithCurrency(this decimal price)
        {
            return ConfigurationsHelper.PriceCurrencyPosition
                                       .Replace("{price}", price.ToString())
                                       .Replace("{currency}", ConfigurationsHelper.CurrencySymbol);
        }


        public static string GetSubstringText(this string Str, string Start, string End)
        {
            try
            {
                var StartingIndex = !string.IsNullOrEmpty(Start) ? Str.IndexOf(Start) + 1 : 0;

                var EndingIndex = (!string.IsNullOrEmpty(End) ? Str.IndexOf(End) : Str.Length);

                var Length = EndingIndex - StartingIndex;

                return Str.Substring(StartingIndex, Length).Trim();
            }
            catch
            {
                return null;
            }
        }
    }
}