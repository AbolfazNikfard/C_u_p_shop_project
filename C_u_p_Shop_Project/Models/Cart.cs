using Crops_Shop_Project.Enum;

namespace Crops_Shop_Project.Models
{
    public class Cart
    {
        public Cart()
        {
            //cartItems= new List<CartItem>();
        }
        public int id { get; set; }
        public int Number { get; set; }        
        //public ulong getTotalprice() { return product.Price * Number; }
        //Navigation Property
        public int productId { get; set; }
        public Product product { get; set; }
        public int buyerId { get; set; }
        public Buyer buyer { get; set; }
        //public int OrderId { get; set; }
        //public ICollection<CartItem> cartItems { get; set; }
        //public Customer Customer { get; set; }

    }
}
