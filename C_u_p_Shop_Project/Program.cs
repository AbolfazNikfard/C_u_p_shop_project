using C_u_p_Shop_Project.Data;
using C_u_p_Shop_Project.Models;
using C_u_p_Shop_Project.PersianTranslationError;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
builder.Configuration.AddJsonFile("appsettings.json");
// Add services to the container.
services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();

// Retrieve the connection string from an environment variable
string connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

services.AddDbContext<CropsShopContext>(options =>
{
    options.UseSqlServer(connectionString,
                       options => options.EnableRetryOnFailure());
});


services.AddIdentity<User, IdentityRole>(option =>
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
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
DbInitializer.Seed(app);
app.Run();

