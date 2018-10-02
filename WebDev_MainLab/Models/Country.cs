using System.ComponentModel.DataAnnotations;


namespace WebDev_MainLab.Models
{
    public class Country
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
