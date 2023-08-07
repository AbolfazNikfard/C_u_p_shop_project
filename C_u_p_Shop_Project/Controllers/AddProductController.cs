using C_u_p_Shop_Project.Models.View_Models;
using C_u_p_Shop_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using C_u_p_Shop_Project.Data;
using Microsoft.AspNetCore.Identity;
using C_u_p_Shop_Project.Shared;

namespace C_u_p_Shop_Project.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AddProductController : Controller
    {
        private CropsShopContext _context;
        private readonly UserManager<User> _userManager;
        private const string BaseUrl = "assets/images/product-image";
        public AddProductController(CropsShopContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        #region AddProduct
        [HttpGet]
        public IActionResult addProduct(int? productId, string message)
        {
            try
            {
                if (message == null) { return NotFound(); }
                ViewData["Message"] = message;
                Product product;
                if (productId == null)
                    product = new Product();
                else
                {
                    product = _context.products.SingleOrDefault(p => p.id == productId);
                    if (product == null) { return NotFound(); }
                }
                var groupsAndSubGroups = _context.groups.Include(g => g.subGroups)
                    .Select(g => new GroupAndSubGroupViewModel
                    {
                        groupId = g.id,
                        groupName = g.Name,
                        subGroops = g.subGroups.ToList()
                    }).ToList();

                AddProductViewModel addProduct = new AddProductViewModel
                {
                    groupAndSubGroups = groupsAndSubGroups,
                    product = product

                };
                return View(addProduct);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Catched Error: {e.Message}");
                return StatusCode(500);
            }
        }
        [HttpPost]
        public IActionResult addProduct([FromForm] AddProductViewModel addProduct, string message)
        {
            ViewData["Message"] = message;
            try
            {
                var groupsAndSubGroups = _context.groups.Include(g => g.subGroups)
                        .Select(g => new GroupAndSubGroupViewModel
                        {
                            groupId = g.id,
                            groupName = g.Name,
                            subGroops = g.subGroups.ToList()
                        }).ToList();
                addProduct.groupAndSubGroups = groupsAndSubGroups;

                var subGroup = _context.subGroups.Where(s => s.id == addProduct.product.subGroupId).SingleOrDefault();
                int ParentGroupId = 0;
                if (subGroup != null) ParentGroupId = subGroup.parentGroupId;
                var group = _context.groups.SingleOrDefault(g => g.id == addProduct.product.groupId);

                #region FormValidation
                if (addProduct.product.Name == null)
                {
                    ModelState.AddModelError("", "لطفا نام محصول را وارد کنید");
                    return View(addProduct);
                }
                if (addProduct.productImage == null && addProduct.product.productImage == null)
                {
                    ModelState.AddModelError("", "لطفا برای محصول خود یک عکس آپلود کنید");
                    return View(addProduct);
                }
                if (addProduct.product.groupId != null && addProduct.product.subGroupId != null)
                {
                    if (group == null || subGroup == null)
                    {
                        ModelState.AddModelError("", " زیر گروه یا گروه موردنظر یافت نشد لطفا مجدد تلاش کنید ");
                        return View(addProduct);
                    }
                    if (ParentGroupId != addProduct.product.groupId)
                    {
                        ModelState.AddModelError("", "گروه و زیر گروه انتخاب شده تداخل دارند لطفا در انتخاب دقت نمایید");
                        return View(addProduct);
                    }
                }
                if (addProduct.product.groupId == null && addProduct.product.subGroupId != null)
                {
                    if (subGroup == null)
                    {
                        ModelState.AddModelError("", "زیر گروه یا گروه موردنظر یافت نشد لطفا مجدد تلاش کنید ");
                        return View(addProduct);
                    }
                    addProduct.product.groupId = ParentGroupId;
                }
                if (addProduct.product.Stock <= 0)
                {
                    ModelState.AddModelError("", "موجودی محصول باید بزرگ تر از 0 باشد");
                    return View(addProduct);
                }
                #endregion

                #region AddOrUpdateProduct                
                string ImageUrl = "";
                if ((ViewData["Message"].Equals("Edit") && addProduct.productImage != null) || addProduct.product.productImage == null)
                {
                    ImageUrl = saveImages.createImage(addProduct.productImage.FileName.ToString(), addProduct.productImage, BaseUrl);
                    addProduct.product.productImage = ImageUrl;
                }
                if (ViewData["Message"].Equals("Edit"))
                    _context.products.Update(addProduct.product);
                else if (ViewData["Message"].Equals("Add"))
                {
                    addProduct.product.registerDate = DateTime.Now;
                    _context.products.Add(addProduct.product);
                }

                _context.SaveChanges();
                #endregion                
                return RedirectToAction("ProductList", "AddProduct");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Catched Error: {e.Message}");
                return StatusCode(500);
            }
        }
        #endregion
        #region EditAndDeleteProduct
        public IActionResult EditProduct(int productId)
        {
            return RedirectToAction("addProduct", "AddProduct", new { productId = productId, message = "Edit" });
        }
        public IActionResult DeleteProduct(int productId)
        {
            try
            {
                var product = _context.products.SingleOrDefault(p => p.id == productId);
                if (product == null) { return NotFound(); }
                product.IsDelete = true;//soft delete
                _context.products.Update(product);
                _context.SaveChanges();
                return RedirectToAction("ProductList", "AddProduct");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Catched Error: {e.Message}");
                return StatusCode(500);
            }
        }
        #endregion
        public IActionResult ShowProduct(int productId)
        {
            try
            {
                var product = _context.products.IgnoreQueryFilters().SingleOrDefault(p => p.id == productId);
                if (product == null) { return NotFound(); }
                var comments = _context.comments.Where(c => c.productId == productId).ToList();
                ProductDetailViewModel model = new ProductDetailViewModel
                {
                    product = product,
                    comments = comments
                };
                return View(model);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Catched Error: {e.Message}");
                return StatusCode(500);
            }
        }
        public async Task<IActionResult> ProductList()
        {
            try
            {
                List<Product> productList;
                productList = _context.products.ToList();
                return View(productList);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Catched Error: {e.Message}");
                return StatusCode(500);
            }
        }
    }
}
