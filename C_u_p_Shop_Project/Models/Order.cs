using C_u_p_Shop_Project.Enum;

namespace C_u_p_Shop_Project.Models
{
    public class Order
    {
        public int id { get; set; }
        public int Price { get; set; }
        //public int Weight { get; set; }
        //public UnitOFMassMeasurement WeightMassUnit { get; set; }
        public int Number { get; set; }
        public DateTime orderDateTime { get; set; }
        public int productId { get; set; }
        public int buyerId { get; set; }
        //public int? sellerId { get; set; }
        //public Seller seller { get; set; }
        public Buyer buyer { get; set; }
        public Product product { get; set; }
    }
}
