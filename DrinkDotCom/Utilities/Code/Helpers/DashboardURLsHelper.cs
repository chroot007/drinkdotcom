using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DrinkDotCom.Utilities.Code.Helpers
{
    public static class DashboardURLsHelper
    {
        public static string Dashboard(this UrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("Dashboard");

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string ListAction(this UrlHelper helper, string controller, string searchTerm = "", int? pageNo = 0)
        {
            string routeURL = string.Empty;

            var routeValues = new RouteValueDictionary();

            routeValues.Add("Controller", controller);

            if (!string.IsNullOrEmpty(searchTerm))
            {
                routeValues.Add("searchTerm", searchTerm);
            }

            if (pageNo.HasValue && pageNo.Value > 1)
            {
                routeValues.Add("pageNo", pageNo.Value);
            }

            routeURL = helper.RouteUrl("EntityList", routeValues);

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }
        
        public static string Categories(this UrlHelper helper, string searchTerm = "", int? pageNo = 0, int? parentCategoryID = 0)
        {
            string routeURL = string.Empty;

            var routeValues = new RouteValueDictionary();

            routeValues.Add("Controller", "Categories");

            if (!string.IsNullOrEmpty(searchTerm))
            {
                routeValues.Add("searchTerm", searchTerm);
            }

            if (parentCategoryID.HasValue && parentCategoryID.Value > 0)
            {
                routeValues.Add("parentCategoryID", parentCategoryID.Value);
            }

            if (pageNo.HasValue && pageNo.Value > 1)
            {
                routeValues.Add("pageNo", pageNo.Value);
            }

            routeURL = helper.RouteUrl("EntityList", routeValues);

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string Products(this UrlHelper helper, string searchTerm = "", int? pageNo = 0, int? categoryID = 0)
        {
            string routeURL = string.Empty;

            var routeValues = new RouteValueDictionary();

            routeValues.Add("Controller", "Products");

            if (!string.IsNullOrEmpty(searchTerm))
            {
                routeValues.Add("searchTerm", searchTerm);
            }

            if (categoryID.HasValue && categoryID.Value > 0)
            {
                routeValues.Add("categoryID", categoryID.Value);
            }

            if (pageNo.HasValue && pageNo.Value > 1)
            {
                routeValues.Add("pageNo", pageNo.Value);
            }

            routeURL = helper.RouteUrl("EntityList", routeValues);

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }
        public static string Promos(this UrlHelper helper, string searchTerm = "", int? pageNo = 0)
        {
            string routeURL = string.Empty;

            var routeValues = new RouteValueDictionary();

            routeValues.Add("Controller", "Promos");

            if (!string.IsNullOrEmpty(searchTerm))
            {
                routeValues.Add("searchTerm", searchTerm);
            }
            
            if (pageNo.HasValue && pageNo.Value > 1)
            {
                routeValues.Add("pageNo", pageNo.Value);
            }

            routeURL = helper.RouteUrl("EntityList", routeValues);

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }
        
        public static string Orders(this UrlHelper helper, string userEmail = "", int? orderID = 0, int? orderStatus = 0, int? pageNo = 0)
        {
            string routeURL = string.Empty;

            var routeValues = new RouteValueDictionary();

            routeValues.Add("Controller", "Orders");

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

            routeURL = helper.RouteUrl("EntityList", routeValues);

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string OrdersByEmail(this UrlHelper helper, string userEmail, int? orderID = 0, int? orderStatus = 0, int? pageNo = 0)
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

            routeURL = helper.RouteUrl("OrdersByEmail", routeValues);

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }


        public static string UpdateOrderStatus(this UrlHelper helper, int orderID)
        {
            string routeURL = string.Empty;

            var routeValues = new RouteValueDictionary();

            routeValues.Add("orderID", orderID);

            routeURL = helper.RouteUrl("UpdateOrderStatus", routeValues);

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string Users(this UrlHelper helper, string searchTerm = "", string roleID = "", int? pageNo = 0)
        {
            string routeURL = string.Empty;

            var routeValues = new RouteValueDictionary();

            routeValues.Add("Controller", "Users");

            if (!string.IsNullOrEmpty(searchTerm))
            {
                routeValues.Add("searchTerm", searchTerm);
            }

            if (!string.IsNullOrEmpty(roleID))
            {
                routeValues.Add("roleID", roleID);
            }

            if (pageNo.HasValue && pageNo.Value > 1)
            {
                routeValues.Add("pageNo", pageNo.Value);
            }

            routeURL = helper.RouteUrl("EntityList", routeValues);

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string Comments(this UrlHelper helper, string searchTerm = "", string userID = "", int? pageNo = 0)
        {
            string routeURL = string.Empty;

            var routeValues = new RouteValueDictionary();

            routeValues.Add("Controller", "Comments");

            if (!string.IsNullOrEmpty(searchTerm))
            {
                routeValues.Add("searchTerm", searchTerm);
            }

            if (!string.IsNullOrEmpty(userID))
            {
                routeValues.Add("userID", userID);
            }

            if (pageNo.HasValue && pageNo.Value > 1)
            {
                routeValues.Add("pageNo", pageNo.Value);
            }

            routeURL = helper.RouteUrl("EntityList", routeValues);

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string Configurations(this UrlHelper helper, string searchTerm = "", int? configurationType = 0, int? pageNo = 0)
        {
            string routeURL = string.Empty;

            var routeValues = new RouteValueDictionary();

            routeValues.Add("Controller", "Configurations");

            if (!string.IsNullOrEmpty(searchTerm))
            {
                routeValues.Add("searchTerm", searchTerm);
            }

            if (configurationType.HasValue && configurationType.Value > 0)
            {
                routeValues.Add("configurationType", configurationType.Value);
            }

            if (pageNo.HasValue && pageNo.Value > 1)
            {
                routeValues.Add("pageNo", pageNo.Value);
            }

            routeURL = helper.RouteUrl("EntityList", routeValues);

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string CreateAction(this UrlHelper helper, string controller)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("EntityCreate", new
            {
                controller = controller,
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string EditAction(this UrlHelper helper, string controller, object ID)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("EntityEdit", new
            {
                controller = controller,
                ID = ID
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string DetailsAction(this UrlHelper helper, string controller, object ID)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("EntityDetails", new
            {
                controller = controller,
                ID = ID
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string EditAction(this UrlHelper helper, string controller)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("EntityEditWithoutID", new
            {
                controller = controller,
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string DeleteAction(this UrlHelper helper, string controller)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("EntityDelete", new
            {
                controller = controller
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string UserDetails(this UrlHelper helper, string userID)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("UserDetails", new
            {
                userID = userID
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string UserRoles(this UrlHelper helper, string userID)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("UserRoles", new
            {
                userID = userID
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string RoleDetails(this UrlHelper helper, string roleID)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("RoleDetails", new
            {
                roleID = roleID
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string RoleUsers(this UrlHelper helper, string roleID, int? pageNo = 0)
        {
            string routeURL = string.Empty;

            var routeValues = new RouteValueDictionary();

            routeValues.Add("roleID", roleID);
            
            if (pageNo.HasValue && pageNo.Value > 1)
            {
                routeValues.Add("pageNo", pageNo.Value);
            }

            routeURL = helper.RouteUrl("RoleUsers", routeValues);

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string AssignUserRole(this UrlHelper helper, string userID)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("AssignUserRole", new
            {
                userID = userID
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string DeleteUserRole(this UrlHelper helper, string userID)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("DeleteUserRole", new
            {
                userID = userID
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

    }
}