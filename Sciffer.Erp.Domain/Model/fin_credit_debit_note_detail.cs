using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class fin_credit_debit_note_detail
    {
        [Key]
        public int fin_credit_debit_node_detail_id { get; set; }
        public int fin_credit_debit_node_id { get; set; }
        public int gl_ledger_id { get; set; }
        [ForeignKey("gl_ledger_id")]
        public virtual ref_general_ledger ref_general_ledger { get; set; }
        public string user_description { get; set; }
        public double credit_debit_amount { get; set; }
        public double credit_debit_local_amount { get; set; }
        public int tax_id { get; set; }
        [ForeignKey("tax_id")]
        public virtual ref_tax ref_tax { get; set; }
        public int cost_center_id { get; set; }
        [ForeignKey("cost_center_id")]
        public virtual ref_cost_center ref_cost_center { get; set; }
        public decimal? tax_rate { get; set; }
        public string exclusive_inclusive { get; set; }
        public int? item_type_id { get; set; }
        [ForeignKey("item_type_id")]
        public virtual ref_item_type ref_item_type { get; set; }
        public int? sac_hsn_id { get; set; }
    }
}
