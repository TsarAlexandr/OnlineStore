using System.ComponentModel.DataAnnotations;


namespace WebDev_MainLab.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="UserRequired")]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "PasswordRequired")]
        [StringLength(100, ErrorMessage = "PasswordLength", MinimumLength = 6)]
        [DataType(DataType.Password, ErrorMessage = "PasswordType")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password, ErrorMessage = "PasswordType")]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Compare")]
        public string ConfirmPassword { get; set; }

        
    }
}
