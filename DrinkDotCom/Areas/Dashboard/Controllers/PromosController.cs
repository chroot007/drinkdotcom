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
    public class PromosController : BaseController
    {
        public ActionResult Index(string searchTerm, int? pageNo)
        {
            var pageSize = ConfigurationsHelper.DashboardRecordsSizePerPage;

            PromosListingViewModel model = new PromosListingViewModel();

            model.PageTitle = "Promos";
            model.PageDescription = "Promos Listing Page";

            model.SearchTerm = searchTerm;

            model.Promos = PromosService.Instance.SearchPromos(searchTerm, pageNo, pageSize);
            var totalPromos = PromosService.Instance.GetPromosCount(searchTerm);

            model.Pager = new Pager(totalPromos, pageNo, pageSize);

            return View(model);
        }

        [HttpGet]
        public ActionResult Action(int? ID, string errorMessage)
        {
            PromoActionViewModel model = new PromoActionViewModel();

            if (ID.HasValue)
            {
                var promo = PromosService.Instance.GetPromoByID(ID.Value);

                if (promo == null) return HttpNotFound();

                model.PageTitle = "Edit Promo";
                model.PageDescription = string.Format("Edit Promo {0}.", promo.Name);

                model.ID = promo.ID;
                model.PromoType = promo.PromoType;
                model.Name = promo.Name;
                model.Description = promo.Description;
                model.Code = promo.Code;
                model.Value = promo.Value;
                model.ValidTill = promo.ValidTill;
            }
            else
            {
                model.PageTitle = "Create Promo";
                model.PageDescription = "Create New Promo.";
            }

            return View(model);
        }

        [HttpPost, ValidateInput(false)]
        public JsonResult Action(PromoActionViewModel model)
        {
            JsonResult json = new JsonResult();

            if (model.ID > 0)
            {
                var promo = PromosService.Instance.GetPromoByID(model.ID);

                if (promo == null)
                {

                }
                else
                {
                    promo.ID = model.ID;
                    promo.PromoType = model.PromoType;
                    promo.Name = model.Name;
                    promo.Description = model.Description;
                    promo.Code = model.Code;
                    promo.Value = model.Value;
                    promo.ValidTill = model.ValidTill;

                    try
                    {
                        PromosService.Instance.UpdatePromo(promo);

                        json.Data = new { Success = true };
                    }
                    catch
                    {
                        json.Data = new { Success = false, Message = "Unable to update promo. Please use a unique code." };
                    }
                }
            }
            else
            {
                Promo promo = new Promo();

                promo.ID = model.ID;
                promo.PromoType = model.PromoType;
                promo.Name = model.Name;
                promo.Description = model.Description;
                promo.Code = model.Code;
                promo.Value = model.Value;
                promo.ValidTill = model.ValidTill;

                try
                {
                    PromosService.Instance.SavePromo(promo);

                    json.Data = new { Success = true };
                }
                catch
                {
                    json.Data = new { Success = false, Message = "Unable to save promo. Please use a unique code." };
                }
            }

            return json;
        }

        [HttpPost]
        public JsonResult Delete(int ID)
        {
            JsonResult result = new JsonResult();

            try
            {
                var operation = PromosService.Instance.DeletePromo(ID);

                result.Data = new { Success = operation, Message = operation ? string.Empty : "Can not delete promo." };
            }
            catch (Exception ex)
            {
                result.Data = new { Success = false, Message = string.Format("{0}", ex.Message) };
            }

            return result;
        }
    }
}