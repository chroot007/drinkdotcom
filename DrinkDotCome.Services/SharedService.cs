using DrinkDotCom.Entities;
using DrinkDotComDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkDotCom.Services
{
    public class SharedService
    {
        #region Define as Singleton
        private static SharedService _Instance;

        public static SharedService Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new SharedService();
                }

                return (_Instance);
            }
        }

        private SharedService()
        {
        }
        #endregion

        public int SavePicture(Picture picture)
        {
            DrinkDotComContext context = new DrinkDotComContext();

            context.Pictures.Add(picture);

            context.SaveChanges();

            return picture.ID;
        }
    }
}