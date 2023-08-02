using Crops_Shop_Project.Models;
using Crops_Shop_Project.Models.View_Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.DotNet.Scaffolding.Shared.Project;

namespace Crops_Shop_Project.Controllers
{
    [Authorize(Roles = "Seller,Buyer")]
    public class UserController : Controller
    {
        private UserManager<User> _userManager;
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
                Name = user.Name,
                Family = user.Family,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber
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
                else if (message == "AddProduct")
                    ViewData["AddProductMessage"] = message;
                else
                {
                    ViewData["BuyMessage"] = "";
                    ViewData["AddProductMessage"] = "";
                }
                #region Validation
                bool isNumber;
                isNumber = long.TryParse(updateUser.PhoneNumber, out long phone);
                long number = phone;
                int count = 0;
                while (number > 0)
                {
                    number = number / 10;
                    count++;
                }
                if (updateUser.Name == null)
                {
                    ModelState.AddModelError("", "لطفا نام را وارد کنید");
                    if (User.IsInRole("Buyer"))
                        return View("~/Views/Buyer/Profile.cshtml", updateUser);
                    else
                        return View("~/Views/Seller/Profile.cshtml", updateUser);
                }
                if (updateUser.Family == null)
                {
                    ModelState.AddModelError("", "لطفا نام خانوادگی را وارد کنید");
                    if (User.IsInRole("Buyer"))
                        return View("~/Views/Buyer/Profile.cshtml", updateUser);
                    else
                        return View("~/Views/Seller/Profile.cshtml", updateUser);
                }
                if (updateUser.Address == null)
                {
                    ModelState.AddModelError("", "لطفا آدرس را وارد کنید");
                    if (User.IsInRole("Buyer"))
                        return View("~/Views/Buyer/Profile.cshtml", updateUser);
                    else
                        return View("~/Views/Seller/Profile.cshtml", updateUser);
                }
                if (updateUser.PhoneNumber == null)
                {
                    ModelState.AddModelError("", "لطفا شماره تماس را وارد کنید");
                    if (User.IsInRole("Buyer"))
                        return View("~/Views/Buyer/Profile.cshtml", updateUser);
                    else
                        return View("~/Views/Seller/Profile.cshtml", updateUser);
                }
                if (isNumber == false || count != 10)
                {
                    ModelState.AddModelError("", "شماره تماس معتبر نیست");
                    if (User.IsInRole("Buyer"))
                        return View("~/Views/Buyer/Profile.cshtml", updateUser);
                    else
                        return View("~/Views/Seller/Profile.cshtml", updateUser);
                }
                if (phone == 0)
                {
                    ModelState.AddModelError("", "شماره تماس را بدون صفر اول وارد کنید");
                    if (User.IsInRole("Buyer"))
                        return View("~/Views/Buyer/Profile.cshtml", updateUser);
                    else
                        return View("~/Views/Seller/Profile.cshtml", updateUser);
                }
                #endregion
                var user = await _userManager.FindByEmailAsync(User.Identity.Name);
                if (user == null) { return NotFound(); }
                user.Name = updateUser.Name;
                user.Family = updateUser.Family;
                user.Address = updateUser.Address;
                user.PhoneNumber = updateUser.PhoneNumber;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    if (User.IsInRole("Buyer"))
                        if (ViewData["BuyMessage"] == "Buy")
                            return RedirectToAction("userCart", "Buy");
                        else
                            return RedirectToAction("Index", "Buyer");
                    else
                        return RedirectToAction("Index", "Seller");
                }
                else
                {
                    ModelState.AddModelError("", "در ویرایش مشکل پیش آمده است ");
                    if (User.IsInRole("Buyer"))
                        return View("~/Views/Buyer/Profile.cshtml", updateUser);
                    else
                        return View("~/Views/Seller/Profile.cshtml", updateUser);
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", $"مشکل {e} در ویرایش مشکل پیش آمده است");
                if (User.IsInRole("Buyer"))
                    return View("~/Views/Buyer/Profile.cshtml", updateUser);
                else
                    return View("~/Views/Seller/Profile.cshtml", updateUser);
            }
        }
        #endregion
        [HttpGet]
        public IActionResult ChangePassword()
        {
            if (User.IsInRole("Buyer"))
                return View("~/Views/Buyer/ChangePass.cshtml");
            else
                return View("~/Views/Seller/ChangePass.cshtml");
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
                        if (User.IsInRole("Buyer"))
                            return View("~/Views/Buyer/ChangePass.cshtml", model);
                        else
                            return View("~/Views/Seller/ChangePass.cshtml", model);
                    }
                    var result = await _userManager.ChangePasswordAsync(user, model.oldPassword, model.newPassword);
                    if (result.Succeeded)
                    {
                        if (User.IsInRole("Buyer"))
                            return RedirectToAction("Index", "Buyer");
                        else
                            return RedirectToAction("Index", "Seller");
                    }
                    else
                    {
                        ModelState.AddModelError("", "عملیات با شکست مواجه شد لطفا دوباره امتحان کنید");
                        if (User.IsInRole("Buyer"))
                            return View("~/Views/Buyer/ChangePass.cshtml", model);
                        else
                            return View("~/Views/Seller/ChangePass.cshtml", model);
                    }
                }
                else
                {
                    if (User.IsInRole("Buyer"))
                        return View("~/Views/Buyer/ChangePass.cshtml", model);
                    else
                        return View("~/Views/Seller/ChangePass.cshtml", model);
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", $"مشکل {e} در به روزرسانی مشکل پیش آمده است");
                if (User.IsInRole("Buyer"))
                    return View("~/Views/Buyer/ChangePass.cshtml", model);
                else
                    return View("~/Views/Seller/ChangePass.cshtml", model);
            }
        }
    }
}
