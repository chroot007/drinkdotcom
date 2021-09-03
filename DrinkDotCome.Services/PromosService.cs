using DrinkDotCom.Entities;
using DrinkDotComDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkDotCom.Services
{
    public class PromosService
    {
        #region Define as Singleton
        private static PromosService _Instance;

        public static PromosService Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new PromosService();
                }

                return (_Instance);
            }
        }

        private PromosService()
        {
        }
        #endregion

        public List<Promo> GetAllPromos()
        {
            DrinkDotComContext context = new DrinkDotComContext();

            return context.Promos.ToList();
        }

        public List<Promo> SearchPromos(string searchTerm, int? pageNo, int pageSize)
        {
            DrinkDotComContext context = new DrinkDotComContext();

            var Promos = context.Promos.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                Promos = Promos.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            pageNo = pageNo ?? 1;

            var skipCount = (pageNo.Value - 1) * pageSize;

            return Promos.OrderByDescending(x => x.Name).Skip(skipCount).Take(pageSize).ToList();
        }

        public int GetPromosCount(string searchTerm)
        {
            DrinkDotComContext context = new DrinkDotComContext();

            var Promos = context.Promos.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                Promos = Promos.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower()));
            }


            return Promos.Count();
        }

        public Promo GetPromoByID(int ID)
        {
            DrinkDotComContext context = new DrinkDotComContext();

            return context.Promos.Find(ID);
        }
        public Promo GetPromoByCode(string code)
        {
            DrinkDotComContext context = new DrinkDotComContext();

            return context.Promos.FirstOrDefault(x => x.Code == code);
        }

        public List<Promo> GetPromosByIDs(List<int> IDs)
        {
            DrinkDotComContext context = new DrinkDotComContext();

            return IDs.Select(id => context.Promos.Find(id)).OrderBy(x => x.ID).ToList();
        }

        public void SavePromo(Promo Promo)
        {
            DrinkDotComContext context = new DrinkDotComContext();

            context.Promos.Add(Promo);

            context.SaveChanges();
        }


        public void UpdatePromo(Promo Promo)
        {
            DrinkDotComContext context = new DrinkDotComContext();

            var exitingPromo = context.Promos.Find(Promo.ID);

            context.Entry(exitingPromo).CurrentValues.SetValues(Promo);

            context.SaveChanges();
        }

        public bool DeletePromo(int ID)
        {
            using (var context = new DrinkDotComContext())
            {
                var promo = context.Promos.Find(ID);

                context.Promos.Remove(promo);

                return context.SaveChanges() > 0;
            }
        }
    }
}
