using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
    public class REF_CUSTOMER_CATEGORY
    {
        [Key]
        public int CUSTOMER_CATEGORY_ID { get; set; }
        [Required]
        [Display(Name = "Customer Category")]
        public string CUSTOMER_CATEGORY_NAME { get; set; }
        public bool is_active { get; set; }
        public bool is_blocked { get; set; }
    }
}
