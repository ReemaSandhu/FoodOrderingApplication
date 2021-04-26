using System.Collections.Generic;
namespace mvcEntities.Entities
{
    public partial class Category
    {
        public Category()
        {
            MenuList = new HashSet<MenuList>();
            OrderHistory = new HashSet<OrderHistory>();
        }

        public long CategoryId { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<MenuList> MenuList { get; set; }
        public virtual ICollection<OrderHistory> OrderHistory { get; set; }
    }
}
