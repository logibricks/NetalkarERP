using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
  public  class REF_FREIGHT_TERMS
    {
        [Key]
        public int FREIGHT_TERMS_ID { get; set; }
        [Required]
        [Display(Name = "Freight Terms Name")]
        public string FREIGHT_TERMS_NAME { get; set; }
        public bool Is_active { get; set; }
        public bool Is_blocked { get; set; }
    }
}
