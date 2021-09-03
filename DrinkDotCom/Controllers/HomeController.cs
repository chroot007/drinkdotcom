using DrinkDotCom.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DrinkDotCom.Entities;
using DrinkDotCom.Services;
using ClassLibrary.Extensions;
using DrinkDotCom.Utilities.Code.Helpers;
using ClassLibrary;

namespace DrinkDotCom.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            PageViewModel model = new PageViewModel();

            model.PageTitle = "TheDrinkKiosk- Drink Hub";
            model.PageDescription = " Welcome to TheDrinkKiosk, a one stop shop for all kinds of Drinks";
            model.PageURL = Url.Home().ToSiteURL();
            model.PageImageURL = PictureHelper.PageImageURL("products.jpg");

            return View(model);
        }

        [OutputCache(Duration = 600)]
        public ActionResult HomeSliders()
        {
            HomeSlidersViewModel model = new HomeSlidersViewModel();

            model.SlidersConfigurations = ConfigurationsService.Instance.GetConfigurationsByType((int)ConfigurationType.HomeSliders);

            return PartialView("_BannerSlider", model);
        }

        public ActionResult Search(string category, string q, decimal? from, decimal? to, string sortby, int? pageNo, int? pageSize, bool isPartial = false)
        {
            pageSize = pageSize ?? ConfigurationsHelper.FrontendRecordsSizePerPage;

            ProductsViewModel model = new ProductsViewModel();
            model.Categories = CategoriesService.Instance.GetAllCategories();

            if (!string.IsNullOrEmpty(category))
            {
                var selectedCategory = CategoriesService.Instance.GetCategoryByName(category);

                if (selectedCategory == null) return HttpNotFound();
                else
                {
                    model.CategoryID = selectedCategory.ID;
                    model.CategoryName = selectedCategory.SanitizedName;

                    model.SearchedCategories = Methods.GetAllCategoryChildrens(selectedCategory, model.Categories);

                    model.PageTitle = string.Format("{0} Products", selectedCategory.Name);
                    model.PageDescription = selectedCategory.Description;

                    model.PageURL = Url.SearchProducts(selectedCategory.SanitizedName, q).ToSiteURL();
                }
            }
            else
            {
                model.PageTitle = "Search Products";
                model.PageDescription = "Search Latest Products on DrinkDotCom";

                model.PageURL = Url.SearchProducts().ToSiteURL();
            }

            model.PageImageURL = PictureHelper.PageImageURL("products.jpg");

            model.SearchTerm = q;
            model.PriceFrom = from;
            model.PriceTo = to;
            model.SortBy = sortby;
            model.PageSize = pageSize;
            model.isPartial = isPartial;

            var selectedCategoryIDs = model.SearchedCategories != null ? model.SearchedCategories.Select(x => x.ID).ToList() : null;

            model.Products = ProductsService.Instance.SearchProducts(selectedCategoryIDs, model.SearchTerm, model.PriceFrom, model.PriceTo, model.SortBy, pageNo, pageSize.Value);
            var totalProducts = ProductsService.Instance.GetProductCount(selectedCategoryIDs, q, model.PriceFrom, model.PriceTo);

            model.Pager = new Pager(totalProducts, pageNo, pageSize.Value);

            if (model.isPartial)
            {
                return PartialView(model);
            }
            else
            {
                return View(model);
            }
        }
    }
}