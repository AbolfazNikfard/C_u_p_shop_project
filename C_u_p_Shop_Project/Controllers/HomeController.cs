using Crops_Shop_Project.Data;
using Crops_Shop_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Crops_Shop_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private CropsShopContext _context;
        public HomeController(ILogger<HomeController> logger, CropsShopContext context)
        {
            _logger = logger;
            _context= context;
        }

        public IActionResult Index(string? addToCartMessage)
        {
            ViewData["Message"] = addToCartMessage;            
            var products = _context.products.
                 /*Take(8).*/ToList();
                 Console.WriteLine($"Products: {products}");
            var sendToView = new ShowProductByGroupComponentViewModel()
            {
                products = products,
                whichViewCallMe = "Index"
            };
            return View(sendToView);
        }
        public IActionResult ContactUs()
        {
            return View();
        }
        public IActionResult RulesAndConditions()
        {
            return View();
        }
        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult Questions()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}