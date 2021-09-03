using DrinkDotCom.Areas.Dashboard.ViewModels;
using DrinkDotCom.Entities;
using DrinkDotCom.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DrinkDotCom.Areas.Dashboard.Controllers
{
    public class DashboardController : BaseController
    {
        public ActionResult Index()
        {
            DashboardViewModel model = new DashboardViewModel();

            model.CategoriesCount = CategoriesService.Instance.GetCategoriesCount(null, null);
            model.ProductsCount = ProductsService.Instance.GetProductCount(null, null, null, null);
            model.OrdersCount = OrdersService.Instance.SearchOrdersCount(null, null, null);
            model.CommentsCount = CommentsService.Instance.GetCommentsTotalCount(null, null, (int)EntityEnums.Product);
            model.UserCount = DashboardService.Instance.GetUserCount();
            model.RolesCount = DashboardService.Instance.GetRolesCount();

            return View(model);
        }
    }
}