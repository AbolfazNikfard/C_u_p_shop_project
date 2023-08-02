using Crops_Shop_Project.Models;
using Microsoft.AspNetCore.Identity;

namespace Crops_Shop_Project.Data
{
    public class DbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.GetRequiredService<IServiceProvider>()
                .CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<CropsShopContext>();
                if (context != null)
                {
                    User user = null;
                    IdentityRole admin = null;
                    if (context.Users != null && !context.Users.Any())
                    {
                        user = new User()
                        {
                            PasswordHash =
                                "AQAAAAEAACcQAAAAEE5W8z3JXjlDAENV/mrcVLZ8rlmSq3FzpNfatgjigHhfrvQPEMIjQRLNUYED5Nt9rQ==",
                            Name = "ابوالفضل",
                            Family = "نیک فرد",
                            UserName = "Admin@gmail.com",
                            NormalizedUserName = "ADMIN@GMAIL.COM",
                            Email = "Admin@gmail.com",
                            NormalizedEmail = "ADMIN@GMAIL.COM",
                            EmailConfirmed = true,
                        };
                        context.Users.Add(user);
                    }
                    if (context.Roles != null && !context.Roles.Any())
                    {
                        admin = new IdentityRole()
                        {
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        };
                        var role = new List<IdentityRole>()
                        {

                            // new IdentityRole()
                            // {
                            //     Name = "Seller",
                            //     NormalizedName = "SELLER"
                            // },
                            new IdentityRole()
                            {
                                Name = "Buyer",
                                NormalizedName = "BUYER"
                            },
                        };
                        context.AddRange(role);
                        context.Add(admin);
                    }

                    if (user != null && admin != null)
                    {
                        context.UserRoles.Add(new IdentityUserRole<string>()
                        {
                            RoleId = admin.Id,
                            UserId = user.Id
                        });
                    }
                    context.SaveChanges();
                }

            }
        }
    }
}
