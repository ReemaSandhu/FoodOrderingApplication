using System.ComponentModel.DataAnnotations;

namespace mvcEntities.CustomModel
{
    public class Login                                                                                                                                                                              
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}
