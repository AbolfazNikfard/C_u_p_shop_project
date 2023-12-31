﻿using C_u_p_Shop_Project.Data;
using C_u_p_Shop_Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace C_u_p_Shop_Project.Components
{
    public class ShowProductByGroupComponent : ViewComponent
    {
        private CropsShopContext _context;
        public ShowProductByGroupComponent(CropsShopContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(List<Product> products)
        {
            return View("~/Views/Component/ShowProductByGroup.cshtml", products);
        }
    }
}
