using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_validation_gl
    {
        [Key]
        public int validation_gl_detail_id { get; set; }
        public int validation_id { get; set; }
        public int ledger_account_type_id { get; set; }
        [ForeignKey("ledger_account_type_id")]
        public virtual ref_ledger_account_type ref_ledger_account_type { get; set; }
        public int gl_ledger_id { get; set; }
        [ForeignKey("gl_ledger_id")]
        public virtual ref_general_ledger ref_general_ledger { get; set; }
    }
}
