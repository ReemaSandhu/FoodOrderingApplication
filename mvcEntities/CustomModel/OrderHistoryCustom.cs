using System.ComponentModel.DataAnnotations;

namespace mvcEntities.CustomModel
{
    public class OrderHistoryCustom
    {
        [Key]
        public long OrderId { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string MenuName { get; set; }
        public string MenuImage { get; set; }
        public long MenuPrice { get; set; }
        public string MenuDescription { get; set; }
        public string CategoryName { get; set; }
    }
}
