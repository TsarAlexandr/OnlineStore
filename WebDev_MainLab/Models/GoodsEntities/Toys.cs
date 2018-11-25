using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebDev_MainLab.Models.GoodsEntities
{
    public class Toys
    {
        [Display(Name ="Age")]
        public int Age { get; set; }
        [Display(Name = "Color")]
        public string Color { get; set; }
    }
}
