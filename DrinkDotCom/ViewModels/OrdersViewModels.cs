using DrinkDotCom.Entities;
using DrinkDotCom.Utilities.Code.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DrinkDotCom.ViewModels
{
    public class CheckoutViewModel : PageViewModel
    {
        public bool CartHasProducts { get; set; }
        public string PromoCode { get; set; }
    }
    public class TrackOrderViewModel : PageViewModel
    {
        public Order Order { get; set; }
        public int? OrderID { get; set; }
    }

    public class PrintInvoiceViewModel : PageViewModel
    {
        public Order Order { get; set; }
        public int? OrderID { get; set; }
    }

    public class CartItemsViewModel
    {
        public List<int> ProductIDs { get; set; }
        public List<Product> Products { get; set; }
        public string PromoCode { get; set; }
        public Promo Promo { get; internal set; }
    }
    public class DeliveryInfoViewModel
    {
        public DrinkDotComUser User { get; set; }
    }
    public class ConfirmOrderViewModel
    {
        public List<int> ProductIDs { get; set; }
        public List<Product> Products { get; set; }
    }
    public class PlaceOrderCrediCardViewModel : AuthorizeNetCreditCardModel
    {
        [Required]
        [Display(Name = "Customer Name")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Customer Email")]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Country")]
        public string Country { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "ZipCode")]
        public string ZipCode { get; set; }

        public int PromoID { get; set; }
        public decimal Discount { get; set; }

        public List<int> ProductIDs { get; set; }
        public List<Product> Products { get; set; }
    }
    public class PlaceOrderCashOnDeliveryViewModel
    {
        [Required]
        [Display(Name = "Customer Name")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Customer Email")]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Country")]
        public string Country { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "ZipCode")]
        public string ZipCode { get; set; }

        public int PromoID { get; set; }
        public decimal Discount { get; set; }

        public List<int> ProductIDs { get; set; }
        public List<Product> Products { get; set; }
    }
    public class UserOrdersViewModel
    {
        public int? OrderID { get; set; }
        public int? OrderStatus { get; set; }
        public string UserEmail { get; set; }
        public DrinkDotComUser User { get; set; }
        public List<Order> UserOrders { get; set; }
        public Pager Pager { get; set; }

    }
}