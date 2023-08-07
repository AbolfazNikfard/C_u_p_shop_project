using C_u_p_Shop_Project.Data;
using C_u_p_Shop_Project.Models;
using C_u_p_Shop_Project.PersianTranslationError;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();
builder.Services.AddDbContext<CropsShopContext>(options => {
    options.UseSqlServer("Data Source=DESKTOP-QDE3PR6; Initial Catalog=Crops_DB_Test;Trust Server Certificate=True;Integrated Security=False;User ID=sa;Password=1378529");
});
builder.Services.AddIdentity<User, IdentityRole>(option =>
option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10))
    .AddEntityFrameworkStores<CropsShopContext>()
    .AddDefaultTokenProviders()
    .AddErrorDescriber<PersianIdentityErrorDescriber>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
DbInitializer.Seed(app);
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();