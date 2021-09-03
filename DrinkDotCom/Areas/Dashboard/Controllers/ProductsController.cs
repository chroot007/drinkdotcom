using ClassLibrary;
using DrinkDotCom.Areas.Dashboard.ViewModels;
using DrinkDotCom.Entities;
using DrinkDotCom.Services;
using DrinkDotCom.Utilities.Code.Commons;
using DrinkDotCom.Utilities.Code.Helpers;
using DrinkDotCom.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DrinkDotCom.Areas.Dashboard.Controllers
{
    public class ProductsController : BaseController
    {
        public ActionResult Index(int? categoryID, string searchTerm, int? pageNo)
        {
            var pageSize = ConfigurationsHelper.DashboardRecordsSizePerPage;

            ProductsListingViewModel model = new ProductsListingViewModel();

            model.PageTitle = "Products";
            model.PageDescription = "Products Listing Page";

            model.SearchTerm = searchTerm;

            model.Categories = CategoriesService.Instance.GetAllCategories();

            List<int> selectedCategoryIDs = null;

            if (categoryID.HasValue && categoryID.Value > 0)
            {
                var selectedCategory = CategoriesService.Instance.GetCategoryByID(categoryID.Value);

                if (selectedCategory == null) return HttpNotFound();
                else
                {
                    model.CategoryID = selectedCategory.ID;

                    var searchedCategories = Methods.GetAllCategoryChildrens(selectedCategory, model.Categories);

                    selectedCategoryIDs = searchedCategories != null ? searchedCategories.Select(x => x.ID).ToList() : null;
                }
            }

            model.Products = ProductsService.Instance.SearchProducts(selectedCategoryIDs, searchTerm, null, null, null, pageNo, pageSize);
            var totalProducts = ProductsService.Instance.GetProductCount(selectedCategoryIDs, searchTerm, null, null);

            model.Pager = new Pager(totalProducts, pageNo, pageSize);

            return View(model);
        }

        [HttpGet]
        public ActionResult Action(int? ID)
        {
            ProductActionViewModel model = new ProductActionViewModel();

            if (ID.HasValue)
            {
                var product = ProductsService.Instance.GetProductByID(ID.Value);

                if (product == null) return HttpNotFound();

                model.PageTitle = "Edit Product";
                model.PageDescription = string.Format("Edit Product {0}.", product.Name);

                model.ID = product.ID;
                model.CategoryID = product.CategoryID;
                model.Name = product.Name;
                model.Summary = product.Summary;
                model.Description = product.Description;
                model.Price = product.Price;
                model.isFeatured = product.isFeatured;
                model.ProductPicturesList = product.ProductPictures;
                model.ThumbnailPicture = product.ThumbnailPictureID;
                model.ProductSpecifications = product.ProductSpecifications;
            }
            else
            {
                model.PageTitle = "Create Product";
                model.PageDescription = "Create New Product.";
            }

            model.Categories = CategoriesService.Instance.GetAllCategories();

            return View(model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Action(FormCollection formCollection)
        {
            ProductActionViewModel model = GetProductActionViewModelFromForm(formCollection);

            if (model.ID > 0)
            {
                var product = ProductsService.Instance.GetProductByID(model.ID);

                if (product == null) return HttpNotFound();

                product.ID = model.ID;
                product.Name = model.Name;
                product.CategoryID = model.CategoryID;
                product.Summary = model.Summary;
                product.Description = model.Description;
                product.Price = model.Price;
                product.isFeatured = model.isFeatured;
                product.ModifiedOn = DateTime.Now;

                if (model.ProductSpecifications != null)
                {
                    product.ProductSpecifications.Clear();
                    product.ProductSpecifications.AddRange(model.ProductSpecifications.Select(x => new ProductSpecification() { ProductID = product.ID, Title = x.Title, Value = x.Value }));
                }

                if (!string.IsNullOrEmpty(model.ProductPictures))
                {
                    var pictureIDs = model.ProductPictures
                                                .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                                .Select(ID => int.Parse(ID)).ToList();

                    if (pictureIDs.Count > 0)
                    {
                        product.ProductPictures.Clear();
                        product.ProductPictures.AddRange(pictureIDs.Select(x => new ProductPicture() { ProductID = product.ID, PictureID = x }).ToList());

                        product.ThumbnailPictureID = model.ThumbnailPicture != 0 ? model.ThumbnailPicture : pictureIDs.First();
                    }
                }

                ProductsService.Instance.UpdateProduct(product);
            }
            else
            {
                Product product = new Product();

                product.ID = model.ID;
                product.Name = model.Name;
                product.CategoryID = model.CategoryID;
                product.Summary = model.Summary;
                product.Description = model.Description;
                product.Price = model.Price;
                product.isFeatured = model.isFeatured;
                product.ModifiedOn = DateTime.Now;

                if (model.ProductSpecifications != null)
                {
                    product.ProductSpecifications = new List<ProductSpecification>();
                    product.ProductSpecifications.AddRange(model.ProductSpecifications.Select(x => new ProductSpecification() { ProductID = product.ID, Title = x.Title, Value = x.Value }));
                }

                if (!string.IsNullOrEmpty(model.ProductPictures))
                {
                    var pictureIDs = model.ProductPictures
                                                .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                                .Select(ID => int.Parse(ID)).ToList();

                    if (pictureIDs.Count > 0)
                    {
                        product.ProductPictures = new List<ProductPicture>();
                        product.ProductPictures.AddRange(pictureIDs.Select(x => new ProductPicture() { ProductID = product.ID, PictureID = x }).ToList());

                        product.ThumbnailPictureID = model.ThumbnailPicture != 0 ? model.ThumbnailPicture : pictureIDs.First();
                    }
                }

                ProductsService.Instance.SaveProduct(product);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult Delete(int ID)
        {
            JsonResult result = new JsonResult();

            try
            {
                var operation = ProductsService.Instance.DeleteProduct(ID);

                result.Data = new { Success = operation, Message = operation ? string.Empty : "Can not delete product." };
            }
            catch (Exception ex)
            {
                result.Data = new { Success = false, Message = string.Format("{0}", ex.Message) };
            }

            return result;
        }

        private ProductActionViewModel GetProductActionViewModelFromForm(FormCollection formCollection)
        {
            var model = new ProductActionViewModel();

            model.ID = !string.IsNullOrEmpty(formCollection["ID"]) ? int.Parse(formCollection["ID"]) : 0;
            model.CategoryID = int.Parse(formCollection["CategoryID"]);
            model.Name = formCollection["Name"];
            model.Summary = formCollection["Summary"];
            model.Description = formCollection["Description"];
            model.Price = decimal.Parse(formCollection["Price"]);
            model.isFeatured = formCollection["isFeatured"].Contains("true");
            model.ProductPictures = formCollection["ProductPictures"];
            model.ThumbnailPicture = !string.IsNullOrEmpty(formCollection["ThumbnailPicture"]) ? int.Parse(formCollection["ThumbnailPicture"]) : 0;

            model.ProductSpecifications = new List<ProductSpecification>();

            foreach (string key in formCollection)
            {
                if (key.Contains("specification"))
                {
                    var value = formCollection[key];

                    if (!string.IsNullOrEmpty(value))
                    {
                        var specificationTitle = value.GetSubstringText("", "~");
                        var specificationValue = value.GetSubstringText("~", "");

                        if (!string.IsNullOrEmpty(specificationTitle) && !string.IsNullOrEmpty(specificationValue))
                        {
                            model.ProductSpecifications.Add(new ProductSpecification() { Title = specificationTitle, Value = specificationValue });
                        }
                    }
                }
            }

            return model;
        }
    }
}