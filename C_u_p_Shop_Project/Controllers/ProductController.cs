using Crops_Shop_Project.Data;
using Crops_Shop_Project.Models;
using Crops_Shop_Project.Models.View_Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Crops_Shop_Project.Controllers
{
    public class ProductController : Controller
    {
        private CropsShopContext _context;
        private readonly UserManager<User> _userManager;
        public ProductController(CropsShopContext context,UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult ShowProductByGroupId(int id)
        {
            var products = _context.products.
                Where(p => p.groupId == id)
                .ToList();
            if (products == null) { return NotFound(); }
            var sendToView = new ShowProductByGroupComponentViewModel()
            {
                products = products,
                whichViewCallMe = "ShowProductByGroupId",
                id = id
            };
            return View(sendToView);
        }
        public IActionResult ShowProductBySubGroupId(int id)
        {
            var products = _context.products.
                Where(p => p.subGroupId == id)
                .ToList();
            if (products == null) { return NotFound(); }
            var sendToView = new ShowProductByGroupComponentViewModel()
            {
                products = products,
                whichViewCallMe = "ShowProductBySubGroupId",
                id = id
            };
            return View(sendToView);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ShowProductBySearchInput(string input)
        {
            if (input == null) { return NotFound(); }
            var products = _context.products
                .Where(p => p.Name.StartsWith(input))
                .ToList();
            var sendToView = new ShowProductByGroupComponentViewModel()
            {
                products = products,
                whichViewCallMe = "ShowProductBySearchInput",
                searchInput = input
            };
            return View(sendToView);
        }
        [HttpPost]
        public IActionResult ShowProductByFilter(List<Product> products,string filter)
        {
            //var products = _context.products;
            switch (filter)
            {
                case "Newest":
                    products.OrderBy(p => p.registerDate);
                    break;
                case "Oldest":
                    products.OrderByDescending(p => p.registerDate);
                    break;
                case "ExpensiveToCheap":
                    products.OrderByDescending(p => p.Price);
                    break;
                case "CheapToExpensive":
                    products.OrderBy(p => p.Price);
                    break;
                case "Alphabet":
                    products.OrderBy(p => p.Name);
                    break;
                case "ReverseAlphabet":
                    products.OrderByDescending(p => p.Name);
                    break;
            }
            var sendToView = new ShowProductByGroupComponentViewModel()
            {
                products = products,
                whichViewCallMe = "ShowProductByFilter",                
            };
            return View(sendToView);
        }
        public async Task<IActionResult> ProductDetails(int productId, string? addToCartMessage)
        {
            if (addToCartMessage == null)
                ViewData["Message"] = "";
            else
                ViewData["Message"] = addToCartMessage;
            var product = _context.products
                .Where(p => p.id == productId).SingleOrDefault();
            //var seller = _context.sellers.SingleOrDefault(s=>s.id == product.sellerId);
            //var user = await _userManager.FindByIdAsync(seller.userId);
            if (product == null) { return NotFound(); }
            var comments = _context.comments.Where(c=>c.productId == productId).ToList();
            ProductDetailViewModel productDetailViewModel = new ProductDetailViewModel
            {
                product = product,
                comments = comments,
                //seller = user.UserName
            };            
            return View(productDetailViewModel);
        }       
    }
}
