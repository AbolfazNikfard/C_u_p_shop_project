﻿using C_u_p_Shop_Project.Data;
using C_u_p_Shop_Project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace C_u_p_Shop_Project.Components
{
    public class numberOFUserCartItemComponent : ViewComponent
    {
        private CropsShopContext _context;
        private UserManager<User> _userManager;
        public numberOFUserCartItemComponent(CropsShopContext context, UserManager<User> userManage)
        {
            _context = context;
            _userManager = userManage;
        }
        public async Task<IViewComponentResult> InvokeAsync(string ForWhere)
        {
            string viewAdress = "~/Views/Component/numberOfUserCartItem.cshtml";
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
            {
                ViewData["NumberOfCartItem"] = 0;
                ViewData["ForWhere"] = ForWhere;
                return View(viewAdress);
            }
            var buyer = _context.buyers.SingleOrDefault(b => b.userId == user.Id);
            if (buyer == null)
            {
                ViewData["NumberOfCartItem"] = 0;
                ViewData["ForWhere"] = ForWhere;
                return View(viewAdress);
            }
            int userCart = _context.carts.Where(c => c.buyerId == buyer.id).Select(c => c.Number).Sum();
            ViewData["NumberOfCartItem"] = userCart;
            ViewData["ForWhere"] = ForWhere;
            return View(viewAdress);
        }
    }
}
