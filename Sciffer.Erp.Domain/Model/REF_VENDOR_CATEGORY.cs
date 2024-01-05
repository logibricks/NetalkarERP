using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
    public class REF_VENDOR_CATEGORY
    {
        [Key]
        public int VENDOR_CATEGORY_ID { get; set; }
        [Required]
        [Display(Name = "Vendor Category")]
        public string VENDOR_CATEGORY_NAME { get; set; }
        public bool is_active { get; set; }
        public bool is_blocked { get; set; }
    }
}
