namespace C_u_p_Shop_Project.Models
{
    public class Favorite
    {
        public int productId { get; set; }
        public int buyerId { get; set; }
        public Product product { get; set; }
        public Buyer buyer { get; set; }
    }
}
