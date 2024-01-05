using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
   public class REF_TERRITORY
    {
        [Key]
        public int TERRITORY_ID { get; set; }
        [Required]
        [Display(Name = "Territory")]
        public string TERRITORY_NAME { get; set; }
        public bool is_active { get; set; }
        public bool is_blocked { get; set; }
    }
}
