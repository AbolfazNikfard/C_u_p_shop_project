// using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.Schema;

// namespace Crops_Shop_Project.Models
// {
//     public class Seller
//     {
//         public Seller()
//         {
//             products = new List<Product>();
//             orders = new List<Order>();
//         }
//         [Key]        
//         public int id { get; set; }
//         public bool IsDelete { get; set; }
//         //Relation
//         [Required]
//         [ForeignKey("User")]
//         public string userId { get; set; }
//         public User user { get; set; }
//         public ICollection<Order> orders { get; set; }
//         public ICollection<Product> products { get; set; }
//     }
// }
