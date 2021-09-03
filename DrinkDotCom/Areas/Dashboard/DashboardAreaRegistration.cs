using System.Web.Mvc;

namespace DrinkDotCom.Areas.Dashboard
{
    public class DashboardAreaRegistration : AreaRegistration 
    {
        public override string AreaName
        {
            get
            {
                return "Dashboard";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Dashboard",
                "Dashboard",
                new { controller = "Dashboard", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "UnAuthorized",
                "UnAuthorized",
                new { controller = "Base", action = "UnAuthorized" }
            );

            context.MapRoute(
                "EntityList",
                "Dashboard/{controller}/",
                new { action = "Index" }
            );

            context.MapRoute(
                "EntityCreate",
                "Dashboard/{controller}/Create/",
                new { action = "Action" }
            );

            context.MapRoute(
                "EntityEdit",
                "Dashboard/{controller}/Edit/{id}",
                new { action = "Action" }
            );

            context.MapRoute(
                "EntityEditWithoutID",
                "Dashboard/{controller}/Edit/",
                new { action = "Action" }
            );

            context.MapRoute(
                "EntityDetails",
                "Dashboard/{controller}/Details/{id}",
                new { action = "Details" }
            );

            context.MapRoute(
                "EntityDelete",
                "Dashboard/{controller}/Delete/",
                new { action = "Delete" }
            );

            context.MapRoute(
                name: "UserDetails",
                url: "Dashboard/Users/UserDetails/{userID}",
                defaults: new { controller = "Users", action = "UserDetails" }
            );

            context.MapRoute(
                name: "UserRoles",
                url: "Dashboard/Users/UserRoles/{userID}",
                defaults: new { controller = "Roles", action = "UserRoles" }
            );

            context.MapRoute(
                name: "RoleDetails",
                url: "Dashboard/Roles/RoleDetails/{roleID}",
                defaults: new { controller = "Roles", action = "RoleDetails" }
            );

            context.MapRoute(
                name: "RoleUsers",
                url: "Dashboard/Roles/RoleUsers/{roleID}",
                defaults: new { controller = "Roles", action = "RoleUsers" }
            );

            context.MapRoute(
                name: "AssignUserRole",
                url: "Dashboard/Roles/UserRole/{userID}/Assign/",
                defaults: new { controller = "Roles", action = "AssignUserRole" }
            );

            context.MapRoute(
                name: "DeleteUserRole",
                url: "Dashboard/Roles/UserRole/{userID}/Delete/",
                defaults: new { controller = "Roles", action = "DeleteUserRole" }
            );

            context.MapRoute(
                name: "OrdersByEmail",
                url: "Dashboard/Orders/OrdersByEmail",
                defaults: new { controller = "Orders", action = "OrdersByEmail" }
            );

            context.MapRoute(
                name: "UpdateOrderStatus",
                url: "Dashboard/Orders/{orderID}/Update-Status/",
                defaults: new { controller = "Orders", action = "UpdateStatus" }
            );

            context.MapRoute(
                "Dashboard_Default",
                "Dashboard/{controller}/{action}/{id}",
                new { controller = "Dashboard", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}