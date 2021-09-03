using ClassLibrary.Extensions;
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
    public class CategoriesController : BaseController
    {
        public ActionResult Index(int? parentCategoryID, string searchTerm, int? pageNo)
        {
            var pageSize = ConfigurationsHelper.DashboardRecordsSizePerPage;

            CategoriesListingViewModel model = new CategoriesListingViewModel();

            model.PageTitle = "Categories";
            model.PageDescription = "Categories Listing Page";

            model.ParentCategoryID = parentCategoryID;
            model.SearchTerm = searchTerm;

            model.ParentCategories = CategoriesService.Instance.GetAllParentCategories();

            model.Categories = CategoriesService.Instance.SearchCategories(parentCategoryID, searchTerm, pageNo, pageSize);
            var totalCategories = CategoriesService.Instance.GetCategoriesCount(parentCategoryID, searchTerm);

            model.Pager = new Pager(totalCategories, pageNo, pageSize);

            return View(model);
        }

        [HttpGet]
        public ActionResult Action(int? ID)
        {
            CategoryActionViewModel model = new CategoryActionViewModel();

            if (ID.HasValue)
            {
                var category = CategoriesService.Instance.GetCategoryByID(ID.Value);

                if (category == null) return HttpNotFound();

                model.PageTitle = "Edit Category";
                model.PageDescription = string.Format("Edit Category {0}.", category.Name);

                model.ParentCategoryID = category.ParentCategoryID.HasValue ? category.ParentCategoryID.Value : 0;
                model.ID = category.ID;
                model.Name = category.Name;
                model.Description = category.Description;
                model.isFeatured = category.isFeatured;
            }
            else
            {
                model.PageTitle = "Create Category";
                model.PageDescription = "Create New Category.";
            }

            model.Categories = CategoriesService.Instance.GetAllCategories();

            return View(model);
        }

        [HttpPost]
        public ActionResult Action(CategoryActionViewModel model)
        {
            if (model.ID > 0)
            {
                var category = CategoriesService.Instance.GetCategoryByID(model.ID);

                if (category == null) return HttpNotFound();

                if (model.ParentCategoryID > 0)
                {
                    category.ParentCategoryID = model.ParentCategoryID;
                }
                else
                {
                    category.ParentCategoryID = null;
                    category.ParentCategory = null;
                }

                category.Name = model.Name;
                category.SanitizedName = model.Name.SanitizeLowerString();
                category.Description = model.Description;
                category.isFeatured = model.isFeatured;
                category.DisplaySeqNo = 1;

                CategoriesService.Instance.UpdateCategory(category);
            }
            else
            {
                Category category = new Category();

                if (model.ParentCategoryID > 0)
                {
                    category.ParentCategoryID = model.ParentCategoryID;
                }

                category.Name = model.Name;
                category.SanitizedName = model.Name.SanitizeLowerString();
                category.Description = model.Description;
                category.isFeatured = model.isFeatured;
                category.DisplaySeqNo = 1;

                CategoriesService.Instance.SaveCategory(category);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult Delete(int ID)
        {
            JsonResult result = new JsonResult();

            try
            {
                var operation = CategoriesService.Instance.DeleteCategory(ID);

                result.Data = new { Success = operation, Message = operation ? string.Empty : "Can not delete category." };
            }
            catch (Exception ex)
            {
                result.Data = new { Success = false, Message = string.Format("{0}", ex.Message) };
            }

            return result;
        }
    }
}