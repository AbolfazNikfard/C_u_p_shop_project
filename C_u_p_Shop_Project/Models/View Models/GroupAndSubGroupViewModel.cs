using System.Text.RegularExpressions;

namespace C_u_p_Shop_Project.Models
{
    public class GroupAndSubGroupViewModel
    {
        public GroupAndSubGroupViewModel()
        {
            subGroops = new List<SubGroup>();
        }
        public int groupId { get; set; }
        public string groupName { get; set; }
        public List<SubGroup> subGroops { get; set; }
    }
}
