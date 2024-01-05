using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
    public class REF_BRAND
    {
        [Key]
        public int BRAND_ID { get; set; }
        [Required]
        [Display(Name = "Brand")]
        public string BRAND_NAME { get; set; }
        public bool is_active { get; set; }
        public bool is_blocked { get; set; }
    }
}
