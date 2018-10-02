using System;
using System.ComponentModel.DataAnnotations;


namespace WebDev_MainLab.Models
{
    public class CreditCard
    {
        private string _number;
        [Required]
        public string CradNumber {
            get
                { return _number; }
            set
                {
                if (isValidCardNumber(value))
                    _number = value;
            }
        }
        [Required]
        public string OwnerName { get; set; }

        [Required]
        public string OwnerSurname { get; set; }

        [Required]
        public DateTime validDate { get; set; }

        [Required]
        public int Code { get; set; }

        private bool isValidCardNumber(string value)
        {
            if (value.Length == 16)
            {

            }
            return false;
        }
    }
}
