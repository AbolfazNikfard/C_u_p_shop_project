﻿using C_u_p_Shop_Project.Data;
using C_u_p_Shop_Project.Enum;
using C_u_p_Shop_Project.Models;
using C_u_p_Shop_Project.Models.ApiModel;
using C_u_p_Shop_Project.Models.View_Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;

namespace C_u_p_Shop_Project.Controllers
{
    [Authorize(Roles = "Buyer")]
    public class BuyController : Controller
    {
        private CropsShopContext _context;
        private readonly UserManager<User> _userManager;
        public BuyController(CropsShopContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> addToCart([FromBody] addToCart model)
        {
            try
            {
                #region Validation OF number
                bool isNumber;
                isNumber = int.TryParse(model.number, out int quntity);
                if (isNumber == false)
                {
                    return this.BadRequest();
                }
                if (quntity >= 100)
                {
                    return Ok(new { message = "Too large" });
                }
                #endregion
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (user == null) { return NotFound(); }

                var buyer = _context.buyers.SingleOrDefault(b => b.userId == user.Id);
                if (buyer == null) { return NotFound(); }

                var product = _context.products.SingleOrDefault(p => p.id == model.productId);
                if (product == null) { return NotFound(); }

                if (product.Stock > 0)
                {
                    var cartItem = _context.carts.Where(r => r.productId == product.id && r.buyerId == buyer.id).SingleOrDefault();
                    #region Validation
                    if (quntity > product.Stock)
                        return Ok(new { message = "More than stock" });
                    #endregion
                    if (cartItem == null)
                    {
                        _context.carts.Add(new Cart
                        {
                            Number = quntity,
                            productId = product.id,
                            buyerId = buyer.id
                        });
                    }
                    else
                    {
                        if (cartItem.Number + quntity >= 6)
                            return Ok(new { message = "Too large" });
                        cartItem.Number += quntity;
                        _context.carts.Update(cartItem);

                    }
                    _context.SaveChanges();
                }
                else
                    return Ok(new { message = "Stock not enough" });

                return Ok(new { message = "Success" });
            }
            catch (Exception e)
            {
                Console.WriteLine($"catche error: {e.Message}");
                return StatusCode(500);
            }
        }
        public async Task<IActionResult> userCart()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null) { return NotFound(); }

            var buyer = _context.buyers.SingleOrDefault(b => b.userId == user.Id);
            if (buyer == null) { return NotFound(); }

            var userCart = _context.carts.Where(c => c.buyerId == buyer.id)
                .Include(p => p.product)
                .Select(i => new CartViewModel
                {
                    productId = i.productId,
                    productName = i.product.Name,
                    productImage = i.product.productImage,
                    productPrice = i.product.Price,
                    selectedNumberOfProducts = i.Number.ToString()
                }
                ).ToList();
            return View(userCart);
        }
        [HttpPost]
        public async Task<IActionResult> userCart([FromForm] List<CartViewModel> carts, bool forPurchase)
        {
            try
            {
                var productIdList = carts.Select(c => c.productId).ToList();
                var products = _context.products.Where(p => productIdList.Contains(p.id)).ToList();
                var sortCarts = carts.OrderBy(c => c.productId).ToList();
                #region Validation
                bool isNumber;
                int result;
                for (int i = 0; i < sortCarts.Count; i++)
                {
                    isNumber = int.TryParse(carts[i].selectedNumberOfProducts, out result);
                    if (isNumber == false)
                    {
                        ModelState.AddModelError("", "تعداد محصول وارد شده معتبر نیست");
                        return View(carts);
                    }
                    if (result > products[i].Stock)
                    {
                        ModelState.AddModelError("", "موجودی محصول با شناسه " + products[i].id + " کافی نیست");
                        return View(carts);
                    }
                    if (result > 5)
                    {
                        ModelState.AddModelError("", "تعداد محصول وارد شده با شناسه " + products[i].id + " بیش از حد مجاز است");
                        return View(carts);
                    }
                }
                #endregion
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (user == null) { return NotFound(); }

                var buyer = _context.buyers.SingleOrDefault(b => b.userId == user.Id);
                if (buyer == null) { return NotFound(); }

                var userCart = _context.carts.Where(c => c.buyerId == buyer.id).ToList();
                for (int i = 0; i < carts.Count; i++)
                {
                    for (int j = 0; j < userCart.Count; j++)
                    {
                        if (userCart[j].productId == carts[i].productId)
                        {
                            userCart[j].Number = int.Parse(carts[i].selectedNumberOfProducts);
                            break;
                        }
                    }

                }
                _context.carts.UpdateRange(userCart);
                _context.SaveChanges();
                if (forPurchase == true)
                    return RedirectToAction("CompletePurchaseAndPayment", "Buy");
                return View(carts);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Catched Error: {e.Message}");
                return StatusCode(500);
            }
        }
        public async Task<IActionResult> CompletePurchaseAndPayment()
        {
            try
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (user == null) { return NotFound(); }

                var buyer = _context.buyers.SingleOrDefault(b => b.userId == user.Id);
                if (buyer == null) { return NotFound(); }

                if (user.PhoneNumber == null || user.Address == null)
                    return RedirectToAction("Profile", "User", new { message = "Buy" });

                var buyerCart = _context.carts.Where(c => c.buyerId == buyer.id)
                    .Include(c => c.product)
                    .ToList();

                List<Order> orders = new List<Order>();
                for (int i = 0; i < buyerCart.Count; i++)
                {
                    orders.Add(new Order
                    {
                        buyerId = buyer.id,
                        productId = buyerCart[i].productId,
                        Price = buyerCart[i].product.Price,
                        Number = buyerCart[i].Number,
                        orderDateTime = DateTime.Now,
                    });
                }
                List<Product> products = new List<Product>();
                var buyerCartProductIdlist = buyerCart.Select(c => c.productId).ToList();
                products = _context.products.Where(p => buyerCartProductIdlist.Contains(p.id)).ToList();
                var sortBuyerCart = buyerCart.OrderBy(c => c.productId).ToList();
                for (int i = 0; i < sortBuyerCart.Count; i++)
                {
                    products[i].Stock -= sortBuyerCart[i].Number; //* products[i].Weight);
                }
                _context.products.UpdateRange(products);
                _context.orders.AddRange(orders);
                _context.SaveChangesAsync();
                return RedirectToAction("Factor");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Catched Error: {e.Message}");
                return StatusCode(500);
            }
        }
        public async Task<IActionResult> Factor()
        {
            try
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (user == null) { return NotFound(); }

                var buyer = _context.buyers.SingleOrDefault(b => b.userId == user.Id);
                if (buyer == null) { return NotFound(); }

                var userCart = _context.carts.Where(c => c.buyerId == buyer.id).ToList();
                var buyerCart = _context.carts.Where(c => c.buyerId == buyer.id)
                    .Include(p => p.product)
                    .Select(i => new CartViewModel
                    {
                        productId = i.productId,
                        productName = i.product.Name,
                        productPrice = i.product.Price,
                        selectedNumberOfProducts = i.Number.ToString()
                    }
                    ).ToList();
                var factor = new FactorViewModel
                {
                    Address = user.Address,
                    Telphone = user.PhoneNumber,
                    cartItems = buyerCart
                };
                _context.RemoveRange(userCart);
                _context.SaveChanges();
                return View(factor);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Catched Error: {e.Message}");
                return StatusCode(500);
            }
        }
        public async Task<IActionResult> deleteItemFromCart(int productId)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (user == null) { return NotFound(); }

                var buyer = _context.buyers.SingleOrDefault(b => b.userId == user.Id);
                if (buyer == null) { return NotFound(); }

                var deleteItem = _context.carts.Where(c => c.productId == productId && c.buyerId == buyer.id).SingleOrDefault();
                if (deleteItem == null) { return NotFound(); }

                _context.carts.Remove(deleteItem);
                _context.SaveChanges();
                return RedirectToAction("userCart", "Buy");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Catched Error: {e.Message}");
                return StatusCode(500);
            }
        }
    }
}
