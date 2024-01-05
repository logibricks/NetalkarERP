using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
   public class sal_si_form
    {
        [Key]
        [Column(Order = 0)]
        public int si_id { get; set; }
        [Key]
        [Column(Order = 1)]
        public int form_id { get; set; }
        [ForeignKey("form_id")]
        public virtual REF_FORM REF_FORM { get; set; }
    }
}
