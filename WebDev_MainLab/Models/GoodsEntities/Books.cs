using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebDev_MainLab.Models.GoodsEntities
{
    public class Books
    {
        [Display(Name = "Author")]
        public string Author { get; set; }
        [Display(Name = "ISBN")]
        public string ISBN { get; set; }
        [Display(Name = "PagesCount")]
        public int PagesCount { get; set; }
    }
}
