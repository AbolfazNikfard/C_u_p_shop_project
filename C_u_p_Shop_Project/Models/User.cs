using Microsoft.AspNetCore.Identity;

namespace Crops_Shop_Project.Models
{
    public class User:IdentityUser
    {
        public string? Name { get; set; }
        public string? Family { get; set; }
        public string? Address { get; set; }
        public bool IsDelete { get; set; }
        //Relation        
        public Buyer buyer { get; set; }
        // public Seller seller { get; set; }
    }
}
