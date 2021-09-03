using DrinkDotCom.Utilities.Code.Helpers;
using ClassLibrary.Extensions;
using DrinkDotCom.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DrinkDotCom.Controllers
{
    public class ContentsController : Controller
    {
        public ActionResult AboutUs()
        {
            PageViewModel model = new PageViewModel();

            model.PageTitle = "About Us";
            model.PageDescription = String.Format("Know more about us and the great work we do here at {0}.", ConfigurationsHelper.ApplicationName);
            model.PageURL = Url.RouteUrl("AboutUs").ToSiteURL();
            model.PageImageURL = PictureHelper.PageImageURL("about-us.jpg");

            return View(model);
        }

        public ActionResult ContactUs()
        {
            PageViewModel model = new PageViewModel();

            model.PageTitle = "Contact Us";
            model.PageDescription = string.Format("Contact {0} Team.", ConfigurationsHelper.ApplicationName);
            model.PageURL = Url.RouteUrl("ContactUs").ToSiteURL();
            model.PageImageURL = PictureHelper.PageImageURL("contact-us.jpg");

            return View(model);
        }

        public ActionResult Blog()
        {
            PageViewModel model = new PageViewModel();

            model.PageTitle = "Blog";
            model.PageDescription = string.Format("Latest updates from {0}.", ConfigurationsHelper.ApplicationName);
            model.PageURL = Url.RouteUrl("Blog").ToSiteURL();
            model.PageImageURL = PictureHelper.PageImageURL("blog.jpg");

            return View(model);
        }

        public ActionResult PrivacyPolicy()
        {
            PageViewModel model = new PageViewModel();

            model.PageTitle = "Privacy Policy";
            model.PageDescription = string.Format("Read {0} Privacy Policy.", ConfigurationsHelper.ApplicationName);
            model.PageURL = Url.RouteUrl("PrivacyPolicy").ToSiteURL();
            model.PageImageURL = PictureHelper.PageImageURL("privacy-policy.jpg");

            return View(model);
        }

        public ActionResult RefundPolicy()
        {
            PageViewModel model = new PageViewModel();

            model.PageTitle = "Refund Policy";
            model.PageDescription = string.Format("Read {0} Refund Policy.", ConfigurationsHelper.ApplicationName);
            model.PageURL = Url.RouteUrl("RefundPolicy").ToSiteURL();
            model.PageImageURL = PictureHelper.PageImageURL("refund-policy.jpg");

            return View(model);
        }

        public ActionResult TermsConditions()
        {
            PageViewModel model = new PageViewModel();

            model.PageTitle = "Terms & Conditions";
            model.PageDescription = string.Format("Read {0} Terms & Conditions.", ConfigurationsHelper.ApplicationName);
            model.PageURL = Url.RouteUrl("TermsConditions").ToSiteURL();
            model.PageImageURL = PictureHelper.PageImageURL("terms-conditions.jpg");

            return View(model);
        }
    }
}