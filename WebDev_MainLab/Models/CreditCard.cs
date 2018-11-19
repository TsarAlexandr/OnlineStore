using System;
using System.ComponentModel.DataAnnotations;


namespace WebDev_MainLab.Models
{
    public class CreditCard
    {
        private string _number = "";
        private int _code;
        
        [Required]
        public string CardNumber {
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
        [Range(1,12)]
        public int ValidDateMonth { get; set; }

        [Required]
        [Range(18, 28)]
        public int ValidDateYear { get; set; }

        [Required]
        public int Code
        {
            get
            { return _code; }
            set
            {
                if (isValidCode(value))
                    _code = value;
            }
        }

        private bool isValidCode(int value)
        {
            if (value.ToString().Length == 3)
                return true;
            return false;
        }
        private bool isValidCardNumber(string value)
        {
            if (value.Length == 16)
            {
                if(isNumberCorrespondsToLunaAlgorithm(value))
                    return true;
            }
            return false;
        }

        private bool isNumberCorrespondsToLunaAlgorithm(string value)
        {
            int[] number = new int[16];
            int sum = 0;
            for (int i = 0; i < 15; i += 2)
            {
                var doubleCurrentVal = 2 * Int32.Parse(value[i].ToString());
                number[i] = doubleCurrentVal > 9 ? (doubleCurrentVal - 9) : doubleCurrentVal; 
            }

            for (int i = 1; i < 16; i += 2)
            {
                number[i] = Int32.Parse(value[i].ToString());
            }

            foreach (var digit in number)
            {
                sum += digit;
            }
            return sum % 10 == 0;
        }
    }
}
