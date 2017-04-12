using System.ComponentModel.DataAnnotations;

namespace CRM.WebUI.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required]
        //[EmailAddress]
        [StringLength (20)]
        [Display(Name = "User Name")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
