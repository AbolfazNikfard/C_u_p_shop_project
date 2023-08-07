namespace C_u_p_Shop_Project.Models
{
    public class ShowProductByGroupComponentViewModel
    {
        public ShowProductByGroupComponentViewModel()
        {
            products= new List<Product>();
        }
        public IEnumerable<Product> products { get; set; }
        public string whichViewCallMe { get; set; }
        public int? id { get; set; }
        public string? searchInput { get; set; }

    }
}
