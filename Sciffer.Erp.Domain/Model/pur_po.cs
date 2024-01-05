using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class pur_po
    {
        [Key]
        public int po_id { get; set; }
        public int category_id { get; set; }
        [ForeignKey("category_id")]
        public virtual REF_CATEGORY REF_CATEGORY { get; set; }
        public int item_service_id { get; set; }
        public int? pur_requisition_id { get; set; }
        public string po_no { get; set; }
        public int vendor_id { get; set; }
        [ForeignKey("vendor_id")]
        public virtual REF_VENDOR REF_VENDOR { get; set; }
        [Display(Name = "Date")]
        [Required(ErrorMessage = "date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime po_date { get; set; }
        [Display(Name = "Net Value")]
        [Required(ErrorMessage = "net value is required")]
        public double net_value { get; set; }
        public int net_value_currency_id { get; set; }
        [ForeignKey("net_value_currency_id")]
        public virtual REF_CURRENCY REF_CURRENCY { get; set; }
        [Required(ErrorMessage = "gross value is required")]
        [Display(Name = "Gross Value")]
        public double gross_value { get; set; }
        public int gross_value_currency_id { get; set; }
        [ForeignKey("gross_value_currency_id")]
        public virtual REF_CURRENCY REF_CURRENCY1 { get; set; }
        public int business_unit_id { get; set; }
        [ForeignKey("business_unit_id")]
        public virtual REF_BUSINESS_UNIT REF_BUSINESS_UNIT { get; set; }
        public int plant_id { get; set; }
        [ForeignKey("plant_id")]
        public virtual REF_PLANT REF_PLANT { get; set; }
        public int freight_terms_id { get; set; }
        [ForeignKey("freight_terms_id")]
        public virtual REF_FREIGHT_TERMS REF_FREIGHT_TERMS { get; set; }
        [Display(Name = "Delivery Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? delivery_date { get; set; }
        [Display(Name = "Quotation No.")]
        public string vendor_quotation_no { get; set; }
        [Display(Name = "Quotation Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? vendor_quotation_date { get; set; }
        [Display(Name = "Valid Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? valid_until { get; set; }
        [Display(Name = "Created Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime created_on { get; set; }
        [Display(Name = "Created By")]
        public int created_by { get; set; }
        [Required(ErrorMessage = "billing address is required")]
        [Display(Name = "Billing Address")]
        public string billing_address { get; set; }
        [Required(ErrorMessage = "billing city is required")]
        [Display(Name = "Billing City")]
        public string billing_city { get; set; }
        public int billing_state_id { get; set; }
        [ForeignKey("billing_state_id")]
        public virtual REF_STATE REF_STATE { get; set; }      
        [Display(Name = "Pin Code")]
        public string billing_pin_code { get; set; }
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string email_id { get; set; }

        public string mobile_number { get; set; }
        public int payment_terms_id { get; set; }
        [ForeignKey("payment_terms_id")]
        public virtual REF_PAYMENT_TERMS REF_PAYMENT_TERMS { get; set; }
        public int payment_cycle_id { get; set; }
        [ForeignKey("payment_cycle_id")]
        public virtual REF_PAYMENT_CYCLE REF_PAYMENT_CYCLE { get; set; }
        public int status_id { get; set; }
        [ForeignKey("status_id")]
        public virtual ref_status ref_status { get; set; }
        [Display(Name = "Internal Remarks")]
        [DataType(DataType.MultilineText)]
        public string internal_remarks { get; set; }
        [Display(Name = "Remarks On Document")]
        [DataType(DataType.MultilineText)]
        public string remarks_on_document { get; set; }
        public string attachement { get; set; }
        public bool is_active { get; set; }
        [Display(Name = "PAN No ")]
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
        public string ref_doc_no { get; set; }
        public int delivery_type_id { get; set; }
        public virtual ICollection<pur_po_detail> pur_po_detail { get; set; }
        public bool pi_status { get; set; }
        public bool? order_status { get; set; }
        public bool? is_seen { get; set; }

        public int? approval_status { get; set; }
        public string approval_comments { get; set; }
        public virtual ICollection<pur_po_form> pur_po_form { get; set; }
        public virtual ICollection<pur_po_staggered_delivery> pur_po_staggered_delivery { get; set; }
        public int place_of_supply_id { get; set; }
        public string gst_in { get; set; }
        public int? gst_vendor_type_id { get; set; }
        public bool is_rcm { get; set; }
        public string cancellation_remarks { get; set; }
        public DateTime? created_ts { get; set; }
        public int? modify_by { get; set; }
        public DateTime? modify_ts { get; set; }
        public int? cancelled_by { get; set; }
        public DateTime? cancelled_date { get; set; }
        public int? responsibility_id { get; set; }
        public int? approved_by { get; set; }
        public DateTime? approved_ts { get; set; }
        public string closed_remarks { get; set; }
        public int? closed_by { get; set; }
        public DateTime? closed_ts { get; set; }
        public string vendor_name { get; set; }
        public int? version { get; set; }
        public int? with_without_service_id { get; set; }
        public int? original_po_id { get; set; }
        public bool is_amendment { get; set; }
    }
}
