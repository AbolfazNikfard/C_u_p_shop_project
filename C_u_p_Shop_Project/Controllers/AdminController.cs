using Crops_Shop_Project.Data;
using Crops_Shop_Project.Enum;
using Crops_Shop_Project.Models;
using Crops_Shop_Project.Models.View_Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Crops_Shop_Project.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private CropsShopContext _context;
        private readonly UserManager<User> _userManager;
        public AdminController(CropsShopContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        // public IActionResult PendingProducts()
        // {
        //     var products = _context.products.IgnoreQueryFilters().Where(p => p.IsDelete == false).ToList(); //&& p.confirmation == AcceptProduct.Pending).ToList();
        //     return View(products);
        // }
        // public IActionResult acceptProduct(int productId, bool Accept)
        // {
        //     try
        //     {
        //         var product = _context.products.IgnoreQueryFilters().SingleOrDefault(p => p.id == productId);
        //         if (product == null) { return NotFound(); }
        //         if (Accept == true)
        //             product.confirmation = AcceptProduct.Accept;
        //         else
        //             product.confirmation = AcceptProduct.Reject;
        //         _context.products.Update(product);
        //         _context.SaveChanges();
        //         return RedirectToAction("PendingProducts", "Admin");
        //     }
        //     catch (Exception e)
        //     {
        //         return NotFound();
        //     }
        // }
        // public IActionResult SellerList()
        // {
        //     var sellers = _context.sellers
        //         .Include(p => p.products).Include(s => s.orders).Include(u => u.user)
        //         .Select(s => new SellerListViewModel
        //         {
        //             Id = s.id,
        //             Name = s.user.Name,
        //             Family = s.user.Family,
        //             Email = s.user.Email,
        //             productsNumber = s.products.Count(),
        //             soldNumber = s.orders.Sum(o => o.Number)
        //         }).ToList();
        //     return View(sellers);
        // }
        public IActionResult BuyerList()
        {
            var buyers = _context.buyers
                .Include(o => o.orders).Include(u => u.user)
                .Select(b => new BuyerListViewModel
                {
                    Id = b.id,
                    Name = b.user.Name,
                    Family = b.user.Family,
                    Email = b.user.Email,
                    boughtNumber = b.orders.Sum(o => o.Number)
                }).ToList();
            return View(buyers);
        }
        #region EditUser
        [HttpGet]
        public async Task<IActionResult> EditUser(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null) { return NotFound(); }
            UserViewModel userModel = new UserViewModel
            {
                Name = user.Name,
                Family = user.Family,
                Email = user.Email,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber
            };
            return View(userModel);
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(UserViewModel updateUser)
        {
            try
            {
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
                    return View(updateUser);
                }
                if (updateUser.Family == null)
                {
                    ModelState.AddModelError("", "لطفا نام خانوادگی را وارد کنید");
                    return View(updateUser);
                }
                if (updateUser.Address == null)
                {
                    ModelState.AddModelError("", "لطفا آدرس را وارد کنید");
                    return View(updateUser);
                }
                if (updateUser.PhoneNumber == null)
                {
                    ModelState.AddModelError("", "لطفا شماره تماس را وارد کنید");
                    return View(updateUser);
                }
                if (isNumber == false || count != 10)
                {
                    ModelState.AddModelError("", "شماره تماس معتبر نیست");
                    return View(updateUser);
                }
                if (phone == 0)
                {
                    ModelState.AddModelError("", "شماره تماس را بدون صفر اول وارد کنید");
                    return View(updateUser);
                }
                #endregion
                var user = await _userManager.FindByEmailAsync(updateUser.Email);
                if (user == null) { return NotFound(); }
                user.Name = updateUser.Name;
                user.Family = updateUser.Family;
                user.Address = updateUser.Address;
                user.PhoneNumber = updateUser.PhoneNumber;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("Index", "Admin");
                else
                {
                    ModelState.AddModelError("", "در ویرایش مشکل پیش آمده است ");
                    return View(updateUser);
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", $"مشکل {e} در ثبت نام پیش آمده است");
                return View(updateUser);
            }
        }
        #endregion
        #region DeleteUser
        public async Task<IActionResult> DeleteUser(string userEmail)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(userEmail);
                if (user == null) { return NotFound(); }

                var roleName = await _userManager.GetRolesAsync(user);
                // if (roleName[0] == "Seller")
                // {
                //     var seller = _context.sellers.SingleOrDefault(s => s.userId == user.Id);
                //     if (seller == null) { return NotFound(); }
                //     seller.IsDelete = true;
                //     var products = _context.products.IgnoreQueryFilters().Where(p => p.IsDelete == false && p.sellerId == seller.id).ToList();
                //     foreach (var product in products)
                //     {
                //         product.IsDelete = true;
                //     }
                //     _context.sellers.Update(seller);
                //     _context.products.UpdateRange(products);
                // }
                if (roleName[0] == "Buyer")
                {
                    var buyer = _context.buyers.SingleOrDefault(b => b.userId == user.Id);
                    if (buyer == null) { return NotFound(); }
                    buyer.IsDelete = true;
                    _context.buyers.Update(buyer);
                }
                user.IsDelete = true;
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                    return NotFound();
                _context.SaveChanges();

            }
            catch (Exception e)
            {
                return NotFound();
            }
            return RedirectToAction("Index", "Admin");
        }
        #endregion
        #region Group
        public IActionResult addGroup()
        {
            var groupsWithSubgroups = _context.groups
                .Include(sg => sg.subGroups).Include(p => p.product)
                .Select(g => new GroupWithSubGroupsWithProductsViewModel
                {
                    groupId = g.id,
                    groupName = g.Name,
                    productNumber = g.product.Count(),
                    subGroups = g.subGroups.ToList()
                }).ToList();
            return View(groupsWithSubgroups);
        }
        [HttpPost]
        public IActionResult addGroup(string name)
        {
            try
            {
                if (name != null)
                {
                    Group newGroup = new Group
                    {
                        Name = name,
                    };
                    _context.groups.Add(newGroup);
                    _context.SaveChanges();

                }
                return RedirectToAction("addGroup", "Admin");
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }
        public IActionResult DeleteGroup(int groupId)
        {
            try
            {
                var group = _context.groups.SingleOrDefault(g => g.id == groupId);
                if (group == null) { return NotFound(); }
                group.IsDelete = true;
                _context.groups.Update(group);
                _context.SaveChanges();
                return RedirectToAction("addGroup", "Admin");
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }
        public IActionResult EditGroup(int groupId)
        {
            var group = _context.groups.SingleOrDefault(g => g.id == groupId);
            if (group == null) { return NotFound(); }
            return View(group);
        }
        [HttpPost]
        public IActionResult EditGroup(Group group)
        {
            try
            {
                if (group.Name == null)
                {
                    ModelState.AddModelError("", "لطفا نام گروه را وارد کنید");
                    return View(group);
                }
                _context.groups.Update(group);
                _context.SaveChanges();
                return RedirectToAction("addGroup", "Admin");
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }
        #endregion
        #region SubGroup
        public IActionResult addSubGroup()
        {
            var groups = _context.groups.ToList();
            var subGroups = _context.subGroups
                .Include(g => g.parentGroup).Include(p => p.product)
                .Select(sg => new SubGroup_With_It_ProductsNumber_ViewModel
                {
                    subgroupId = sg.id,
                    subgroupName = sg.Name,
                    productNumber = sg.product.Count(),
                    parentGroupName = sg.parentGroup.Name
                }).ToList();
            addSubgroupViewModel addSubgroupView = new addSubgroupViewModel
            {
                subgroups = subGroups,
                groups = groups
            };
            return View(addSubgroupView);
        }
        [HttpPost]
        public IActionResult addsubGroup(string name, int parentId)
        {
            try
            {
                if (name != null)
                {
                    SubGroup newsubGroup = new SubGroup
                    {
                        Name = name,
                        parentGroupId = parentId

                    };
                    _context.subGroups.Add(newsubGroup);
                    _context.SaveChanges();
                }
                return RedirectToAction("addSubGroup", "Admin");
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }
        public IActionResult EditSubGroup(int subGroupId)
        {
            var Groups = _context.groups.ToList();
            var subGroup = _context.subGroups.SingleOrDefault(sg => sg.id == subGroupId);
            if (subGroup == null) { return NotFound(); }
            EditSubGroupViewModel model = new EditSubGroupViewModel
            {
                subGroupId = subGroupId,
                subgroupName = subGroup.Name,
                parentGroupId = subGroup.parentGroupId,
                groups = Groups
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult EditSubGroup(EditSubGroupViewModel model)
        {
            try
            {
                if (model.subgroupName == null)
                {
                    ModelState.AddModelError("", "لطفا نام گروه را وارد کنید");
                    return View(model);
                }
                var subGroup = _context.subGroups.SingleOrDefault(sg => sg.id == model.subGroupId);
                if (subGroup == null) { return NotFound(); }
                subGroup.Name = model.subgroupName;
                subGroup.parentGroupId = model.parentGroupId;
                _context.subGroups.Update(subGroup);
                _context.SaveChanges();
                return RedirectToAction("addsubGroup", "Admin");
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }
        public IActionResult DeleteSubGroup(int subGroupId)
        {
            try
            {
                var subgroup = _context.subGroups.SingleOrDefault(sg => sg.id == subGroupId);
                if (subgroup == null) { return NotFound(); }
                subgroup.IsDelete = true;
                _context.subGroups.Update(subgroup);
                _context.SaveChanges();
                return RedirectToAction("addSubGroup", "Admin");
            }
            catch (Exception e) { return NotFound(); }
        }
        #endregion
    }
}
