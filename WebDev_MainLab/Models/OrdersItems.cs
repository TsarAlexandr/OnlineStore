using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDev_MainLab.Models
{
    public class OrdersItems
    {
        public int ID { get; set; }
        public int GoodsId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
    }
}
