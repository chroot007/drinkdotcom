using DrinkDotCom.Entities;
using DrinkDotCom.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrinkDotCom.Areas.Dashboard.ViewModels
{
    public class ProductsListingViewModel : PageViewModel
    {
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }

        public int? CategoryID { get; set; }
        public string SearchTerm { get; set; }

        public Pager Pager { get; set; }
    }

    public class ProductActionViewModel : PageViewModel
    {
        public int CategoryID { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool isFeatured { get; set; }

        public string ProductPictures { get; set; }
        public int ThumbnailPicture { get; set; }
        public List<ProductPicture> ProductPicturesList { get; set; }

        public List<Category> Categories { get; set; }
        public List<ProductSpecification> ProductSpecifications { get; set; }
    }
}