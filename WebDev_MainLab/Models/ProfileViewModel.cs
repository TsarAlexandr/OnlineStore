using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDev_MainLab.Models
{
    public class ProfileViewModel
    {
        public string Name { get; set; }
        
        public List<Order> OrdersList { get; set; }

    }
}
