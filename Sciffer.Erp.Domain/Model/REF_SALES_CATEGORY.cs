using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
    public class REF_SALES_CATEGORY
    {
        [Key]
        public int SALES_CATEGORY_ID { get; set; }
        [Required]
        [Display(Name ="Sales Category")]
        public string SALES_CATEGORY_NAME { get; set; }
    }
}
