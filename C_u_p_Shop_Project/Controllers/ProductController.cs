using Crops_Shop_Project.Data;
using Crops_Shop_Project.Models;
using Crops_Shop_Project.Models.View_Models;
using Crops_Shop_Project.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Crops_Shop_Project.Controllers
{
    public class ProductController : Controller
    {
        private CropsShopContext _context;
        private readonly UserManager<User> _userManager;
        public ProductController(CropsShopContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult ShowProductByGroupId(int groupId, int page = 1, string sort = null, string search = null)
        {
            try
            {
                if (page < 1)
                    return BadRequest(new { StatusCode = 400, message = "page number should be greater than 0" });
                int limit = 8;
                int skip = (page - 1) * limit;
                double productCount, result;

                IQueryable<Product> products;
                if (search != null)
                {
                    products = _context.products.Where(p => p.groupId == groupId && p.Name.StartsWith(search));
                    productCount = (double)_context.products.Where(p => p.groupId == groupId && p.Name.StartsWith(search)).Count();
                }
                else
                {
                    products = products = _context.products.Where(p => p.groupId == groupId);
                    productCount = (double)_context.products.Where(p => p.groupId == groupId).Count();
                }

                ViewData["page"] = page;
                result = productCount / (double)limit;
                int pageCount = (int)Math.Ceiling(result);
                ViewData["pagesCount"] = pageCount;

                List<Product> productViewModel;
                if (sort != null)
                    productViewModel = filter.sorted_Products(products, sort, skip, limit);
                else
                    productViewModel = products.Skip(skip).Take(limit).ToList();
             
                return View(productViewModel);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
        public IActionResult ShowProductBySubGroupId(int subGroupId, int page = 1, string sort = null, string search = null)
        {
            try
            {
                if (page < 1)
                    return BadRequest(new { StatusCode = 400, message = "page number should be greater than 0" });
                int limit = 8;
                int skip = (page - 1) * limit;
                double productCount, result;

                IQueryable<Product> products;
                if (search != null)
                {
                    products = _context.products.Where(p => p.subGroupId == subGroupId && p.Name.StartsWith(search));
                    productCount = (double)_context.products.Where(p => p.subGroupId == subGroupId && p.Name.StartsWith(search)).Count();
                }
                else
                {
                    products = products = _context.products.Where(p => p.subGroupId == subGroupId);
                    productCount = (double)_context.products.Where(p => p.subGroupId == subGroupId).Count();
                }

                ViewData["page"] = page;
                result = productCount / (double)limit;
                int pageCount = (int)Math.Ceiling(result);
                ViewData["pagesCount"] = pageCount;

                List<Product> productViewModel;
                if (sort != null)
                    productViewModel = filter.sorted_Products(products, sort, skip, limit);
                else
                    productViewModel = products.Skip(skip).Take(limit).ToList();

                return View(productViewModel);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
        public IActionResult ProductDetails(int productId, string? addToCartMessage)
        {
            try
            {
                if (addToCartMessage == null)
                    ViewData["Message"] = "";
                else
                    ViewData["Message"] = addToCartMessage;
                var product = _context.products.Where(p => p.id == productId).SingleOrDefault();
                if (product == null) { return NotFound(); }
                List<Comment> comments = _context.comments.Where(c => c.productId == productId).ToList();
                ProductDetailViewModel productDetailViewModel = new ProductDetailViewModel
                {
                    product = product,
                    comments = comments,
                };
                return View(productDetailViewModel);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
    }
}
