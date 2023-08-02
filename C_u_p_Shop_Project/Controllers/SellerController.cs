using Crops_Shop_Project.Convertor;
using Crops_Shop_Project.Data;
using Crops_Shop_Project.Models;
using Crops_Shop_Project.Models.View_Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Crops_Shop_Project.Controllers
{
    [Authorize(Roles = "Seller")]
    public class SellerController : Controller
    {
        private CropsShopContext _context;
        private readonly UserManager<User> _userManager;
        private const string BaseUrl = "assets/images/product-image";
        public SellerController(CropsShopContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
