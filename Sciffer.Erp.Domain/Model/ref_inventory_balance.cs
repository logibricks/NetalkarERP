using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public  class ref_inventory_balance
    {
        [Key]
        public int? inventory_balance_id { get; set; }
        public int? offset_account_id { get; set; }
        [ForeignKey("offset_account_id")]
        public virtual ref_general_ledger ref_general_ledger { get; set; }
        public DateTime posting_date { get; set; }
        public string header_remarks { get; set; }
        public bool is_active { get; set; }
        public string doc_number { get; set; }
        public int category_id { get; set; }
        public virtual ICollection<ref_inventory_balance_details> ref_inventory_balance_details { get; set; }

    }
}
