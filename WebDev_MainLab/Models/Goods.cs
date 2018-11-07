using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebDev_MainLab.Models
{
    public class Goods
    {
        private int _rating;
        private string _name, _description, _price;


        public int ID { get; set; }


        [Required]
        [Range(1, 10, ErrorMessage = "Rating should be in range 1 to 10")]
        public int Rating
        {
            get { return _rating; }
            set
            {
                if (isRatingInRange(value))
                    _rating = value;
            }
        }
        [Required]
        [StringLength(50)]
        public string Name
        {
            get { return _name; }
            set
            {
                if (isStringCorrect(value))
                    _name = value;
            }
        }

        [Required]
        [StringLength(50)]
        public string Description
        {
            get { return _description; }
            set
            {
                if (isStringCorrect(value))
                    _description = value;
            }
        }
        [Required]
        [DataType(DataType.Currency)]
        public string Price
        {
            get { return _price; }
            set
            {
                if (isPriceCorrect(value))
                    _price = value;
            }
        }
        [NotMapped]
        public IFormFile ImageMimeType { get; set; }
        public byte[] ImageData { get; set; }
        [Required]
        public Categories Category { get; set; }
        public List<Commentar> Comments { get; set; }
        public string AdditionalParameters { get; set; }

        public bool isPriceCorrect(string value)
        {
            var len = value.Length;
            double res;
            if (len < 8)
                if (Double.TryParse(value, out res))
                    if (value.IndexOf(',') == (len - 3))
                        return true;
            return false;
        }
        public bool isRatingInRange(int value)
        {
            return value >= 1 && value <= 10;
        }

        public bool isStringCorrect(string value)
        {
            var len = value.Length;
            return len > 0 && len < 50;
        }
    }

    public enum Categories
    {
        All,
        Electroniks,
        Books,
        Clothes,
        Toys,
        HomeAndGarden
    }
}
