using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
   public  class REF_TDS_SECTION
    {
        [Key]
        public int TDS_SECTION_ID { get; set; }
        [Required]
        [Display(Name = "TDS Section Name")]
        public string TDS_SECTION_NAME { get; set; }
    }
}
