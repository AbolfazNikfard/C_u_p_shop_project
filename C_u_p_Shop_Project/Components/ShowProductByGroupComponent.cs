using Crops_Shop_Project.Data;
using Crops_Shop_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore;

namespace Crops_Shop_Project.Components
{
    public class ShowProductByGroupComponent : ViewComponent
    {
        private CropsShopContext _context;
        public ShowProductByGroupComponent(CropsShopContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(ShowProductByGroupComponentViewModel objectVM)
        {
            var products = objectVM.products;
            if (objectVM.whichViewCallMe == "Index")
            {
                ViewData["whichViewCallMe"] = "Index";
            }
            else if (objectVM.whichViewCallMe == "ShowProductByGroupId")
            {
                ViewData["whichViewCallMe"] = "Group";
                ViewData["Id"] = objectVM.id;
            }
            else if (objectVM.whichViewCallMe == "ShowProductBySubGroupId")
            {
                ViewData["whichViewCallMe"] = "SubGroup";
                ViewData["Id"] = objectVM.id;
            }
            else if (objectVM.whichViewCallMe == "ShowProductBySearchInput")
            {
                ViewData["whichViewCallMe"] = "Search";
                ViewData["inputTxt"] = objectVM.searchInput;
            }
            return View("~/Views/Component/ShowProductByGroup.cshtml", products);
        }
    }
}
