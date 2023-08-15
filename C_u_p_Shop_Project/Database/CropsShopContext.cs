using C_u_p_Shop_Project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace C_u_p_Shop_Project.Data
{
    public partial class CropsShopContext : IdentityDbContext<User, IdentityRole, string>
    {
        public CropsShopContext(DbContextOptions<CropsShopContext> options) : base(options)
        {

        }
        public DbSet<Product> products { get; set; }
        public DbSet<Group> groups { get; set; }

        public DbSet<Buyer> buyers { get; set; }
        public DbSet<SubGroup> subGroups { get; set; }
        public DbSet<Cart> carts { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<Comment> comments { get; set; }
        public DbSet<Favorite> favorites { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            AddModelCreatingConfiguration(modelBuilder);
            #region AddNameAndFamilyToIdentityUserTabel
            modelBuilder.Ignore<IdentityUserLogin<string>>();
            modelBuilder.Ignore<IdentityUserRole<string>>();
            modelBuilder.Ignore<IdentityUserClaim<string>>();
            modelBuilder.Ignore<IdentityUserToken<string>>();
            modelBuilder.Ignore<IdentityUser<string>>();
            modelBuilder.Ignore<User>();

            #endregion
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
            .HasOne(b => b.buyer)
            .WithOne(i => i.user)
            .HasForeignKey<Buyer>(b => b.userId);
            modelBuilder.Entity<Favorite>()
                .HasKey(f => new { f.buyerId, f.productId });
           // modelBuilder.Entity<User>()
           //.HasOne(b => b.seller)
           //.WithOne(i => i.user)
           //.HasForeignKey<Seller>(b => b.userId);
            //modelBuilder.Entity<IdentityRole>().HasData(
            //    new IdentityRole { Id = "e1737af7-cabd-4ba2-8385-49b1b1fea747", Name = "Buyer", NormalizedName = "BUYER" },
            //    new IdentityRole { Id = "51be3cb7-179c-409e-8171-3ade8ef31752", Name = "Seller", NormalizedName = "SELLER" },
            //    new IdentityRole { Id = "038d3a7b-1c0e-4f85-8b13-b8bbc950cdd9", Name = "Admin", NormalizedName = "ADMIN" }
            //    );
            //modelBuilder.Entity<User>().HasData(
            //    new User
            //    {
            //        Id = "964d3f27-e4ab-423a-a180-cc1b1a49822f",
            //        UserName = "Admin@gmail.com",
            //        NormalizedUserName = "ADMIN@GMAIL.COM",
            //        Email = "Admin@gmail.com",
            //        NormalizedEmail = "ADMIN@GMAIL.COM",
            //        EmailConfirmed = true,
            //        PasswordHash = "AQAAAAEAACcQAAAAEJqkDoDJDB/xYgjraFaXbCPDBo3wg7sK3ZKvEd445wQU+5W2nuQlfTcZ6mN818zkPw=="
            //        //Password : Admin@1
            //    });

            //modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            //    new IdentityUserRole<string>
            //    {
            //        UserId = "964d3f27-e4ab-423a-a180-cc1b1a49822f",
            //        RoleId = "038d3a7b-1c0e-4f85-8b13-b8bbc950cdd9"
            //    }
            //    );
        }
    }
}
