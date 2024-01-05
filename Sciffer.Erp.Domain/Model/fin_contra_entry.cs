using System;
using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
    public class fin_contra_entry
    {
        [Key]
        public int contra_entry_id { get; set; }
        public int category_id { get; set; }
        public string document_no { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime posting_date { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? document_date { get; set; }
        public string ref_doc_no { get; set; }
        public int curreny_id { get; set; }
        public int from_cash_bank { get; set; }
        public int transfer_funds_from { get; set; }
        public int to_cash_bank { get; set; }
        public int transfer_funds_to { get; set; }
        public double transfer_amount { get; set; }
        public double current_balance_from { get; set; }
        public double current_balance_to { get; set; }
        [DataType(DataType.MultilineText)]
        public string reamrks { get; set; }
        public string attachment { get; set; }
        public bool is_active { get; set; }
        public int? status_id { get; set; }
        public virtual ref_status ref_status { get; set; }
        public int? cancellation_reason_id { get; set; }
        public string cancellation_remarks { get; set; }
        public DateTime? cancelled_date { get; set; }
        public int? cancelled_by { get; set; }
        public DateTime? modify_ts { get; set; }
        public int? modify_by { get; set; }
        public int? created_by { get; set; }
        public DateTime? created_ts { get; set; }
    }
}
