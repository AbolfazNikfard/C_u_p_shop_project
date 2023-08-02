using Crops_Shop_Project.Data;
using Crops_Shop_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Crops_Shop_Project.Components
{
    public class ShowGroups:ViewComponent
    {
        private CropsShopContext _context;
        public ShowGroups(CropsShopContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(string whichView)
        {
            var groups = _context.groups.Include(sg => sg.subGroups).Select(g =>
            new GroupAndSubGroupViewModel()
            { 
                groupId = g.id,
                groupName = g.Name,
                subGroops = g.subGroups.ToList()
            }).ToList();
            var viewAddress = "~/Views/Component/ShowGroups.cshtml";
            if (whichView == "Responsive")
                viewAddress = "~/Views/Component/ShowGroupsResponsive.cshtml";
            else if(whichView == "Sidebar")
                viewAddress = "~/Views/Component/SidebarGroupBlock.cshtml";
            return View(viewAddress, groups);
        }
        //public async Task<IViewComponentResult> 
    }
}
