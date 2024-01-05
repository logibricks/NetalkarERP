using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class REF_PAYMENT_CYCLE
    {
        [Key]
        public int PAYMENT_CYCLE_ID { get; set; }
        [Required]
        [Display(Name = "Payment Cycle")]
        public string PAYMENT_CYCLE_NAME { get; set; }
        public int PAYMENT_CYCLE_TYPE_ID { get; set; }
        [ForeignKey("PAYMENT_CYCLE_TYPE_ID")]
        public virtual REF_PAYMENT_CYCLE_TYPE REF_PAYMENT_CYCLE_TYPE { get; set; }
    }
}
