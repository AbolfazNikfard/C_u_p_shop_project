using Crops_Shop_Project.Convertor;
using Crops_Shop_Project.Data;
using Crops_Shop_Project.Models;
using Crops_Shop_Project.Models.View_Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;

namespace Crops_Shop_Project.Controllers
{
    [Authorize(Roles = "Buyer")]
    public class BuyerController : Controller
    {
        private CropsShopContext _context;
        private UserManager<User> _userManager;
        public BuyerController(CropsShopContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null) { return NotFound(); }

            var buyer = _context.buyers.SingleOrDefault(b => b.userId == user.Id);
            if (buyer == null) { return NotFound(); }

            return View(buyer);
        }
        public async Task<IActionResult> Order()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null) { return NotFound(); }

            var buyer = _context.buyers.SingleOrDefault(b => b.userId == user.Id);
            if (buyer == null) { return NotFound(); }

            var buyerOrders = _context.orders.Where(o => o.buyerId == buyer.id).Include(p => p.product).IgnoreQueryFilters()
                .Select(o => new OrderViewModel
                {
                    productId = o.productId,
                    productImage = o.product.productImage,
                    productName = o.product.Name,
                    // Weight = o.Weight,
                    // WeightMassUnit = o.WeightMassUnit,
                    Price = o.Price,
                    Number = o.Number,
                    orderDate = o.orderDateTime.ToShamsi()
                })
                .ToList();
            return View(buyerOrders);
        }      
    }
}
