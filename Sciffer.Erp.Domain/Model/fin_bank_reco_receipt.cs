using System;
using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
    public class fin_bank_reco_receipt
    {
        [Key]
        public int fin_bank_reco_receipt_id { get; set; }
        public int fin_ledger_payment_detail_id { get; set; }
        public int fin_bank_reco_id { get; set; }
        public bool is_selected { get; set; }
        public int doc_category_id { get; set; }
        public string doc_no { get; set; }
        public DateTime doc_posting_date { get; set; }
        public int entity_type_id { get; set; }
        public int entity_id { get; set; }
        public DateTime bank_tran_date { get; set; }
        public double amount { get; set; }

    }
}
