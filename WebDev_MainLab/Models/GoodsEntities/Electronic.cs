using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebDev_MainLab.Models.GoodsEntities
{
    public class Electronics
    {
        [Display(Name = "Power")]
        public string Power { get; set; }
        [Display(Name = "CPU")]
        public string CPU { get; set; }
        [Display(Name = "Memory")]
        public string Memory { get; set; }
        [Display(Name = "OS")]
        public string OS { get; set; }
    }
}
