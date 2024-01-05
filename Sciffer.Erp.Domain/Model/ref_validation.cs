using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_validation
    {
        [Key]
        public int validation_id { get; set; }
        [Display(Name = "SO Mandatory for Sales Invoice")]
        public bool so_mandatory_for_si { get; set; }
        [Display(Name = "Can rate be changed at Sales Invoice")]
        public bool rate_change_at_si { get; set; }
        [Display(Name = "Purchase Req mandatory for PO")]
        public bool pr_mandatory_for_po { get; set; }
        [Display(Name = "PO Mandatory for GRN")]
        public bool po_mandatory_for_grn { get; set; }
        [Display(Name = "PO Mandatory for Invoice")]
        public bool po_mandatory_for_pi { get; set; }
        [Display(Name = "Production order - allow to change bom item")]
        public bool allow_to_change_bom { get; set; }

        [Display(Name = "MachineEntry - allow to check cycle time")]
        public bool allow_to_check_cycle_time { get; set; }
        [Display(Name = "Allow Negative Cash ")]
        public bool allow_negative_cash { get; set; }
        [Display(Name = "Allow Negative Bank")]
        public bool allow_negative_bank { get; set; }
        [Display(Name = "Round off Values")]
        public int round_off_values { get; set; }
        [Display(Name = "GRN")]
        public bool grn_for_qa { get; set; }
        [Display(Name = "Goods Receipt")]
        public bool goods_receipt_for_qa { get; set; }
        [Display(Name = "Production Receipt")]
        public bool production_receipt_for_qa { get; set; }
        [Display(Name = "Inventory Adjustment")]
        public bool inventory_adjustment { get; set; }
        [Display(Name = "Order Creation Date for Next Month")]
        public int order_creation_date_for_next_month { get; set; }
        public virtual ICollection<ref_validation_gl> ref_validation_gl { get; set; }
    }
}
