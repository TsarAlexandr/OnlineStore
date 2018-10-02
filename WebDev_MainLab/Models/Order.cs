using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebDev_MainLab.Models
{
    public class Order
    {
        public int ID { get; set; }
        public Country Country { get; set; }
        public State State{ get; set; }
        [Required]
        [MaxLength(20)]
        public string Adress { get; set; }

    }
}
