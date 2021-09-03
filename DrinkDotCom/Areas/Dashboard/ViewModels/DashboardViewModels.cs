using DrinkDotCom.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrinkDotCom.Areas.Dashboard.ViewModels
{
    public class DashboardViewModel : PageViewModel
    {
        public int ProductsCount { get; set; }
        public int OrdersCount { get; set; }
        public int CommentsCount { get; set; }
        public int CategoriesCount { get; set; }
        public int UserCount { get; set; }
        public int RolesCount { get; set; }
    }
}