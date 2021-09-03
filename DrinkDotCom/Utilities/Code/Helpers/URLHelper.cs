using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DrinkDotCom.Utilities.Code.Helpers
{
    public static class URLHelper
    {
        public static string Home(this UrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("Home");

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string Register(this UrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("Register");

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string Login(this UrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("Login");

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string SocialLoginCallback(this UrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("SocialLoginCallback");

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string ForgotPassword(this UrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("ForgotPassword");

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string ResetPassword(this UrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("ResetPassword");

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string Logoff(this UrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("Logoff");

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string SearchProducts(this UrlHelper helper, string category = "", string q = "", decimal? from = 0.0M, decimal? to = 0.0M, string sortby = "", int? pageNo = 0, int? pageSize = 0)
        {
            string routeURL = string.Empty;

            var routeValues = new RouteValueDictionary();

            routeValues.Add("category", category);

            if (!string.IsNullOrEmpty(q))
            {
                routeValues.Add("q", q);
            }

            if (from.HasValue && from.Value > 0.0M)
            {
                routeValues.Add("from", from.Value);
            }

            if (to.HasValue && to.Value > 0.0M)
            {
                routeValues.Add("to", to.Value);
            }

            if (!string.IsNullOrEmpty(sortby))
            {
                routeValues.Add("sortby", sortby);
            }

            if (pageNo.HasValue && pageNo.Value > 1)
            {
                routeValues.Add("pageNo", pageNo.Value);
            }

            if (pageSize.HasValue && pageSize.Value > 1)
            {
                routeValues.Add("pageSize", pageSize.Value);
            }

            routeURL = helper.RouteUrl("SearchProducts", routeValues);

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string ProductDetails(this UrlHelper helper, string category, int ID)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("ProductDetails", new
            {
                category = category,
                ID = ID
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string UserProfile(this UrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("UserProfile");

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }
        public static string UpdateProfile(this UrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("UpdateProfile");

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string ChangePassword(this UrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("ChangePassword");

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }
        public static string UpdatePassword(this UrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("UpdatePassword");

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string ChangeAvatar(this UrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("ChangeAvatar");

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }
        public static string UpdateAvatar(this UrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("UpdateAvatar");

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }


        public static string UserOrders(this UrlHelper helper, string userEmail = "", int? orderID = 0, int? orderStatus = 0, int? pageNo = 0)
        {
            string routeURL = string.Empty;

            var routeValues = new RouteValueDictionary();

            if (!string.IsNullOrEmpty(userEmail))
            {
                routeValues.Add("userEmail", userEmail);
            }

            if (orderID.HasValue && orderID.Value > 0)
            {
                routeValues.Add("orderID", orderID.Value);
            }

            if (orderStatus.HasValue && orderStatus.Value > 0)
            {
                routeValues.Add("orderStatus", orderStatus.Value);
            }

            if (pageNo.HasValue && pageNo.Value > 1)
            {
                routeValues.Add("pageNo", pageNo.Value);
            }

            routeURL = helper.RouteUrl("UserOrders", routeValues);

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string CartProducts(this UrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("CartProducts");

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string CartItems(this UrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("CartItems");

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string Checkout(this UrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("Checkout");

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string PlaceOrder(this UrlHelper helper, bool isCashOnDelivery = false)
        {
            string routeURL = string.Empty;

            if(isCashOnDelivery)
            {
                routeURL = helper.RouteUrl("PlaceOrderViaCashOnDelivery");
            }
            else
            {
                routeURL = helper.RouteUrl("PlaceOrder");
            }

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string OrderTrack(this UrlHelper helper, string orderID = "", bool orderPlaced = false)
        {
            string routeURL = string.Empty;

            var routeValues = new RouteValueDictionary();
            
            if (!string.IsNullOrEmpty(orderID))
            {
                routeValues.Add("orderID", orderID);
            }

            if (orderPlaced)
            {
                routeValues.Add("orderPlaced", orderPlaced);
            }

            routeURL = helper.RouteUrl("OrderTrack", routeValues);

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string PrintInvoice(this UrlHelper helper, int orderID)
        {
            string routeURL = string.Empty;

            var routeValues = new RouteValueDictionary();

            routeValues.Add("orderID", orderID);

            routeURL = helper.RouteUrl("PrintInvoice", routeValues);

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string UploadPictures(this UrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("UploadPictures");

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }
    }
}