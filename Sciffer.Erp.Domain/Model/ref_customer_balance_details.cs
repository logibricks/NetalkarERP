using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_customer_balance_details
    {
        [Key]
        public int? customer_balance_detail_id { get; set; }
        public int? customer_balance_id { get; set; }
        [ForeignKey("customer_balance_id")]
        public virtual ref_customer_balance ref_customer_balance { get; set; }
        public int customer_id { get; set; }
        [ForeignKey("customer_id")]
        public virtual REF_CUSTOMER REF_CUSTOMER { get; set; }
        public string ref1 { get; set; }
        public string ref2 { get; set; }
        public string ref3 { get; set; }
        public DateTime? doc_date { get; set; }
        public double amount { get; set; }
        public int amount_type_id { get; set; }
        [ForeignKey("amount_type_id")]
        public virtual ref_amount_type ref_amount_type {get;set;}
        public string line_remark { get; set; }
        public DateTime? due_date { get; set; }
        public bool is_active { get; set; }

    }
}
