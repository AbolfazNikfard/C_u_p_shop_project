using Crops_Shop_Project.Enum;

namespace Crops_Shop_Project.Models.View_Models
{
    public class OrderViewModel
    {
        public int productId { get; set; }
        public string productImage { get; set; }
        public string productName { get; set; }
        public int Weight { get; set; }
        public UnitOFMassMeasurement WeightMassUnit { get; set; }
        public int Price { get; set; }
        public int Number { get; set; }
        public string orderDate { get; set; }
        
    }
}
