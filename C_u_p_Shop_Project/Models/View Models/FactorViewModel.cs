namespace C_u_p_Shop_Project.Models.View_Models
{
    public class FactorViewModel
    {
        public FactorViewModel()
        {
            cartItems = new List<CartViewModel>();
        }
        public string Telphone { get; set; }
        public string Address { get; set; }
        public List<CartViewModel> cartItems { get; set; }
    }
}
