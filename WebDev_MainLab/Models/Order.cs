﻿using System.ComponentModel.DataAnnotations;


namespace WebDev_MainLab.Models
{
    public class Order
    {
        public int ID { get; set; }
        public int CountryID { get; set; }
        public int StateID{ get; set; }
        [Required]
        [MaxLength(20)]
        public string Adress { get; set; }

    }
}
