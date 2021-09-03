using DrinkDotComDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrinkDotCom.Entities;

namespace DrinkDotCom.Services
{
    public class CategoriesService
    {
        #region Define as Singleton
        private static CategoriesService _Instance;

        public static CategoriesService Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new CategoriesService();
                }

                return (_Instance);
            }
        }

        private CategoriesService()
        {
        }
        #endregion

        public List<Category> GetAllCategories()
        {
            DrinkDotComContext context = new DrinkDotComContext();

            return context.Categories.OrderBy(x => x.DisplaySeqNo).ToList();
        }

        public List<Category> GetFeaturedCategories(int recordSize = 5)
        {
            DrinkDotComContext context = new DrinkDotComContext();

            return context.Categories.Where(x => x.isFeatured).OrderBy(x => x.DisplaySeqNo).Take(recordSize).ToList();
        }

        public List<Category> GetAllParentCategories()
        {
            DrinkDotComContext context = new DrinkDotComContext();

            return context.Categories.Where(x => !x.ParentCategoryID.HasValue).OrderBy(x => x.DisplaySeqNo).ToList();
        }

        public Category GetCategoryByID(int ID)
        {
            using (var context = new DrinkDotComContext())
            {
                return context.Categories.Find(ID);
            }
        }

        public Category GetCategoryByName(string sanitizedCategoryName)
        {
            DrinkDotComContext context = new DrinkDotComContext();

            return context.Categories.FirstOrDefault(x => x.SanitizedName.Equals(sanitizedCategoryName));
        }

        public void SaveCategory(Category category)
        {
            DrinkDotComContext context = new DrinkDotComContext();

            context.Categories.Add(category);

            context.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            DrinkDotComContext context = new DrinkDotComContext();

            context.Entry(category).State = System.Data.Entity.EntityState.Modified;

            context.SaveChanges();
        }

        public bool DeleteCategory(int ID)
        {
            using (var context = new DrinkDotComContext())
            {
                var category = context.Categories.Find(ID);

                context.Categories.Remove(category);

                return context.SaveChanges() > 0;
            }
        }

        public List<Category> SearchCategories(int? parentCategoryID, string searchTerm, int? pageNo, int pageSize)
        {
            DrinkDotComContext context = new DrinkDotComContext();

            var categories = context.Categories.AsQueryable();

            if (parentCategoryID.HasValue && parentCategoryID.Value > 0)
            {
                categories = categories.Where(x => x.ParentCategoryID == parentCategoryID.Value);
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                categories = categories.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            pageNo = pageNo ?? 1;
            var skipCount = (pageNo.Value - 1) * pageSize;

            return categories.OrderBy(x => x.DisplaySeqNo).Skip(skipCount).Take(pageSize).ToList();
        }

        public int GetCategoriesCount(int? parentCategoryID, string searchTerm)
        {
            DrinkDotComContext context = new DrinkDotComContext();

            var categories = context.Categories.AsQueryable();

            if (parentCategoryID.HasValue && parentCategoryID.Value > 0)
            {
                categories = categories.Where(x => x.ParentCategoryID == parentCategoryID.Value);
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                categories = categories.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            return categories.Count();
        }
    }
}