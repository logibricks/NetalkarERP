using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_general_ledger_balance_details
    {
        [Key]
        public int? gen_ledger_balance_detail_id { get; set; }
        public int? gen_ledger_balance_id { get; set; }
        [ForeignKey("gen_ledger_balance_id")]
        public virtual ref_general_ledger_balance ref_general_ledger_balance { get; set; }
        public int general_ledger_id { get; set; }
        [ForeignKey("general_ledger_id")]
        public virtual ref_general_ledger ref_general_ledger { get; set; }
        public string ref1 { get; set; }
        public string ref2 { get; set; }
        public string ref3 { get; set; }
        public DateTime? due_date { get; set; }
        public double amount { get; set; }
        public int amount_type_id { get; set; }
        [ForeignKey("amount_type_id")]
        public virtual ref_amount_type ref_amount_type { get; set; }
        public string line_remark { get; set; }
        public bool is_active { get; set; }
    }
}
