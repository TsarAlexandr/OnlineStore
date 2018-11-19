using System.ComponentModel.DataAnnotations;


namespace WebDev_MainLab.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "UserRequired")]
        [Display(Name = "User")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "PassRequired")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember")]
        public bool RememberMe { get; set; }
    }
}
