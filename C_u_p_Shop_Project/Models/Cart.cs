using C_u_p_Shop_Project.Enum;

namespace C_u_p_Shop_Project.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int Number { get; set; }        
        //Navigation Property
        public int productId { get; set; }
        public Product product { get; set; }
        public int buyerId { get; set; }
        public Buyer buyer { get; set; }
    }
}
