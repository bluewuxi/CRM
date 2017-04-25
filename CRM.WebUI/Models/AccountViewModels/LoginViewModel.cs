using System.ComponentModel.DataAnnotations;

namespace CRM.WebUI.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "User Name")]
        [Required(AllowEmptyStrings = false)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
