using System;
using System.Collections.Generic;
using System.Linq;

namespace WebDev_MainLab.Models
{
    public class CartLine
    {
        public int ID { get; set; }
        public Goods MyItem { get; set; }
        public int Quantity { get; set; }
    }
    public class Cart
    {
        private List<CartLine> lines;

        public void AddItem(Goods good, int quantity)
        {
            var res = lines.FirstOrDefault(x => x.MyItem.ID == good.ID);
            if (res == null)
            {
                lines.Add(new CartLine() { MyItem = good, Quantity = quantity });
            }
            else
            {
                res.Quantity += quantity;
            }
        }
        public void RemoveLine(Goods good)
        {
            var res = lines.FirstOrDefault(x => x.MyItem.ID == good.ID);
            if (res.Quantity > 1)
                res.Quantity--;
            else
                lines.RemoveAll(l => l.MyItem.ID == good.ID);
        }

        public double ComputeTotalValue()
        {
           return lines.Sum(e => Double.Parse(e.MyItem.Price) * e.Quantity);

        }
        public void Clear()
        {
            lines.Clear();
        }

        public List<CartLine> Lines
        {
            get
            {
                return lines;
            }
            set
            {
                if (lines == null)
                {
                    lines = value;
                }
            }
        }
    }
}
