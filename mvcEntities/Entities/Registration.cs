using System.Collections.Generic;

namespace mvcEntities.Entities
{
    public partial class Registration
    {
        public Registration()
        {
            OrderHistory = new HashSet<OrderHistory>();
        }

        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public long RoleId { get; set; }

        public virtual ICollection<OrderHistory> OrderHistory { get; set; }
    }
}
