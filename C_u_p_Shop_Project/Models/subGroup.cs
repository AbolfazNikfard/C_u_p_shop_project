namespace C_u_p_Shop_Project.Models
{
    public class SubGroup
    {
        public SubGroup()
        {
            //subGroupToProducts = new List<subGroupToProduct>();
            product = new List<Product>();
        }
        public int id { get; set; }
        public string Name { get; set; }
        public bool IsDelete { get; set; }
        //Navigation property
        //[Required]
        //[ForeignKey("Category")]
        public int parentGroupId { get; set; }
        public Group parentGroup { get; set; }
        public ICollection<Product> product { get; set; }
        //public ICollection<subGroupToProduct> subGroupToProducts { get; set; }
    }
}
