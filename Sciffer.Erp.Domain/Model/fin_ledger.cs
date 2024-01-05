
using System;
using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
    public class fin_ledger
    {
        [Key]
        public int fin_ledger_id { get; set; }
        public string document_type_code { get; set; }
        public int category_id { get; set; }
        public int source_document_id { get; set; }
        public string document_no { get; set; }
        public string source_document_no { get; set; }
        public DateTime ledger_date { get; set; }
        public DateTime? document_date { get; set; }
        public double ledger_amount { get; set; }
        public double ledger_amount_local { get; set; }
        public int currency_id { get; set; }
        public string narration { get; set; }
        public int create_user { get; set; }
        public DateTime? create_ts { get; set; }
        public int? modify_user { get; set; }
        public DateTime? modify_ts { get; set; }
        public bool is_active { get; set; }
        public string ref1 { get; set; }
        public string ref2 { get; set; }
        public string ref3 { get; set; }
        public DateTime? due_date { get; set; }
        public string ref_bill_no { get; set; }
        public string cancellation_remarks { get; set; }
        public int? status_id { get; set; }
        public virtual ref_status ref_status { get; set; }
        public int? cancellation_reason_id { get; set; }
        public DateTime? cancelled_date { get; set; }
        public int? cancelled_by { get; set; }
        // public virtual ICollection<fin_ledger_detail> fin_ledger_detail { get; set; }
    }
}
