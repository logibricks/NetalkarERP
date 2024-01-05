using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Domain.Model
{
    public class pur_pi_return
    {
        [Key]
        public int pi_return_id { get; set; }   
        public string document_no { get; set; }
        public int category_id { get; set; }
        [ForeignKey("category_id")]
        public virtual ref_document_numbring ref_document_numbring { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime posting_date { get; set; }
        public int vendor_id { get; set; }
        [ForeignKey("vendor_id")]
        public virtual REF_VENDOR REF_VENDOR { get; set; }
        public double net_value { get; set; }
        public int net_currency_id { get; set; }
        [ForeignKey("net_currency_id")]
        public virtual REF_CURRENCY REF_CURRENCY1 { get; set; }
        public double gross_value { get; set; }
        public int gross_currency_id { get; set; }
        [ForeignKey("gross_currency_id")]
        public virtual REF_CURRENCY REF_CURRENCY2 { get; set; }
        public int business_unit_id { get; set; }
        [ForeignKey("business_unit_id")]
        public virtual REF_BUSINESS_UNIT REF_BUSINESS_UNIT { get; set; }
        public int plant_id { get; set; }
        [ForeignKey("plant_id")]
        public virtual REF_PLANT REF_PLANT { get; set; }
        public int freight_terms_id { get; set; }
        [ForeignKey("freight_terms_id")]
        public virtual REF_FREIGHT_TERMS REF_FREIGHT_TERMS { get; set; }
        //public string gate_entry_no { get; set; }
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //public DateTime? gate_entry_date { get; set; }
        public int created_by { get; set; }
        public string billing_address { get; set; }
        public string billing_city { get; set; }
        public int billing_state_id { get; set; }
        [ForeignKey("billing_state_id")]
        public virtual REF_STATE REF_STATE { get; set; }
        public string billing_pincode { get; set; }
        public string email_id { get; set; }
        public int payment_terms_id { get; set; }
        [ForeignKey("payment_terms_id")]
        public virtual REF_PAYMENT_TERMS REF_PAYMENT_TERMS { get; set; }
        public int payment_cycle_id { get; set; }
        [ForeignKey("payment_cycle_id")]
        public virtual REF_PAYMENT_CYCLE REF_PAYMENT_CYCLE { get; set; }
        [Display(Name = "Service Tax No")]
        public string service_tax_no { get; set; }
        [Display(Name = "GST No")]
        public string gst_no { get; set; }
        public DateTime created_ts { get; set; }
        public bool is_active { get; set; }
        public virtual ICollection<pur_pi_return_detail> pur_pi_return_detail { get; set; }
        public virtual ICollection<pur_pi_return_form> pur_pi_return_form { get; set; }
        public string internal_remarks { get; set; }
        public string remarks_on_doc { get; set; }
        public string attachment { get; set; }
        public int? tds_code_id { get; set; }
        public double? round_off { get; set; }
        public string gst_in { get; set; }
        public int gst_vendor_type_id { get; set; }
        public int status_id { get; set; }
        [ForeignKey("status_id")]
        public virtual ref_status ref_status { get; set; }
    }
}
