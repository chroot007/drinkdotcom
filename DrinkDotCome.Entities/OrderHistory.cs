using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkDotCom.Entities
{
    public class OrderHistory : BaseEntity
    {
        public int OrderID { get; set; }
        public int OrderStatus { get; set; }
        public string Note { get; set; }
    }
}
