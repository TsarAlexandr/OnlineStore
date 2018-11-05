using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDev_MainLab.Models
{
    public class Commentar
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public int UserID { get; set; }
        public int GoodsID { get; set; }
    }
}
