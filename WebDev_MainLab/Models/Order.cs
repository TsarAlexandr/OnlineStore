using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;


namespace WebDev_MainLab.Models
{
    public class Order
    {
        public int ID { get; set; }
        [Display(Name = "Country")]
        [Required(ErrorMessage = "CountryRequired")]
        public string Country { get; set; }
        [Display(Name ="City")]
        [Required(ErrorMessage = "CityRequired")]
        public string State{ get; set; }
        [Required(ErrorMessage = "AdressRequired")]
        [MaxLength(20)]
        [Display(Name="Adress")]
        public string Adress { get; set; }
        [Required(ErrorMessage = "AdressRequired")]
        [MaxLength(20, ErrorMessage = "NameLength")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "AdressRequired")]
        [MaxLength(20, ErrorMessage = "SurnameLength")]
        [Display(Name = "Surname")]
        public string Surname { get; set; }
        public string UserID { get; set; }
        public double TotalPrice { get; set; }

    }
}
