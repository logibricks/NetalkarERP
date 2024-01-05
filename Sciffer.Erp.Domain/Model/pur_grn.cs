using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class pur_grn
    {
        [Key]
        public int grn_id { get; set; }
        public int po_id { get; set; }
        public string grn_number { get; set; }
 
        public int vendor_id { get; set; }
        [ForeignKey("vendor_id")]
        public virtual REF_VENDOR REF_VENDOR { get; set; }
        public int category_id { get; set; }
        [ForeignKey("category_id")]
        public virtual REF_CATEGORY REF_CATEGORY { get; set; }
        public double net_value { get; set; }
        public int net_currency_id { get; set; }
        [ForeignKey("net_currency_id")]
        public virtual REF_CURRENCY REF_CURRENCY { get; set; }
        public double gross_value { get; set; }
        public int gross_currency_id { get; set; }
        [ForeignKey("gross_currency_id")]
        public virtual REF_CURRENCY REF_CURRENCY1 { get; set; }
        public DateTime posting_date { get; set; }
        public int business_unit_id { get; set; }
        [ForeignKey("business_unit_id")]
        public virtual REF_BUSINESS_UNIT REF_BUSINESS_UNIT { get; set; }
        public int plant_id { get; set; }
        [ForeignKey("plant_id")]
        public virtual REF_PLANT REF_PLANT { get; set; }
        public int freight_terms_id { get; set; }
        [ForeignKey("freight_terms_id")]
        public virtual REF_FREIGHT_TERMS REF_FREIGHT_TERMS { get; set; }
        public DateTime? delivery_date { get; set; }
        public string vendor_doc_no { get; set; }
        public DateTime? vendor_doc_date { get; set; }
        public string gate_entry_number { get; set; }
        public DateTime? gate_entry_date { get; set; }
        public int? created_by { get; set; }
        public string billing_address { get; set; }
        public string billing_city { get; set; }
        public int? billing_state_id { get; set; }
        public string billing_pin_code { get; set; }
        public string email_id { get; set; }
        public int payment_terms_id { get; set; }
        [ForeignKey("payment_terms_id")]
        public virtual REF_PAYMENT_TERMS REF_PAYMENT_TERMS { get; set; }
        public int payment_cycle_id { get; set; }
        [ForeignKey("payment_cycle_id")]
        public virtual REF_PAYMENT_CYCLE REF_PAYMENT_CYCLE { get; set; }
        public int status_id { get; set; }
        [ForeignKey("status_id")]
        public virtual ref_status ref_status { get; set; }
        public string internal_remarks { get; set; }
        public string remarks_on_doc { get; set; }
        public string attachment { get; set; }
        public bool is_active { get; set; }
        public virtual ICollection<pur_grn_detail> pur_grn_detail { get; set; }
        public virtual ICollection<pur_grn_form> pur_grn_form { get; set; }
        public string pan_no { get; set; }
        [Display(Name = "ECC No")]
        public string ecc_no { get; set; }
        [Display(Name = "VAT TIN No")]
        public string vat_tin_no { get; set; }
        [Display(Name = "CST TIN No")]
        public string cst_tin_no { get; set; }
        [Display(Name = "Service Tax No")]
        public string service_tax_no { get; set; }
        [Display(Name = "GST No")]
        public string gst_no { get; set; }
        public bool? order_status { get; set; }
        public int place_of_supply_id { get; set; }
        public string gst_in { get; set; }
        public int gst_vendor_type_id { get; set; }
        public string cancellation_remarks { get; set; }
        public int? cancellation_reason_id { get; set; }
        public int? cancelled_by { get; set; }
        public DateTime? cancelled_date { get; set; }
        public int? modify_by { get; set; }
        public DateTime? modify_ts { get; set; }
        
    }
}
