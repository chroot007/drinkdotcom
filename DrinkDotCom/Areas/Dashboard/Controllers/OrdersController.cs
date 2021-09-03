using DrinkDotCom.Areas.Dashboard.ViewModels;
using DrinkDotCom.Entities;
using DrinkDotCom.Services;
using DrinkDotCom.Utilities.Code.Helpers;
using DrinkDotCom.ViewModels;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DrinkDotCom.Areas.Dashboard.Controllers
{
    public class OrdersController : BaseController
    {
        private DrinkDotComUserManager _userManager;

        public OrdersController()
        {
        }

        public OrdersController(DrinkDotComUserManager userManager)
        {
            UserManager = userManager;
        }

        public DrinkDotComUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<DrinkDotComUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult Index(string userEmail, int? orderID, int? orderStatus, int? pageNo)
        {
            var pageSize = ConfigurationsHelper.DashboardRecordsSizePerPage;

            OrdersListingViewModel model = new OrdersListingViewModel();

            model.PageTitle = "Orders";
            model.PageDescription = "Orders Listing Page";

            model.UserEmail = userEmail;
            model.OrderID = orderID;
            model.OrderStatus = orderStatus;

            model.Orders = OrdersService.Instance.SearchOrders(model.UserEmail, model.OrderID, model.OrderStatus, pageNo, pageSize);
            var totalProducts = OrdersService.Instance.SearchOrdersCount(model.UserEmail, model.OrderID, model.OrderStatus);

            model.Pager = new Pager(totalProducts, pageNo, pageSize);

            if (Request.IsAjaxRequest())
            {
                return PartialView(model);
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Details(int? ID)
        {
            OrderDetailsViewModel model = new OrderDetailsViewModel();

            if (ID.HasValue)
            {
                model.Order = OrdersService.Instance.GetOrderByID(ID.Value);

                if (model.Order == null) return HttpNotFound();

                model.PageTitle = "Order Details";
                model.PageDescription = string.Format("Order Details: {0}", model.Order.ID);

                if (!string.IsNullOrEmpty(model.Order.CustomerID))
                {
                    model.Customer = await UserManager.FindByIdAsync(model.Order.CustomerID);
                }
            }
            else
            {
                return HttpNotFound();
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult Action(int ID, int? orderStatus, string note)
        {
            JsonResult result = new JsonResult();

            if (ID > 0 && orderStatus.HasValue && !string.IsNullOrEmpty(note))
            {
                var orderHistory = new OrderHistory() { OrderID = ID, OrderStatus = orderStatus.Value, Note = note, ModifiedOn = DateTime.Now };

                var dbOperation = OrdersService.Instance.AddOrderHistory(orderHistory);

                if (dbOperation)
                {
                    result.Data = new { Success = true };

                    return result;
                }
            }

            result.Data = new { Success = false, Message = "Invalid Data. Unable to update order status." };

            return result;
        }

        public ActionResult OrdersByEmail(string userEmail, int? orderID, int? orderStatus, int? pageNo)
        {
            var pageSize = ConfigurationsHelper.DashboardRecordsSizePerPage;

            UserOrdersViewModel model = new UserOrdersViewModel();

            model.UserEmail = userEmail;
            model.OrderID = orderID;
            model.OrderStatus = orderStatus;

            model.UserOrders = OrdersService.Instance.SearchOrders(model.UserEmail, model.OrderID, model.OrderStatus, pageNo, pageSize);
            var totalProducts = OrdersService.Instance.SearchOrdersCount(model.UserEmail, model.OrderID, model.OrderStatus);

            model.Pager = new Pager(totalProducts, pageNo, pageSize);

            return PartialView("_OrdersByEmail", model);
        }
    }
}