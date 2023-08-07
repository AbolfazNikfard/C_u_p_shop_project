using C_u_p_Shop_Project.Enum;

namespace C_u_p_Shop_Project.Models.View_Models
{
    public class CartViewModel
    {
        public int productId { get; set; }      
        public string productName { get; set; }
        public string productImage { get; set; }
        public int productPrice { get; set; }
        public string selectedNumberOfProducts { get; set; }
        //public bool CompletePurchaseAndPayment { get; set; }
        // public UnitOFMassMeasurement productWeightUnit { get; set; }
        // public int productWeight { get; set; }
    }
}
