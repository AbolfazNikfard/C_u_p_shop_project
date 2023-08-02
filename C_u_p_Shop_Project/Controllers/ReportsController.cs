using Crops_Shop_Project.Convertor;
using Crops_Shop_Project.Data;
using Crops_Shop_Project.Enum;
using Crops_Shop_Project.Models;
using Crops_Shop_Project.Models.View_Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using System.Data;

namespace Crops_Shop_Project.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ReportsController : Controller
    {
        private CropsShopContext _context;
        private UserManager<User> _userManager;
        public ReportsController(CropsShopContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> SoldProducts()
        {
            List<SoldProductViewModel> sold;
            // if (User.IsInRole("Seller"))
            // {
            //     var user = await _userManager.FindByNameAsync(User.Identity.Name);
            //     if (user == null) { return NotFound(); }

            //     var seller = _context.sellers.SingleOrDefault(s => s.userId == user.Id);
            //     if (seller == null) { return NotFound(); }

            //     sold = _context.orders.Include(p => p.product)
            //         .Include(b => b.buyer).ThenInclude(u => u.user)
            //         .Where(o => o.product.sellerId == seller.id && o.product.confirmation == AcceptProduct.Accept)
            //         .Select(o => new SoldProductViewModel
            //         {
            //             productId = o.productId,
            //             productName = o.product.Name,
            //             productImg = o.product.productImage,
            //             buyerEmail = o.buyer.user.Email,
            //             soldNumber = o.Number,
            //             price = o.Price,
            //             SoldDate = o.orderDateTime.ToShamsi()
            //         }
            //         ).IgnoreQueryFilters().ToList();
            // }
            // else
            // {
            sold = _context.orders.Include(p => p.product)//.Where(o => o.product.confirmation == AcceptProduct.Accept)
            .Include(b => b.buyer)/*.Include(s => s.seller)*/.ThenInclude(u => u.user)
                .Select(o => new SoldProductViewModel
                {
                    productId = o.productId,
                    productName = o.product.Name,
                    productImg = o.product.productImage,
                    buyerEmail = o.buyer.user.Email,
                    //sellerEmail = o.seller.user.Email,
                    soldNumber = o.Number,
                    price = o.Price,
                    SoldDate = o.orderDateTime.ToShamsi()
                }
                ).IgnoreQueryFilters().ToList();
            //}
            return View(sold);
        }
        public async Task<IActionResult> SoldPerProduct()
        {
            List<SoldPerProductViewModel> soldPerProduct;
            // if (User.IsInRole("Seller"))
            // {
            //     var user = await _userManager.FindByNameAsync(User.Identity.Name);
            //     if (user == null) { return NotFound(); }

            //     var seller = _context.sellers.SingleOrDefault(s => s.userId == user.Id);
            //     soldPerProduct = _context.products.Where(p => p.sellerId == seller.id && p.confirmation == AcceptProduct.Accept)
            //         .Include(o => o.orders)
            //         .Select(p => new SoldPerProductViewModel
            //         {
            //             productId = p.id,
            //             productName = p.Name,
            //             productImg = p.productImage,
            //             productStatus = p.IsDelete,
            //             soldNumber = p.orders.Sum(o => o.Number)
            //         }).IgnoreQueryFilters().ToList();
            // }
            // else
            soldPerProduct = _context.products//.Where(p => p.confirmation == AcceptProduct.Accept)
                .Include(o => o.orders)
                .Select(p => new SoldPerProductViewModel
                {
                    productId = p.id,
                    productName = p.Name,
                    productImg = p.productImage,
                    productStatus = p.IsDelete,
                    soldNumber = p.orders.Sum(o => o.Number)
                }).IgnoreQueryFilters().ToList();
            return View(soldPerProduct);
        }
        [HttpGet]
        public async Task<IActionResult> SoldChart()
        {
            List<SoldDateTimeWithSoldNumberViewModel> sold = new List<SoldDateTimeWithSoldNumberViewModel>();
            // if (User.IsInRole("Seller"))
            // {
            //     var user = await _userManager.FindByNameAsync(User.Identity.Name);
            //     if (user == null) { return NotFound(); }

            //     var seller = _context.sellers.SingleOrDefault(s => s.userId == user.Id);
            //     if (seller == null) { return NotFound(); }

            //     sold = _context.orders.Include(p => p.product)
            //    .Where(o => (o.product.sellerId == seller.id && o.product.confirmation == AcceptProduct.Accept) && (DateTime.Now.Date <= o.orderDateTime && o.orderDateTime.Date < DateTime.Now.Date.AddDays(1)))
            //    .OrderBy(o => o.orderDateTime)
            //     .Select(o => new SoldDateTimeWithSoldNumberViewModel
            //     {
            //         soldDateTime = o.orderDateTime,
            //         soldNumber = o.Number
            //     }).IgnoreQueryFilters().ToList();
            // }
            // else
            // {
            sold = _context.orders.Include(p => p.product)
           .Where(o => DateTime.Now.Date <= o.orderDateTime && o.orderDateTime.Date < DateTime.Now.Date.AddDays(1))//Where(o => (o.product.confirmation == AcceptProduct.Accept) && (DateTime.Now.Date <= o.orderDateTime && o.orderDateTime.Date < DateTime.Now.Date.AddDays(1))
           .OrderBy(o => o.orderDateTime)
            .Select(o => new SoldDateTimeWithSoldNumberViewModel
            {
                soldDateTime = o.orderDateTime,
                soldNumber = o.Number
            }).IgnoreQueryFilters().ToList();
            //}
            List<LineChartViewModel> chart = new List<LineChartViewModel>();
            if (sold.Count != 0)
            {
                var startPoint = DateTime.Now.Date;
                var endPoint = DateTime.Now.Date.AddDays(1);
                chart = CreateChart(startPoint, endPoint, sold, "Today");
                return View(chart);
            }
            else
            {
                chart.Add(new LineChartViewModel { filterMessage = "Today" });
            }
            return View(chart);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SoldChart(List<LineChartViewModel> chartObj)
        {
            // Seller seller = new Seller();
            // if (User.IsInRole("Seller"))
            // {
            //     var user = await _userManager.FindByNameAsync(User.Identity.Name);
            //     if (user == null) { return NotFound(); }

            //     seller = _context.sellers.SingleOrDefault(s => s.userId == user.Id);
            //     if (seller == null) { return NotFound(); }
            // }

            List<SoldDateTimeWithSoldNumberViewModel> sold = new List<SoldDateTimeWithSoldNumberViewModel>();
            if (chartObj[0].filterMessage == "Today")
            {
                // if (User.IsInRole("Seller"))
                // {
                //     sold = _context.orders.Include(p => p.product)
                //    .Where(o => (o.product.sellerId == seller.id && o.product.confirmation == AcceptProduct.Accept) && (DateTime.Now.Date <= o.orderDateTime && o.orderDateTime.Date < DateTime.Now.Date.AddDays(1)))
                //    .OrderBy(o => o.orderDateTime)
                //    .Select(o => new SoldDateTimeWithSoldNumberViewModel
                //    {
                //        soldDateTime = o.orderDateTime,
                //        soldNumber = o.Number
                //    }).IgnoreQueryFilters().ToList();
                // }
                // else
                // {
                sold = _context.orders//.Include(p => p.product)
              .Where(o => DateTime.Now.Date <= o.orderDateTime && o.orderDateTime.Date < DateTime.Now.Date.AddDays(1))//Where(o => (o.product.confirmation == AcceptProduct.Accept) && (DateTime.Now.Date <= o.orderDateTime && o.orderDateTime.Date < DateTime.Now.Date.AddDays(1)))
              .OrderBy(o => o.orderDateTime)
               .Select(o => new SoldDateTimeWithSoldNumberViewModel
               {
                   soldDateTime = o.orderDateTime,
                   soldNumber = o.Number
               }).IgnoreQueryFilters().ToList();
                //}
            }
            else
            {
                //     if (User.IsInRole("Seller"))
                //     {
                //         sold = _context.orders.Include(p => p.product)
                //    .Where(o => o.product.sellerId == seller.id && o.product.confirmation == AcceptProduct.Accept)
                //    .OrderBy(o => o.orderDateTime)
                //     .Select(o => new SoldDateTimeWithSoldNumberViewModel
                //     {
                //         soldDateTime = o.orderDateTime,
                //         soldNumber = o.Number
                //     }).IgnoreQueryFilters().ToList();
                //     }
                //     else
                //     {
                sold = _context.orders//.Include(p => p.product)//.Where(o => o.product.confirmation == AcceptProduct.Accept)
           .OrderBy(o => o.orderDateTime)
            .Select(o => new SoldDateTimeWithSoldNumberViewModel
            {
                soldDateTime = o.orderDateTime,
                soldNumber = o.Number
            }).IgnoreQueryFilters().ToList();
                //}
            }
            if (sold.Count != 0)
            {
                DateTime startPoint = sold.Select(s => s.soldDateTime).First();
                DateTime endPoint = sold.Select(s => s.soldDateTime).Last();
                switch (chartObj[0].filterMessage)
                {
                    case "Today":
                        chartObj = CreateChart(DateTime.Now.Date, DateTime.Now.Date.AddDays(1), sold, "Today");
                        break;
                    case "Weekly":
                        chartObj = CreateChart(startPoint, endPoint, sold, "Weekly");
                        break;
                    case "Monthly":
                        chartObj = CreateChart(startPoint, endPoint, sold, "Monthly");
                        break;
                    case "Yearly":
                        chartObj = CreateChart(startPoint, endPoint, sold, "Yearly");
                        break;
                }
            }
            return View(chartObj);
        }
        public async Task<IActionResult> bestSellingProducts()
        {
            List<BarChartViewModel> barChart;
            // if (User.IsInRole("Seller"))
            // {
            //     var user = await _userManager.FindByNameAsync(User.Identity.Name);
            //     if (user == null) { return NotFound(); }

            //     var seller = _context.sellers.SingleOrDefault(s => s.userId == user.Id);
            //     if (seller == null) { return NotFound(); }

            //     barChart = _context.products.Where(p => p.sellerId == seller.id && p.confirmation == AcceptProduct.Accept)
            //         .Include(o => o.orders)
            //         .Select(p => new BarChartViewModel
            //         {
            //             Lable = ("#" + p.id + " " + p.Name),
            //             Quantity = p.orders.Sum(o => o.Number)
            //         }).IgnoreQueryFilters().ToList();
            // }
            // else
            // {
            barChart = _context.products//.Where(p => p.confirmation == AcceptProduct.Accept)
               .Include(o => o.orders)
               .Select(p => new BarChartViewModel
               {
                   Lable = ("#" + p.id + " " + p.Name),
                   Quantity = p.orders.Sum(o => o.Number)
               }).IgnoreQueryFilters().ToList();
            //}
            int topSellers;
            if (barChart.Count >= 10)
                topSellers = 10;
            else
                topSellers = barChart.Count;
            barChart = barChart.OrderByDescending(b => b.Quantity).Take(topSellers).ToList();
            return View(barChart);
        }
        #region ExtraMethod
        private List<LineChartViewModel> CreateChart(DateTime startPoint, DateTime endPoint, List<SoldDateTimeWithSoldNumberViewModel> sold, string filter)
        {
            DateTime temp = new DateTime();
            List<DateTime> ChartLable = new List<DateTime>();
            List<int> SoldNumber = new List<int>();
            ChartLable.Add(startPoint);
            switch (filter)
            {
                case "Today":
                    temp = startPoint.AddHours(1);
                    break;
                case "Weekly":
                    temp = (startPoint.AddDays(7)).Date.AddDays(1);
                    break;
                case "Monthly":
                    temp = (startPoint.AddMonths(1)).Date.AddDays(1);
                    break;
                case "Yearly":
                    temp = (startPoint.AddYears(1)).Date.AddDays(1);
                    break;

            }
            while (temp < endPoint)
            {
                ChartLable.Add(temp);
                switch (filter)
                {
                    case "Today":
                        temp = temp.AddHours(1);
                        break;
                    case "Weekly":
                        temp = temp.AddDays(7);
                        break;
                    case "Monthly":
                        temp = temp.AddMonths(1);
                        break;
                    case "Yearly":
                        temp = temp.AddYears(1);
                        break;

                }
            }
            ChartLable.Add(temp);
            if (ChartLable.Count == 1)
            {
                var sumOfNumber = sold.Where(o => o.soldDateTime < ChartLable[0]).Sum(o => o.soldNumber);
                SoldNumber.Add(sumOfNumber);
            }
            else
            {
                for (int i = 0; i < ChartLable.Count; i++)
                {

                    if (i != ChartLable.Count - 1)
                    {
                        var sumOfNumber = sold.Where(o => ChartLable[i] <= o.soldDateTime && o.soldDateTime < ChartLable[i + 1]).Sum(o => o.soldNumber);
                        SoldNumber.Add(sumOfNumber);
                    }
                }
            }
            List<LineChartViewModel> chart = new List<LineChartViewModel>();
            for (int i = 1; i < ChartLable.Count; i++)
            {
                if (filter == "Today")
                {
                    chart.Add(new LineChartViewModel
                    {
                        lable = (ChartLable[i].TimeOfDay).ToString(),
                        data = SoldNumber[i - 1],
                        filterMessage = filter
                    });
                }
                else
                {
                    chart.Add(new LineChartViewModel
                    {
                        lable = ChartLable[i].ToShamsi(),
                        data = SoldNumber[i - 1],
                        filterMessage = filter
                    });
                }
            }
            return chart;
        }
        #endregion
    }
}
