using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebDev_MainLab.Models.GoodsEntities
{
    public class Clothes
    {
        [Display(Name = "Sex")]
        public ESex Sex { get; set; }
        [Display(Name = "Size")]
        public int Size { get; set; }
        [Display(Name = "Color")]
        public string Color { get; set; }
        [Display(Name = "Material")]
        public string Material { get; set; }

    }

    public enum ESex
    {
        [Display(Name = "Men")]
        MEN,
        [Display(Name = "Woman")]
        WOMAN,
        [Display(Name = "Child")]
        CHILD
    }
}
