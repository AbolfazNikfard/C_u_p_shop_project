namespace C_u_p_Shop_Project.Models.View_Models
{
    public class AddProductViewModel
    {
        public AddProductViewModel()
        {
            groupAndSubGroups = new List<GroupAndSubGroupViewModel>();
        }
        public Product product { get; set; }
        public List<GroupAndSubGroupViewModel> groupAndSubGroups { get; set; }
        public IFormFile productImage { get; set; }
    }
}
