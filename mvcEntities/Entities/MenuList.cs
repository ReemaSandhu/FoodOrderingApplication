using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace mvcEntities.Entities
{
    public partial class MenuList
    {
        public MenuList()
        {
            OrderHistory = new HashSet<OrderHistory>();
            Payment = new HashSet<Payment>();
        }

        public long MenuId { get; set; }
        public string MenuName { get; set; }
        public long MenuPrice { get; set; }
        public string MenuDescription { get; set; }
        public string MenuImage { get; set; }
        [NotMapped]
        [DisplayName("Upload Image")]
        public IFormFile ImageUploader { get; set; }
        public long CategoryId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<OrderHistory> OrderHistory { get; set; }
        public virtual ICollection<Payment> Payment { get; set; }
    }
}
