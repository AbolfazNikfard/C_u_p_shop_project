using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace C_u_p_Shop_Project.Models
{
    public class Buyer
    {
        public Buyer()
        {
            carts = new List<Cart>();
            orders = new List<Order>();
        }
        [Key]
        public int id { get; set; }
        public bool IsDelete { get; set; }
        //Navigation Property
        [Required]
        [ForeignKey("User")]
        public string userId { get; set; }
        public User user { get; set; }
        public ICollection<Cart> carts { get; set; }
        public ICollection<Order> orders { get; set; }
    }
}
