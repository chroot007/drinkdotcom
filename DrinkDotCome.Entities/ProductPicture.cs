using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkDotCom.Entities
{
    public class ProductPicture : BaseEntity

    {   
        public int ProductID { get; set; }

        public int PictureID { get; set; }

        public virtual Picture Picture { get; set; }
    }
}
