using DrinkDotCom.Utilities.Code.Helpers;
using DrinkDotCom.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DrinkDotCom.Entities;
using DrinkDotCom.Services;
using ClassLibrary.Extensions;

namespace DrinkDotCom.Controllers
{
    public class ProductsController : Controller
    {
        //[OutputCache(Duration = 1000, VaryByParam = "productID;pageSize")]
        public ActionResult FeaturedProducts(int? productID, int pageSize = 0, bool isForHomePage = false)
        {
            if (pageSize == 0)
            {
                pageSize = ConfigurationsHelper.FeaturedRecordsSizePerPage;
            }

            FeaturedProductsViewModel model = new FeaturedProductsViewModel();
            model.Products = ProductsService.Instance.SearchFeaturedProducts(pageSize, new List<int>() { productID.HasValue ? productID.Value : 0 });

            if (isForHomePage)
            {
                return PartialView("_FeaturedProductsHomePage", model);
            }
            else
            {
                return PartialView("_FeaturedProducts", model);
            }
        }

        public ActionResult RecentProducts(int? productID, int pageSize = 0)
        {
            if (pageSize == 0)
            {
                pageSize = ConfigurationsHelper.FrontendRecordsSizePerPage;
            }

            FeaturedProductsViewModel model = new FeaturedProductsViewModel();
            model.Products = ProductsService.Instance.SearchProducts(null, null, null, null, null, 1, pageSize);

            return PartialView("_RecentProducts", model);
        }

        public ActionResult RelatedProducts(int categoryID, int pageSize = 0)
        {
            if (pageSize == 0)
            {
                pageSize = ConfigurationsHelper.RelatedProductsRecordsSizePerPage;
            }

            RelatedProductsViewModel model = new RelatedProductsViewModel();
            model.Products = ProductsService.Instance.SearchProducts(new List<int>() { categoryID }, null, null, null, null, 1, pageSize);

            if (model.Products != null && model.Products.Count >= ConfigurationsHelper.RelatedProductsRecordsSizePerPage)
            {
                model.PageTitle = model.Products.FirstOrDefault().Category.Name;
            }
            else
            {
                //the realted products are less than the specfified RelatedProductsRecordsSizePerPage, so instead show featured products
                model.PageTitle = string.Format("{0}'s", ConfigurationsHelper.ApplicationName);
                model.Products = ProductsService.Instance.SearchFeaturedProducts(pageSize);
            }

            return PartialView("_RelatedProducts", model);
        }


        [HttpGet]
        public ActionResult Details(int ID, string category)
        {
            ProductDetailsViewModel model = new ProductDetailsViewModel();

            model.Product = ProductsService.Instance.GetProductByID(ID);

            if (model.Product == null || !model.Product.Category.SanitizedName.ToLower().Equals(category))
                return HttpNotFound();

            model.EntityID = (int)EntityEnums.Product;
            model.RecordID = model.Product.ID;
            model.Comments = CommentsService.Instance.GetComments(model.EntityID, model.RecordID);

            model.PageTitle = model.Product.Name;
            model.PageDescription = model.Product.Summary;
            model.PageURL = Url.ProductDetails(category, model.Product.ID).ToSiteURL();

            var thumbnail = model.Product.ProductPictures.Where(x => x.PictureID == model.Product.ThumbnailPictureID).FirstOrDefault();

            if (thumbnail != null)
            {
                model.PageImageURL = PictureHelper.PageImageURL(thumbnail.Picture.URL, isCompletePath: true);
            }
            else
            {
                model.PageImageURL = PictureHelper.PageImageURL("products.jpg");
            }

            return View(model);
        }
    }
}