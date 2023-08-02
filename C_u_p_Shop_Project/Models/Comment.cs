using System.ComponentModel.DataAnnotations;

namespace Crops_Shop_Project.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string userName { get; set; }
        public string comment { get; set; }
        //Relation
        public int productId { get; set; }
        public Product product { get; set; }
    }
}
