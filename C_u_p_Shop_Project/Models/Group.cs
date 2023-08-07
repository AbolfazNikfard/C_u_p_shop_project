namespace C_u_p_Shop_Project.Models
{
    public class Group
    {
        public Group()
        {
            //groupToProducts = new List<GroupToProduct>();
            subGroups = new List<SubGroup>();
            product = new List<Product>();
            //products= new List<Product>(); 
        }
        public int id { get; set; }
        public string Name { get; set; }
        public bool IsDelete { get; set; }

        //navigation property
        //ICollection<Product> products { get; set; }
        //public ICollection<GroupToProduct> groupToProducts { get; set; }
        public ICollection<Product> product { get; set; }
        public ICollection<SubGroup> subGroups { get; set; }
    }
}
