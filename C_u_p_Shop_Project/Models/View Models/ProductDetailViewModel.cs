namespace C_u_p_Shop_Project.Models.View_Models
{
    public class ProductDetailViewModel
    {
        public ProductDetailViewModel()
        {
            comments= new List<Comment>();
        }
        public Product product { get; set; }
        public List<Comment> comments { get; set; }
    }
}
