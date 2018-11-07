using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;


namespace WebDev_MainLab.Models
{
    public class Order
    {
        public int ID { get; set; }
        [Display(Name = "Country")]
        public string Country { get; set; }
        [Display(Name ="City")]
        public string State{ get; set; }
        [Required]
        [MaxLength(20)]
        public string Adress { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        [Required]
        [MaxLength(20)]
        public string Surname { get; set; }
        public List<CartLine> Items { get; set; }
        public string UserID { get; set; }
        public double TotalPrice { get; set; }

    }
}
