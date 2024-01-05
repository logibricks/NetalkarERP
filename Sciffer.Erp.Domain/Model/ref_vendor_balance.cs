using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_vendor_balance
    {
        [Key]
        public int? vendor_balance_id { get; set; }
        public int? offset_account_id { get; set; }
        [ForeignKey("offset_account_id")]
        public virtual ref_general_ledger ref_general_ledger { get; set; }
        public DateTime posting_date { get; set; }
        public string header_remark { get; set; }
        public bool is_active { get; set; }
        public int category_id { get; set; }
        public string doc_number { get; set; }
        public int create_user { get; set; }
        public virtual ICollection<ref_vendor_balance_details> ref_vendor_balance_details { get; set; }

    }
}
