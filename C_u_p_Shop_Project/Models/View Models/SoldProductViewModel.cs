namespace Crops_Shop_Project.Models.View_Models
{
    public class SoldProductViewModel
    {
        public int productId { get; set; }
        public string productName { get; set; }
        public string productImg { get; set; }
        public string buyerEmail { get; set; }
        public string sellerEmail { get; set; }
        public int soldNumber { get; set; }
        public int price { get; set; }
        public string SoldDate { get; set; }
    }
}
