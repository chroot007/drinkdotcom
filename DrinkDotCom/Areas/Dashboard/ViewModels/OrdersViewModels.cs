using DrinkDotCom.Entities;
using DrinkDotCom.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrinkDotCom.Areas.Dashboard.ViewModels
{
    public class OrdersListingViewModel : PageViewModel
    {
        public List<Order> Orders { get; set; }

        public string UserEmail { get; set; }
        public int? OrderID { get; set; }
        public int? OrderStatus { get; set; }

        public Pager Pager { get; set; }
    }

    public class OrderDetailsViewModel : PageViewModel
    {
        public Order Order { get; set; }
        public DrinkDotComUser Customer { get; set; }
    }
}