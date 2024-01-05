using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
   public class REF_ORG_TYPE
    {
        [Key]
        public int ORG_TYPE_ID { get; set; }
        [Required]
        [Display(Name = "Organization Type")]
        public string ORG_TYPE_NAME { get; set; }
        public bool? is_active { get; set; }
        public bool is_blocked { get; set; }
    }

}
