using C_u_p_Shop_Project.Data;
using C_u_p_Shop_Project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace C_u_p_Shop_Project.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private CropsShopContext _context;

        public AccountController(UserManager<User> userManager,
            SignInManager<User> signInManager
            , CropsShopContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }


        [HttpGet]
        public IActionResult Register()
        {
            if (_signInManager.IsSignedIn(User))
            {
                if (User.IsInRole("Buyer"))
                    return RedirectToAction("Index", "Home");
                else
                    return RedirectToAction("Index", "Admin");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegisterViewModel model)
        {
            try
            {
                if (_signInManager.IsSignedIn(User))
                {
                    if (User.IsInRole("Buyer"))
                        return RedirectToAction("Index", "Home");
                    else
                        return RedirectToAction("Index", "Admin");
                }
                if (ModelState.IsValid)
                {
                    var user = new User()
                    {
                        UserName = model.Email,
                        Email = model.Email,
                        EmailConfirmed = true
                    };

                    var createUserresult = await _userManager.CreateAsync(user, model.Password);

                    if (createUserresult.Succeeded)
                    {
                        var addUser = await _userManager.FindByNameAsync(model.Email);

                        Buyer buyer = new Buyer()
                        {
                            userId = addUser.Id
                        };
                        var addToRoleResult = await _userManager.AddToRoleAsync(user, "buyer");
                        if (!addToRoleResult.Succeeded)
                        {
                            ModelState.AddModelError("", "در ثبت نام مشکلی پیش آمده است لطفا مجددا امتحان کنید");
                            return View(model);
                        }
                        _context.buyers.Add(buyer);
                        _context.SaveChanges();
                        //}
                        return RedirectToAction("Login", "Account", new { message = "Success" });
                    }

                    foreach (var error in createUserresult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                }
                return View(model);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Catched Error: {e.Message}");
                return StatusCode(500);
            }
        }
        [HttpGet]
        public IActionResult Login(string? message, string? returnUrl)
        {
            if (_signInManager.IsSignedIn(User))
            {
                if (User.IsInRole("Buyer"))
                    return RedirectToAction("Index", "Home");
                else
                    return RedirectToAction("Index", "Admin");
            }

            ViewData["ReturnUrl"] = returnUrl;
            if (returnUrl != null && message == null)
                message = "OtherWhere";
            ViewData["Message"] = message;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginViewModel model, string? message, string? returnUrl)
        {
            try
            {
                if (_signInManager.IsSignedIn(User))
                {
                    if (User.IsInRole("Buyer"))
                        return RedirectToAction("Index", "Home");
                    else
                        return RedirectToAction("Index", "Admin");
                }
                ViewData["Message"] = message;
                ViewData["ReturnUrl"] = returnUrl;

                if (ModelState.IsValid)
                {
                    var isDeleteUser = await _userManager.FindByNameAsync(model.Email);
                    if (isDeleteUser == null || isDeleteUser.IsDelete == true)
                    {
                        ModelState.AddModelError("", "رمزعبور یا نام کاربری اشتباه است");
                        return View(model);
                    }
                    var result = await _signInManager.PasswordSignInAsync(
                        model.Email, model.Password, true, true);

                    if (result.Succeeded)
                    {
                        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                            return Redirect(returnUrl);
                        var user = await _userManager.FindByNameAsync(model.Email);
                        var roleName = await _userManager.GetRolesAsync(user);
                        if (roleName[0] == "Buyer")
                            return RedirectToAction("Index", "Home");
                        else
                            return RedirectToAction("Index", "Admin");
                    }

                    if (result.IsLockedOut)
                    {
                        ViewData["ErrorMessage"] = "اکانت شما به دلیل تلاش بیش از حد مجاز قفل شده است لطفا پس از مدتی دوباره تلاش نمایید";
                        return View(model);
                    }

                    ModelState.AddModelError("", "رمزعبور یا نام کاربری اشتباه است");
                }
                return View(model);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Catched Error: {e.Message}");
                return StatusCode(500);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Catched Error: {e.Message}");
                return StatusCode(500);
            }
        }

        public async Task<IActionResult> IsEmailInUse(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null) return Json(true);
                return Json("ایمیل وارد شده از قبل موجود است");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Catched Error: {e.Message}");
                return StatusCode(500);
            }
        }
    }
}
