using DrinkDotCom.Entities;
using DrinkDotCom.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrinkDotCom.Areas.Dashboard.ViewModels
{
    public class CategoriesListingViewModel : PageViewModel
    {
        public List<Category> Categories { get; set; }
        public List<Category> ParentCategories { get; set; }

        public int? ParentCategoryID { get; set; }
        public string SearchTerm { get; set; }

        public Pager Pager { get; set; }
    }

    public class CategoryActionViewModel : PageViewModel
    {
        public int ParentCategoryID { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool isFeatured { get; set; }

        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
    }
}