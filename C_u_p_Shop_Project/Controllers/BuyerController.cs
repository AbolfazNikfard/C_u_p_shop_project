using C_u_p_Shop_Project.Convertor;
using C_u_p_Shop_Project.Data;
using C_u_p_Shop_Project.Models;
using C_u_p_Shop_Project.Models.View_Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace C_u_p_Shop_Project.Controllers
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
            try
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (user == null) { return NotFound(); }

                var buyer = _context.buyers.SingleOrDefault(b => b.userId == user.Id);
                if (buyer == null) { return NotFound(); }

                return View(buyer);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Catched Error: {e.Message}");
                return StatusCode(500);
            }
        }
        public async Task<IActionResult> Order()
        {
            try
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
                        Price = o.Price,
                        Number = o.Number,
                        orderDate = o.orderDateTime.ToShamsi()
                    })
                    .ToList();
                return View(buyerOrders);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Catched Error: {e.Message}");
                return StatusCode(500);
            }
        }
        public async Task<IActionResult> Favorites()
        {
            try
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (user == null) { return NotFound(); }

                var buyer = _context.buyers.SingleOrDefault(b => b.userId == user.Id);
                if (buyer == null) { return NotFound(); }

                var favorites = _context.favorites.Where(f => f.buyerId == buyer.id)
                    .Include(f => f.product)
                    .Select(f => new Product
                    {
                        id = f.product.id,
                        Name = f.product.Name,
                        productImage = f.product.productImage,
                        Price = f.product.Price,
                        Stock = f.product.Stock
                    }).ToList();
                return View(favorites);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Catched Error: {e.Message}");
                return StatusCode(500);
            }
        }
        public async Task<IActionResult> AddItemToFavorites(int itemId)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (user == null) return NotFound();
                var buyer = _context.buyers.SingleOrDefault(b => b.userId == user.Id);
                if (buyer == null) return NotFound();
                var ExistItem = _context.favorites.Where(f => f.productId == itemId && f.buyerId == buyer.id).SingleOrDefault();
                if (ExistItem == null)
                {
                    Favorite newItem = new Favorite
                    {
                        productId = itemId,
                        buyerId = buyer.id
                    };
                    _context.favorites.Add(newItem);
                    _context.SaveChanges();
                }
                return RedirectToAction("Favorites", "Buyer");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Catched Error: {e.Message}");
                return StatusCode(500);
            }
        }
        public async Task<IActionResult> DeleteItemFromFavorites(int itemId)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (user == null) return NotFound();
                var buyer = _context.buyers.SingleOrDefault(b => b.userId == user.Id);
                if (buyer == null) return NotFound();

                var favorite = _context.favorites.Where(f => f.buyerId == buyer.id && f.productId == itemId).SingleOrDefault();
                if (favorite == null) return NotFound();
                _context.favorites.Remove(favorite);
                var result = _context.SaveChanges();
                if (result == 1)
                    return RedirectToAction("Favorites", "Buyer");
                else return StatusCode(404);
            }
            catch (Exception e)
            {
                Console.WriteLine("catched Internal Server Error: " + e.Message);
                return StatusCode(500);
            }
        }
    }
}
