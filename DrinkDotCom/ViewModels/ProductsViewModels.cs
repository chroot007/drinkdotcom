using DrinkDotCom.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrinkDotCom.ViewModels
{
    public class ProductsViewModel : PageViewModel
    {
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }

        public int? CategoryID { get; set; }
        public string CategoryName { get; set; }
        public List<Category> SearchedCategories { get; set; }

        public string SearchTerm { get; set; }
        public string SortBy { get; set; }

        public bool isPartial { get; set; }

        public Pager Pager { get; set; }
        public int PageNo { get; set; }
        public int? PageSize { get; set; }

        public List<string> Brands { get; set; }
        public List<string> Colors { get; set; }
        public List<string> Sizes { get; set; }
        public decimal? PriceFrom { get; internal set; }
        public decimal? PriceTo { get; internal set; }
    }

    public class FeaturedProductsViewModel : PageViewModel
    {
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }
    }
    public class RelatedProductsViewModel : FeaturedProductsViewModel
    {
        public Category Category { get; set; }
    }

    public class CartProductsViewModel
    {
        public List<int> ProductIDs { get; set; }
        public List<Product> Products { get; set; }
    }

    public class ProductDetailsViewModel : CommentablePageViewModel
    {
        public Product Product { get; set; }
    }
}