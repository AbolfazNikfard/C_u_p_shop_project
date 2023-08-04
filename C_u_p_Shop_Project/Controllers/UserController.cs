using Crops_Shop_Project.Models;
using Crops_Shop_Project.Models.View_Models;
using Crops_Shop_Project.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.DotNet.Scaffolding.Shared.Project;

namespace Crops_Shop_Project.Controllers
{
    [Authorize(Roles = "Buyer")] //[Authorize(Roles = "Seller,Buyer")]
    public class UserController : Controller
    {
        private UserManager<User> _userManager;
        private const string BaseUrl = "assets/images/user-image";
        public UserController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        #region ProfileAndUpdateIt
        public async Task<IActionResult> Profile(string? message)
        {
            if (message == "Buy")
                ViewData["BuyMessage"] = message;
            else if (message == "AddProduct")
                ViewData["AddProductMessage"] = message;
            else
            {
                ViewData["BuyMessage"] = "";
                ViewData["AddProductMessage"] = "";
            }
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            if (user == null) { return NotFound(); }

            UserViewModel userModel = new UserViewModel
            {
                user = user
            };
            if (User.IsInRole("Buyer"))
                return View("~/Views/Buyer/Profile.cshtml", userModel);
            else
                return View("~/Views/Seller/Profile.cshtml", userModel);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(UserViewModel updateUser, string? message)
        {
            try
            {
                if (message == "Buy")
                    ViewData["BuyMessage"] = message;
                //else if (message == "AddProduct")
                //    ViewData["AddProductMessage"] = message;
                else
                {
                    ViewData["BuyMessage"] = "";
                    //ViewData["AddProductMessage"] = "";
                }
                #region Validation
                bool isNumber;
                isNumber = long.TryParse(updateUser.user.PhoneNumber, out long phone);
                long number = phone;
                int count = 0;
                while (number > 0)
                {
                    number = number / 10;
                    count++;
                }
                if (updateUser.user.Name == null)
                {
                    ModelState.AddModelError("", "لطفا نام را وارد کنید");
                    return View("~/Views/Buyer/Profile.cshtml", updateUser);
                }
                if (updateUser.user.Family == null)
                {
                    ModelState.AddModelError("", "لطفا نام خانوادگی را وارد کنید");
                    return View("~/Views/Buyer/Profile.cshtml", updateUser);
                }
                if (updateUser.user.Address == null)
                {
                    ModelState.AddModelError("", "لطفا آدرس را وارد کنید");
                    return View("~/Views/Buyer/Profile.cshtml", updateUser);
                }
                if (updateUser.user.PhoneNumber == null)
                {
                    ModelState.AddModelError("", "لطفا شماره تماس را وارد کنید");
                    return View("~/Views/Buyer/Profile.cshtml", updateUser);
                }
                if (isNumber == false || count != 10)
                {
                    ModelState.AddModelError("", "شماره تماس معتبر نیست");
                    return View("~/Views/Buyer/Profile.cshtml", updateUser);
                }
                if (phone == 0)
                {
                    ModelState.AddModelError("", "شماره تماس را بدون صفر اول وارد کنید");
                    return View("~/Views/Buyer/Profile.cshtml", updateUser);
                }
                #endregion
                var user = await _userManager.FindByEmailAsync(User.Identity.Name);
                if (user == null) { return NotFound(); }

                user.Name = updateUser.user.Name;
                user.Family = updateUser.user.Family;
                user.Address = updateUser.user.Address;
                user.PhoneNumber = updateUser.user.PhoneNumber;

                if (updateUser.userImage != null)
                {
                    string userImageUrl = saveImages.createImage(updateUser.userImage.FileName.ToString(), updateUser.userImage, BaseUrl);
                    user.userImage = userImageUrl;
                }

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    if (message == "Buy")
                        return RedirectToAction("userCart", "Buy");
                    else
                        return RedirectToAction("Index", "Buyer");
                }
                else
                {
                    ModelState.AddModelError("", "مشکلی در سمت سرور پیش آمده است لطفا بعدا مجددا تلاش کنید");
                    return View("~/Views/Buyer/Profile.cshtml", updateUser);
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "مشکلی در سمت سرور پیش آمده است لطفا بعدا مجددا تلاش کنید");
                return View("~/Views/Buyer/Profile.cshtml", updateUser);
            }
        }
        #endregion
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View("~/Views/Buyer/ChangePass.cshtml");
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByEmailAsync(User.Identity.Name);
                    bool checkOldPass = await _userManager.CheckPasswordAsync(user, model.oldPassword);
                    if (checkOldPass == false)
                    {
                        ModelState.AddModelError("", "رمز عبور قدیمی اشتباه است");

                        return View("~/Views/Buyer/ChangePass.cshtml", model);

                    }
                    var result = await _userManager.ChangePasswordAsync(user, model.oldPassword, model.newPassword);
                    if (result.Succeeded)
                        return RedirectToAction("Index", "Buyer");
                    else
                    {
                        ModelState.AddModelError("", "مشکلی در سمت سرور پیش آمده است لطفا بعدا مجددا تلاش کنید");
                        return View("~/Views/Buyer/ChangePass.cshtml", model);
                    }
                }
                else
                    return View("~/Views/Buyer/ChangePass.cshtml", model);

            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "مشکلی در سمت سرور پیش آمده است لطفا بعدا مجددا تلاش کنید");
                return View("~/Views/Buyer/ChangePass.cshtml", model);
            }
        }
    }
}
