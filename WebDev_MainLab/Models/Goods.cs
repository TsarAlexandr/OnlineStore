using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace WebDev_MainLab.Models
{
    public class Goods
    {
        private int _rating;
        private string _name, _description, _price;


        public int ID { get; set; }


        [Required(ErrorMessage = "RatingRequired")]
        [Display(Name = "Rating")]
        [Range(1, 10, ErrorMessage = "RatingRange")]
        public int Rating
        {
            get { return _rating; }
            set
            {
                if (isRatingInRange(value))
                    _rating = value;
            }
        }
        [Required(ErrorMessage = "NameRequired")]
        [Display(Name = "Name")]
        [StringLength(50, ErrorMessage = "NameLength")]
        public string Name
        {
            get { return _name; }
            set
            {
                if (isStringCorrect(value))
                    _name = value;
            }
        }

        [Required(ErrorMessage = "DescriptioneRequired")]
        [Display(Name = "Description")]
        [StringLength(50, ErrorMessage = "DescriptionLength")]
        public string Description
        {
            get { return _description; }
            set
            {
                if (isStringCorrect(value))
                    _description = value;
            }
        }
        [Required(ErrorMessage = "PriceRequired")]
        [Display(Name = "Price")]
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
        [Required(ErrorMessage = "CategoryRequired")]
        [Display(Name = "Category")]
        public Categories Category { get; set; }
        public List<Commentar> Comments { get; set; }
        public string AdditionalParameters { get; set; }

        public bool isPriceCorrect(string value)
        {
            var len = value.Length;
            double res;
            if (len < 8)
                if (Double.TryParse(value, NumberStyles.Any, new CultureInfo("ru"), out res))
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
        [Display(Name = "All")]
        All,
        [Display(Name = "Electronics")]
        Electronics,
        [Display(Name = "Books")]
        Books,
        [Display(Name = "Clothes")]
        Clothes,
        [Display(Name = "Toys")]
        Toys
    }
}
