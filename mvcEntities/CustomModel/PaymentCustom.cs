using System.ComponentModel.DataAnnotations;

namespace mvcEntities.CustomModel
{
    public class PaymentCustom
    {
        [Key]
        public long PaymentId { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public long MenuPrice { get; set; }
        [Required]
        public long MenuId { get; set; }
        [Required]
        public long ModeId { get; set; }
        public long CategoryId { get; set; }

    }
}
