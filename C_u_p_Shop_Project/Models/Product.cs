
using Crops_Shop_Project.Enum;
using NuGet.Protocol.Plugins;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Crops_Shop_Project.Models
{
    public class Product
    {
        public Product()
        {
            //groupToProducts = new List<GroupToProduct>();
            //subGroupToProducts = new List<subGroupToProduct>();
            orders = new List<Order>();
            carts = new List<Cart>();
        }
        [Key]
        public int id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        //public int Weight { get; set; }
        //public UnitOFMassMeasurement WeightMassUnit { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        //public UnitOFMassMeasurement StockMassUnit { get; set; }
        //public AcceptProduct confirmation { get; set; }
        public DateTime registerDate { get; set; }
        public bool IsDelete { get; set; }
        //Navigation Property
        //public int? sellerId { get; set; }
        public int? groupId { get; set; }
        public int? subGroupId { get; set; }
        public string productImage { get; set; }
        //public Seller seller { get; set; }
        public Group group { get; set; }
        public SubGroup subGroup { get; set; }

        //CartItem cartItem { get; set; }
        //ICollection<Category> categories { get; set; }
        //public ICollection<GroupToProduct> groupToProducts { get; set; }
        public ICollection<Cart> carts { get; set; }
        //public ICollection<subGroupToProduct> subGroupToProducts { get; set; }
        public ICollection<Order> orders { get; set; }
    }
}
