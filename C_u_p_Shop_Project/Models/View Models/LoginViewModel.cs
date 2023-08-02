using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Crops_Shop_Project.Models
{
    public class LoginViewModel
    {
        [DisplayName("ایمیل")]
        [EmailAddress]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DisplayName ("رمز عبور")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
