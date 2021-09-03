using DrinkDotCom.Areas.Dashboard.ViewModels;
using DrinkDotCom.Entities;
using DrinkDotCom.Services;
using DrinkDotCom.Utilities.Code.Helpers;
using DrinkDotCom.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DrinkDotCom.Areas.Dashboard.Controllers
{
    public class ConfigurationsController : BaseController
    {
        public ActionResult Index(int? configurationType, string searchTerm, int? pageNo, bool isPartial = false)
        {
            var pageSize = ConfigurationsHelper.DashboardRecordsSizePerPage;

            ConfigurationsListingViewModels model = new ConfigurationsListingViewModels();

            model.PageTitle = "Configurations";
            model.PageDescription = "Configurations Listing Page";

            model.ConfigurationType = configurationType;
            model.SearchTerm = searchTerm;

            model.Configurations = ConfigurationsService.Instance.SearchConfigurations(configurationType, searchTerm, pageNo, pageSize);

            var totalConfigurations = ConfigurationsService.Instance.GetConfigurationsCount(configurationType, searchTerm);

            model.Pager = new Pager(totalConfigurations, pageNo, pageSize);

            if (isPartial)
            {
                return PartialView("_Listing", model);
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Action(string key)
        {
            var configuration = ConfigurationsService.Instance.GetConfigurationByKey(key);

            if (configuration == null) return HttpNotFound();

            if (configuration.ConfigurationType == (int)ConfigurationType.HomeSliders)
            {
                return PartialView("_HomeSlidersEdit", configuration);
            }
            else return PartialView("_Edit", configuration);
        }

        [HttpPost]
        public ActionResult Action(Configuration configuration)
        {
            if (configuration == null) return HttpNotFound();

            if (configuration.ConfigurationType == (int)ConfigurationType.HomeSliders)
            {
                ConfigurationsService.Instance.UpdateConfigurationValue(configuration.Key, configuration.Value);
            }
            else
            {
                ConfigurationsService.Instance.UpdateConfiguration(configuration);
            }

            return RedirectToAction("Index", new { isPartial = true });
        }
    }
}