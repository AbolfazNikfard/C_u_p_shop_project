using System.Text.RegularExpressions;

namespace Crops_Shop_Project.Models
{
    public class GroupAndSubGroupViewModel
    {
        public int groupId { get; set; }
        public string groupName { get; set; }
        public GroupAndSubGroupViewModel()
        {
            //groops = new List<Group>();
            subGroops = new List<SubGroup>();
        }
        //public List<Group> groops { get; set; }
        public List<SubGroup> subGroops { get; set; }
    }
}
