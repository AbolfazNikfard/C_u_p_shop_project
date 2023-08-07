﻿using Microsoft.AspNetCore.Identity;

namespace C_u_p_Shop_Project.Models
{
    public class User:IdentityUser
    {
        public string? Name { get; set; }
        public string? Family { get; set; }
        public string? Address { get; set; }
        public string? userImage { get; set; }
        public bool IsDelete { get; set; }
        //Relation        
        public Buyer buyer { get; set; }
        // public Seller seller { get; set; }
    }
}
