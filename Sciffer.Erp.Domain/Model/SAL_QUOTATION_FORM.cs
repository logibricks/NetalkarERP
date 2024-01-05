using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class SAL_QUOTATION_FORM
    {
        [Key]
        [Column(Order = 0)]
        public int QUOTATION_ID { get; set; }
        [Key]
        [Column(Order = 1)]
        public int FORM_ID { get; set; }
        [ForeignKey("FORM_ID")]
        public virtual REF_FORM REF_FORM { get; set; }
    }
}
