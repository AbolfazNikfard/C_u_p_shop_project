using Crops_Shop_Project.Enum;
using Crops_Shop_Project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Crops_Shop_Project.Data
{
    public partial class CropsShopContext
    {
        internal void AddModelCreatingConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasQueryFilter(p => p.IsDelete == false); //&& p.confirmation == AcceptProduct.Accept);
            // modelBuilder.Entity<Seller>().HasQueryFilter(s => s.IsDelete == false);
            modelBuilder.Entity<Buyer>().HasQueryFilter(b => b.IsDelete == false);
            modelBuilder.Entity<Group>().HasQueryFilter(g => g.IsDelete == false);
            modelBuilder.Entity<SubGroup>().HasQueryFilter(sg => sg.IsDelete == false);            
        }
    }
}
