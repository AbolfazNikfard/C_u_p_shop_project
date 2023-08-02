namespace Crops_Shop_Project.Models.View_Models
{
    public class GroupWithSubGroupsWithProductsViewModel
    {
        public GroupWithSubGroupsWithProductsViewModel()
        {
            subGroups= new List<SubGroup>();
        }
        public int groupId { get; set; }
        public string groupName { get; set; }
        public int productNumber { get; set; }
        public List<SubGroup> subGroups { get; set; }
    }
}
