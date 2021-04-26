using System.ComponentModel.DataAnnotations;

namespace mvcEntities.CustomModel
{
    public class PastOrders
    {
        [Key]
        public long OrderId { get; set; }
        public long UserId { get; set; }
        public long CategoryId { get; set; }
        public long MenuId { get; set; }
    }
}
