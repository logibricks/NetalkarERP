using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_vendor_balance_details
    {
        [Key]
        public int? vendor_balance_detail_id { get; set; }
        public int? vendor_balance_id { get; set; }
        [ForeignKey("vendor_balance_id")]
        public virtual ref_vendor_balance ref_vendor_balance { get; set; }
        public int vendor_id { get; set; }
        [ForeignKey("vendor_id")]
        public virtual REF_VENDOR REF_VENDOR { get; set; }
        public DateTime due_date { get; set; }
        public string ref1 { get; set; }
        public string ref2 { get; set; }
        public string ref3 { get; set; }
        public DateTime doc_date { get; set; }
        public double amount { get; set; }
        public int amount_type_id { get; set; }
        [ForeignKey("amount_type_id")]
        public virtual ref_amount_type ref_amount_type { get; set; }
        public string line_remarks { get; set; }
        public bool is_active { get; set; }
    }
}
