using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDev_MainLab.Models.GoodsEntities
{
    public class Clothes
    {
        public ESex Sex { get; set; }
        public int Size { get; set; }
        public string Color { get; set; }
        public string Material { get; set; }

    }

    public enum ESex
    {
        MEN,
        WOMAN,
        CHILD
    }
}
