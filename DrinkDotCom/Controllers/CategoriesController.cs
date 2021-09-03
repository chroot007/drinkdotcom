using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DrinkDotCom.Services;

namespace DrinkDotCom.Controllers
{
    public class CategoriesController : Controller
    {
        //[OutputCache(Duration = 1000, VaryByParam = "none")]
        public ActionResult FeaturedCategories()
        {
            return PartialView("_FeaturedCategoriesMenuItem", CategoriesService.Instance.GetFeaturedCategories());
        }
    }
}