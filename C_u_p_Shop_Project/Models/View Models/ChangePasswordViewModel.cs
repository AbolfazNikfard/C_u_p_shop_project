using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Crops_Shop_Project.Models.View_Models
{
    public class ChangePasswordViewModel
    {
        [DisplayName("رمز عبور قدیمی")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        public string oldPassword { get; set; }
        [DisplayName("رمز عبور جدید")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        public string newPassword { get; set; }
        [DisplayName("تکرار رمز عبور جدید")]
        [Compare(nameof(newPassword), ErrorMessage = "رمز عبور جدید و تکرار رمز عبور جدید مطابقت ندارند")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        public string confirmNewPassword { get; set; }
    }
}
