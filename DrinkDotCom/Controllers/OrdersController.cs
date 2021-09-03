using DrinkDotCom.Utilities.Code.Helpers;
using ClassLibrary.Extensions;
using DrinkDotCom.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DrinkDotCom.Entities;
using DrinkDotCom.Services;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using AuthorizeNet.Api.Contracts.V1;

namespace DrinkDotCom.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private DrinkDotComSignInManager _signInManager;
        private DrinkDotComUserManager _userManager;

        public DrinkDotComSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<DrinkDotComSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
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

        public OrdersController()
        {
        }

        public OrdersController(DrinkDotComUserManager userManager, DrinkDotComSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        [AllowAnonymous]
        public ActionResult CartProducts(string productIDs)
        {
            CartProductsViewModel model = new CartProductsViewModel();

            if (!string.IsNullOrEmpty(productIDs))
            {
                model.ProductIDs = productIDs.Split('-').Select(x => int.Parse(x)).Where(x => x > 0).ToList();

                if (model.ProductIDs.Count > 0)
                {
                    model.Products = ProductsService.Instance.GetProductsByIDs(model.ProductIDs.Distinct().ToList());
                }
            }

            return PartialView("_CartProducts", model);
        }

        public ActionResult Checkout(string promoCode)
        {
            CheckoutViewModel model = new CheckoutViewModel();

            model.PageTitle = "Checkout";
            model.PageDescription = "Checkout your products.";
            model.PageURL = Url.Checkout().ToSiteURL();
            model.PageImageURL = "https://www.websitedevelopers.pk/img/hero/home-website-developers-pk.jpg";

            model.PromoCode = promoCode;

            var cartItemsCookie = Request.Cookies["cartItems"];
            if (cartItemsCookie != null && !string.IsNullOrEmpty(cartItemsCookie.Value))
            {
                model.CartHasProducts = cartItemsCookie.Value.Split('-').Select(x => int.Parse(x)).ToList().Count > 0;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_Checkout", model);
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult CartItems(string promoCode)
        {
            CartItemsViewModel model = new CartItemsViewModel();

            var cartItemsCookie = Request.Cookies["cartItems"];
            if (cartItemsCookie != null && !string.IsNullOrEmpty(cartItemsCookie.Value))
            {
                model.ProductIDs = cartItemsCookie.Value.Split('-').Select(x => int.Parse(x)).ToList();

                if (model.ProductIDs.Count > 0)
                {
                    model.Products = ProductsService.Instance.GetProductsByIDs(model.ProductIDs.Distinct().ToList());
                }

                if (!string.IsNullOrEmpty(promoCode))
                {
                    model.PromoCode = promoCode;
                    model.Promo = PromosService.Instance.GetPromoByCode(promoCode);
                }
            }

            return PartialView("_CartItems", model);
        }

        public ActionResult DeliveryInfo()
        {
            DeliveryInfoViewModel model = new DeliveryInfoViewModel();

            if (User.Identity.IsAuthenticated)
            {
                model.User = UserManager.FindById(User.Identity.GetUserId());
            }
            else
            {
                model.User = new DrinkDotComUser();
            }

            return PartialView("_DeliveryInfo", model);
        }

        public ActionResult ConfirmOrder()
        {
            ConfirmOrderViewModel model = new ConfirmOrderViewModel();

            var cartItemsCookie = Request.Cookies["cartItems"];
            if (cartItemsCookie != null && !string.IsNullOrEmpty(cartItemsCookie.Value))
            {
                model.ProductIDs = cartItemsCookie.Value.Split('-').Select(x => int.Parse(x)).ToList();

                if (model.ProductIDs.Count > 0)
                {
                    model.Products = ProductsService.Instance.GetProductsByIDs(model.ProductIDs.Distinct().ToList());
                }
            }

            return PartialView("_ConfirmOrder", model);
        }

        public JsonResult PlaceOrder(PlaceOrderCrediCardViewModel model)
        {
            JsonResult jsonResult = new JsonResult();

            var errorDetails = string.Empty;

            if (model != null && ModelState.IsValid)
            {
                var cartItemsCookie = Request.Cookies["cartItems"];
                if (cartItemsCookie != null && !string.IsNullOrEmpty(cartItemsCookie.Value))
                {
                    model.ProductIDs = cartItemsCookie.Value.Split('-').Select(x => int.Parse(x)).ToList();

                    if (model.ProductIDs.Count > 0)
                    {
                        model.Products = ProductsService.Instance.GetProductsByIDs(model.ProductIDs.Distinct().ToList());
                    }
                }

                if (model.ProductIDs != null && model.ProductIDs.Count > 0 && model.Products != null && model.Products.Count > 0)
                {
                    var newOrder = new Order();

                    newOrder.CustomerID = User.Identity.GetUserId();
                    newOrder.CustomerName = model.FullName;
                    newOrder.CustomerEmail = model.Email;
                    newOrder.CustomerPhone = model.PhoneNumber;
                    newOrder.CustomerCountry = model.Country;
                    newOrder.CustomerCity = model.City;
                    newOrder.CustomerAddress = model.Address;
                    newOrder.CustomerZipCode = model.ZipCode;

                    newOrder.OrderItems = new List<OrderItem>();
                    foreach (var product in model.Products)
                    {
                        var orderItem = new OrderItem();
                        orderItem.ProductID = product.ID;
                        orderItem.ProductName = product.Name;
                        orderItem.ItemPrice = product.Price;
                        orderItem.Quantity = model.ProductIDs.Where(x => x == product.ID).Count();

                        newOrder.OrderItems.Add(orderItem);
                    }

                    newOrder.OrderCode = Guid.NewGuid().ToString();
                    newOrder.TotalAmmount = newOrder.OrderItems.Sum(x => (x.ItemPrice * x.Quantity));
                    newOrder.DeliveryCharges = ConfigurationsHelper.FlatDeliveryCharges;

                    //Applying Promo/voucher 
                    if (model.PromoID > 0)
                    {
                        var promo = PromosService.Instance.GetPromoByID(model.PromoID);
                        if (promo != null && promo.Value > 0)
                        {
                            if (promo.ValidTill >= DateTime.Now)
                            {
                                newOrder.PromoID = promo.ID;

                                if (promo.PromoType == (int)PromoTypes.Percentage)
                                {
                                    newOrder.Discount = Math.Round((newOrder.TotalAmmount * promo.Value) / 100);
                                }
                                else if (promo.PromoType == (int)PromoTypes.Amount)
                                {
                                    newOrder.Discount = promo.Value;
                                }
                            }
                        }
                    }

                    newOrder.FinalAmmount = newOrder.TotalAmmount + newOrder.DeliveryCharges - newOrder.Discount;
                    newOrder.PaymentMethod = (int)PaymentMethods.CreditCard;

                    newOrder.OrderHistory = new List<OrderHistory>() {
                        new OrderHistory() {
                            OrderStatus = (int)OrderStatus.Successful,
                            ModifiedOn = DateTime.Now,
                            Note = "Order Placed."
                        }
                    };

                    newOrder.PlacedOn = DateTime.Now;

                    #region ExecuteAuthorizeNetPayment Execution

                    var creditCardInfo = new creditCardType
                    {
                        cardNumber = model.CCCardNumber,
                        expirationDate = string.Format("{0}{1}", model.CCExpiryMonth, model.CCExpiryYear),
                        cardCode = model.CCCVC
                    };

                    var authorizeNetResponse = AuthorizeNetHelper.ExecutePayment(newOrder, creditCardInfo);

                    #endregion

                    if (authorizeNetResponse.Success)
                    {
                        newOrder.TransactionID = authorizeNetResponse.Response.transactionResponse.transId;

                        if (OrdersService.Instance.SaveOrder(newOrder))
                        {
                            jsonResult.Data = new
                            {
                                Success = true,
                                Order = newOrder
                            };
                        }
                        else
                        {
                            jsonResult.Data = new
                            {
                                Success = false,
                                Message = "System can not take any order."
                            };
                        }
                    }
                    else
                    {
                        jsonResult.Data = new
                        {
                            Success = authorizeNetResponse.Success,
                            Message = authorizeNetResponse.Message
                        };
                    }
                }
                else
                {
                    jsonResult.Data = new
                    {
                        Success = false,
                        Message = "Invalid products in cart."
                    };
                }
            }
            else
            {
                jsonResult.Data = new
                {
                    Success = false,
                    Message = string.Join("\n", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage))
                };
            }

            return jsonResult;
        }
        public JsonResult PlaceOrderViaCashOnDelivery(PlaceOrderCashOnDeliveryViewModel model)
        {
            JsonResult jsonResult = new JsonResult();

            var errorDetails = string.Empty;

            if (model != null && ModelState.IsValid)
            {
                var cartItemsCookie = Request.Cookies["cartItems"];
                if (cartItemsCookie != null && !string.IsNullOrEmpty(cartItemsCookie.Value))
                {
                    model.ProductIDs = cartItemsCookie.Value.Split('-').Select(x => int.Parse(x)).ToList();

                    if (model.ProductIDs.Count > 0)
                    {
                        model.Products = ProductsService.Instance.GetProductsByIDs(model.ProductIDs.Distinct().ToList());
                    }
                }

                if (model.ProductIDs != null && model.ProductIDs.Count > 0 && model.Products != null && model.Products.Count > 0)
                {
                    var newOrder = new Order();

                    newOrder.CustomerID = User.Identity.GetUserId();
                    newOrder.CustomerName = model.FullName;
                    newOrder.CustomerEmail = model.Email;
                    newOrder.CustomerPhone = model.PhoneNumber;
                    newOrder.CustomerCountry = model.Country;
                    newOrder.CustomerCity = model.City;
                    newOrder.CustomerAddress = model.Address;
                    newOrder.CustomerZipCode = model.ZipCode;

                    newOrder.OrderItems = new List<OrderItem>();
                    foreach (var product in model.Products)
                    {
                        var orderItem = new OrderItem();
                        orderItem.ProductID = product.ID;
                        orderItem.ProductName = product.Name;
                        orderItem.ItemPrice = product.Price;
                        orderItem.Quantity = model.ProductIDs.Where(x => x == product.ID).Count();

                        newOrder.OrderItems.Add(orderItem);
                    }

                    newOrder.OrderCode = Guid.NewGuid().ToString();
                    newOrder.TotalAmmount = newOrder.OrderItems.Sum(x => (x.ItemPrice * x.Quantity));
                    newOrder.DeliveryCharges = ConfigurationsHelper.FlatDeliveryCharges;

                    //Applying Promo/voucher 
                    if (model.PromoID > 0)
                    {
                        var promo = PromosService.Instance.GetPromoByID(model.PromoID);
                        if (promo != null && promo.Value > 0)
                        {
                            if (promo.ValidTill >= DateTime.Now)
                            {
                                newOrder.PromoID = promo.ID;

                                if (promo.PromoType == (int)PromoTypes.Percentage)
                                {
                                    newOrder.Discount = Math.Round((newOrder.TotalAmmount * promo.Value) / 100);
                                }
                                else if (promo.PromoType == (int)PromoTypes.Amount)
                                {
                                    newOrder.Discount = promo.Value;
                                }
                            }
                        }
                    }

                    newOrder.FinalAmmount = newOrder.TotalAmmount + newOrder.DeliveryCharges - newOrder.Discount;
                    newOrder.PaymentMethod = (int)PaymentMethods.CashOnDelivery;

                    newOrder.OrderHistory = new List<OrderHistory>() {
                        new OrderHistory() {
                            OrderStatus = (int)OrderStatus.Successful,
                            ModifiedOn = DateTime.Now,
                            Note = "Order Placed."
                        }
                    };

                    newOrder.PlacedOn = DateTime.Now;

                    if (OrdersService.Instance.SaveOrder(newOrder))
                    {
                        jsonResult.Data = new
                        {
                            Success = true,
                            Order = newOrder
                        };
                    }
                    else
                    {
                        jsonResult.Data = new
                        {
                            Success = false,
                            Message = "System can not take any order."
                        };
                    }
                }
                else
                {
                    jsonResult.Data = new
                    {
                        Success = false,
                        Message = "Invalid products in cart."
                    };
                }
            }
            else
            {
                jsonResult.Data = new
                {
                    Success = false,
                    Message = string.Join("\n", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage))
                };
            }

            return jsonResult;
        }

        public ActionResult Tracking(int? orderID, bool orderPlaced = false)
        {
            TrackOrderViewModel model = new TrackOrderViewModel();
            model.OrderID = orderID;

            if (orderID.HasValue)
            {
                model.Order = OrdersService.Instance.GetOrderByID(orderID.Value);
            }

            if (model.Order != null)
            {
                model.PageTitle = string.Format("Track Order : {0}", model.Order.ID);
                model.PageDescription = "You can check the status of your order. Please enter the order ID and you will get information related to your order.";
                model.PageURL = Url.OrderTrack(orderID.ToString()).ToSiteURL();
            }
            else
            {
                model.PageTitle = string.Format("Track Order");
                model.PageDescription = "You can check the status of your order. Please enter the order ID and you will get information related to your order.";
                model.PageURL = Url.OrderTrack().ToSiteURL();
            }

            model.PageImageURL = PictureHelper.PageImageURL("orders.jpg");

            ViewBag.ShowOrderPlaceMessage = orderPlaced;

            return View(model);
        }

        public ActionResult UserOrders(string userEmail, int? orderID, int? orderStatus, int? pageNo)
        {
            var pageSize = ConfigurationsHelper.DashboardRecordsSizePerPage;

            UserOrdersViewModel model = new UserOrdersViewModel();

            model.UserEmail = !string.IsNullOrEmpty(userEmail) ? userEmail : User.Identity.GetUserEmail();
            model.OrderID = orderID;
            model.OrderStatus = orderStatus;

            model.UserOrders = OrdersService.Instance.SearchOrders(model.UserEmail, model.OrderID, model.OrderStatus, pageNo, pageSize);
            var totalProducts = OrdersService.Instance.SearchOrdersCount(model.UserEmail, model.OrderID, model.OrderStatus);

            model.Pager = new Pager(totalProducts, pageNo, pageSize);

            return PartialView("_UserOrders", model);
        }

        public ActionResult PrintInvoice(int orderID)
        {
            PrintInvoiceViewModel model = new PrintInvoiceViewModel();
            model.OrderID = orderID;

            model.Order = OrdersService.Instance.GetOrderByID(orderID);

            if (model.Order == null)
            {
                return HttpNotFound();
            }

            model.PageTitle = string.Format("Print Invoice Order : {0}", model.Order.ID);
            model.PageDescription = "Print invoice for your order.";
            model.PageURL = Url.PrintInvoice(orderID).ToSiteURL();
            model.PageImageURL = PictureHelper.PageImageURL("orders.jpg");

            return PartialView("_PrintInvoice", model);
        }

    }
}