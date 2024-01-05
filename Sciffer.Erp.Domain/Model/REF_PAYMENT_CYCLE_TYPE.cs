using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
   public class REF_PAYMENT_CYCLE_TYPE
    {
        [Key]
        public int PAYMENT_CYCLE_TYPE_ID { get; set; }
        [Required]
        [Display(Name = "Payment Cycle Type")]
        public string PAYMENT_CYCLE_TYPE_NAME { get; set; }
    }
}
