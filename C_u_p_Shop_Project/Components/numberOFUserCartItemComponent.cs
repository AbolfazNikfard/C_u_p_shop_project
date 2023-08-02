using Crops_Shop_Project.Data;
using Crops_Shop_Project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Crops_Shop_Project.Components
{
    public class numberOFUserCartItemComponent:ViewComponent
    {
        private CropsShopContext _context;
        private UserManager<User> _userManager;
        public numberOFUserCartItemComponent(CropsShopContext context,UserManager<User> userManage)
        {
            _context = context;
            _userManager = userManage;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var viewAdress = "~/Views/Component/numberOfUserCartItem.cshtml";
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null) { ViewData["NumberOfCartItem"] = 0; return View(viewAdress); }
            var buyer = _context.buyers.SingleOrDefault(b => b.userId == user.Id);
            if (buyer == null) { ViewData["NumberOfCartItem"] = 0; return View(viewAdress); }
            var userCart = _context.carts.Where(c => c.buyerId == buyer.id)
                .Select(c=>c.Number).Sum();
            ViewData["NumberOfCartItem"] = userCart;
            return View(viewAdress);
        }
    }
}
