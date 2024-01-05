using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
   public class fin_credit_debit_note_detail_vm
    {
        public int fin_credit_debit_note_detail_id { get; set; }
        public int fin_credit_debit_note_id { get; set; }
        public int gl_ledger_id { get; set; }
        public string gl_ledger_name { get; set; }
        public string user_description { get; set; }
        public double? credit_debit_amount { get; set; }
        public double? credit_debit_local_amount { get; set; }
        public int tax_id { get; set; }
        public int cost_center_id { get; set; }
        public int item_type_id { get; set; }
        public int? sac_hsn_id { get; set; }
        public decimal? price { get; set; }
        public decimal? discount { get; set; }
        public string entity_type_name { get; set; }
        public string description { get; set; }
        public string item_type_name { get; set; }
        public string tax_name { get; set; }
        public string hsn_code { get; set; }
        public string sac_code { get; set; }
        public string cost_center { get; set; }
        public decimal? tax_rate { get; set; }
        public decimal? tax_element_rate { get; set; }
        public string exclusive_inclusive { get; set; }
        public decimal? amount { get; set; }
        public decimal? cgst { get; set; }
        public decimal? igst { get; set; }
        public decimal? sgst { get; set; }
        public int? fin_credit_debit_node_detail_id { get; set; }
        public int? fin_credit_debit_node_id { get; set; }
    }
}
