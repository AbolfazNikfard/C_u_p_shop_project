using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Crops_Shop_Project.Models
{
    public class RegisterViewModel
    {
        
        [DisplayName("ایمیل")]
        [EmailAddress]           
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        [Remote("IsEmailInUse", "Account")]
        public string Email { get; set; }

        [DisplayName("رمز عبور")]
        [DataType(DataType.Password)]        
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        public string Password { get; set; }

        [DisplayName("تکرار رمز عبور")]
        [Compare(nameof(Password), ErrorMessage = "رمز عبور و تکرار رمز عبور مطابقت ندارند")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        public string ConfirmPassword { get; set; }
        //public bool role { get; set; }
    }
}
