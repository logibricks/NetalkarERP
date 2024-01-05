using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class pur_srn_vm
    {
        [Key]
        public int srn_id { get; set; }
        [Display(Name = "Purchase Order *")]
        public int po_id { get; set; }
        public string po_no { get; set; }
        [Display(Name = "GRN Number *")]
        public string document_no { get; set; }
        [Display(Name = "Vendor Code *")]
        public int vendor_id { get; set; }
        [ForeignKey("vendor_id")]
        public virtual REF_VENDOR REF_VENDOR { get; set; }
        public string vendor_id1 { get; set; }
        public string vendor_code { get; set; }
        public string vendor_name { get; set; }
        [Display(Name = "Category *")]
        [Required(ErrorMessage = "category is required")]
        public int category_id { get; set; }
        public string category_name { get; set; }
        [ForeignKey("category_id")]
        public virtual REF_CATEGORY REF_CATEGORY { get; set; }
        [Display(Name = "Net Value *")]
        [Required(ErrorMessage = "net value is required")]
        public double net_value { get; set; }
        [Required(ErrorMessage = "net currency is required")]
        public int net_currency_id { get; set; }
        [ForeignKey("net_currency_id")]
        public virtual REF_CURRENCY REF_CURRENCY { get; set; }
        [Display(Name = "Gross Value *")]
        public double gross_value { get; set; }
        [Required(ErrorMessage = "gross currency is required")]
        public int gross_currency_id { get; set; }
        [ForeignKey("gross_currency_id")]
        public virtual REF_CURRENCY REF_CURRENCY1 { get; set; }
        [Display(Name = "Posting Date *")]
        [Required(ErrorMessage = "posting date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime posting_date { get; set; }
        public string BUSINESS_UNIT_DESCRIPTION { get; set; }
        public string business_unit_code { get; set; }
        [Display(Name = "Business Unit *")]
        [Required(ErrorMessage = "business unit is required")]
        public int business_unit_id { get; set; }
        [ForeignKey("business_unit_id")]
        public virtual REF_BUSINESS_UNIT REF_BUSINESS_UNIT { get; set; }
        [Display(Name = "Plant *")]
        [Required(ErrorMessage = "plant is required")]
        public int plant_id { get; set; }
        [ForeignKey("plant_id")]
        public virtual REF_PLANT REF_PLANT { get; set; }
        public string plant_name { get; set; }
        public string plant_code { get; set; }
        [Display(Name = "Freight Terms *")]
        public int freight_terms_id { get; set; }
        [ForeignKey("freight_terms_id")]
        public virtual REF_FREIGHT_TERMS REF_FREIGHT_TERMS { get; set; }
        public string freight_terms { get; set; }
        [Display(Name = "Delivery Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? delivery_date { get; set; }
        [Display(Name = "Vendor Doc Number *")]
        public string vendor_doc_no { get; set; }
        [Display(Name = "Vendor Doc Date *")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? vendor_doc_date { get; set; }
        [Display(Name = "Gate Entry No")]
        public string gate_entry_number { get; set; }
        [Display(Name = "Gate Entry Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? gate_entry_date { get; set; }
        [Display(Name = "Created By ")]
        public int? created_by { get; set; }
        [Display(Name = "Billing Address *")]
        [DataType(DataType.MultilineText)]
        public string billing_address { get; set; }
        [Display(Name = "Billing City *")]
        public string billing_city { get; set; }
        [Display(Name = "Billing State *")]
        public int? billing_state_id { get; set; }
        [ForeignKey("billing_state_id")]
        public virtual REF_STATE REF_STATE { get; set; }
        [Display(Name = "Billing Pin Code ")]
        public string billing_pin_code { get; set; }
        [Display(Name = "Email")]
        public string email_id { get; set; }
        [Display(Name = "Payment Terms *")]
        public int payment_terms_id { get; set; }
        [ForeignKey("payment_terms_id")]
        public virtual REF_PAYMENT_TERMS REF_PAYMENT_TERMS { get; set; }
        [Display(Name = "Payment Cycle *")]
        [Required(ErrorMessage = "payment cycle is required")]
        public int payment_cycle_id { get; set; }
        [ForeignKey("payment_cycle_id")]
        public virtual REF_PAYMENT_CYCLE REF_PAYMENT_CYCLE { get; set; }
        [Display(Name = "Status")]
        [Required(ErrorMessage = "status is required")]
        public int? status_id { get; set; }
        [ForeignKey("status_id")]
        public virtual ref_status ref_status { get; set; }
        [Display(Name = "Internal Remarks")]
        [DataType(DataType.MultilineText)]
        public string internal_remarks { get; set; }
        [Display(Name = "Remarks On Document")]
        [DataType(DataType.MultilineText)]
        public string remarks_on_doc { get; set; }
        [NotMapped]
        public HttpPostedFileBase FileUpload { get; set; }
        public string attachment { get; set; }
        public bool is_active { get; set; }
        [Display(Name = "Payment Cycle Type *")]
        [Required(ErrorMessage = "payment cycle type is required")]
        public int payment_cycle_type_id { get; set; }
        public int? form_id { get; set; }
        [Required(ErrorMessage = "country is required")]
        public int country_id { get; set; }
        public int parment_cycle_type_id { get; set; }
        public String deleteids { get; set; }
        public string country_name { get; set; }
        public string payment_cycle { get; set; }
        public string payment_cycle_type { get; set; }
        public string created_by1 { get; set; }
        public string payment_terms { get; set; }
        public string billing_state { get; set; }
        public string net_currency { get; set; }
        public string gross_currency { get; set; }
        public string purchase_name { get; set; }      
        [Display(Name = "PAN No")]
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
        [Display(Name = "Place of Supply *")]
        public int place_of_supply_id { get; set; }
        [Display(Name = "GSTIN *")]
        public string gst_in { get; set; }
        [Display(Name = "GST Vendor Category *")]
        public int? gst_vendor_type_id { get; set; }
        [Display(Name = "Cancellation Remarks")]
        [DataType(DataType.MultilineText)]
        public string cancellation_remarks { get; set; }
        public int? cancellation_reason_id { get; set; }
        [Display(Name = "Cancelled By")]
        public int? cancelled_by { get; set; }
        [Display(Name = "Cancelled Date")]
        public DateTime? cancelled_date { get; set; }
        [Display(Name = "Last Modified By")]
        public int? modify_by { get; set; }
        [Display(Name = "Last Modified Date ")]
        public DateTime? modify_ts { get; set; }
        [Display(Name = "Created Time ")]
        public DateTime created_ts { get; set; }
        public virtual List<pur_srn_detail> pur_srn_detail { get; set; }
        public List<pur_srn_detail_vm> pur_srn_detail_vm { get; set; }
    }
    public class pur_srn_detail_vm
    {
        public int? srn_detail_id { get; set; }
        public DateTime delivery_date { get; set; }
        public int storage_location_id { get; set; }
        public double quantity { get; set; }
        public int uom_id { get; set; }
        public double unit_price { get; set; }
        public double discount { get; set; }
        public double eff_unit_price { get; set; }
        public double purchase_value { get; set; }       
        public int tax_id { get; set; }
        public bool is_active { get; set; }
        public int srn_id { get; set; }
        public int po_detail_id { get; set; }
        public string user_description { get; set; }       
        public int? hsn_id { get; set; }
        public string hsn_code { get; set; }
        public string storage_location_name { get; set; }
        public string uom_name { get; set; }
        public int item_id { get; set; }
        public string item_code { get; set; }
        public string tax_code { get; set; }
        public int staggerred_delivery_detail_id { get; set; }
    }
}
