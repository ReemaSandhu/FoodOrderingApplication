using System.ComponentModel.DataAnnotations;

namespace mvcEntities.CustomModel
{
    public class CustomMenu
    {
        [Key]
        public long MenuId { get; set; }
        public string MenuName { get; set; }
        public long MenuPrice { get; set; }
        public string MenuDescription { get; set; }
        public string MenuImage { get; set; }
        public string CategoryName { get; set; }
        public long CategoryId { get; set; }
    }
}
