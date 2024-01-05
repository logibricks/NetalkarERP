using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class pur_incoming_excise_vm
    {
        [Key]
        public int incoming_excise_id { get; set; }
        [Display(Name = "Number *")]
        
        public int? status_id { get; set; }
        [Display(Name = "Status")]
        public virtual ref_status ref_status { get; set; }
        public string status_name { get; set; }
        public string incoming_excise_number { get; set; }
        [Display(Name = "Posting Date *")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime incoming_excise_posting_date { get; set; }
        [Display(Name = "Tax Document Ref No. *")]
        public string excise_ref_no { get; set; }
        [Display(Name = "Tax Document Ref Date *")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime excise_ref_date { get; set; }
        public string vendor_id1 { get; set; }
        [Display(Name = "Vendor")]
        public int? vendor_id { get; set; }
        [ForeignKey("vendor_id")]
        public virtual REF_VENDOR REF_VENDOR { get; set; }
        [Display(Name = "Category *")]
        public int category_id { get; set; }
        [ForeignKey("category_id")]
        public virtual ref_document_numbring ref_document_numbring { get; set; }
        [Display(Name = "Business Unit")]
        public int? business_unit_id { get; set; }
        [ForeignKey("business_unit_id")]
        public virtual REF_BUSINESS_UNIT REF_BUSINESS_UNIT { get; set; }
        [Display(Name = "Plant")]
        public int? plant_id { get; set; }
        [ForeignKey("plant_id")]
        public virtual REF_PLANT REF_PLANT { get; set; }
        [Display(Name = "Freight Terms")]
        public int? freight_terms_id { get; set; }
        [ForeignKey("freight_terms_id")]
        public virtual REF_FREIGHT_TERMS REF_FREIGHT_TERMS { get; set; }
        [Display(Name = "Delivery Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? delivery_date { get; set; }
        [Display(Name = "Vendor Document Number")]
        public string vendor_doc_no { get; set; }
        [Display(Name = "Vendor Document Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? vendor_doc_date { get; set; }
        [Display(Name = "Gate Entry Number")]
        public string gate_entry_number { get; set; }
        [Display(Name = "Gate Entry Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? gate_entry_date { get; set; }
        [Display(Name = "Created By")]
        public int? created_by { get; set; }
        [Display(Name = "Billing Address")]
        [DataType(DataType.MultilineText)]
        public string billing_address { get; set; }
        [Display(Name = "Billing City")]
        public string billing_city { get; set; }
        [Display(Name = "Billing State")]
        public int? billing_state_id { get; set; }
        [ForeignKey("billing_state_id")]
        public virtual REF_STATE REF_STATE { get; set; }
        [Display(Name ="Billing Country")]
        public int? country_id { get; set; }
        [ForeignKey("country_id")]
        public virtual REF_COUNTRY REF_COUNTRY { get; set; }
        [Display(Name = "Billing Pin Code")]
        public string billing_pin_code { get; set; }
        [Display(Name = "Email")]
        public string email_id { get; set; }
        [Display(Name = "Payment Terms")]
        public int? payment_terms_id { get; set; }
        [ForeignKey("payment_terms_id")]
        public virtual REF_PAYMENT_TERMS REF_PAYMENT_TERMS { get; set; }
        [Display(Name = "Payment Cycle Type")]
        public int? payment_cycle_type_id { get; set; }
        [Display(Name = "Payment Cycle")]
        public int? payment_cycle_id { get; set; }
        [ForeignKey("payment_cycle_id")]
        public virtual REF_PAYMENT_CYCLE REF_PAYMENT_CYCLE { get; set; }
        public bool is_active { get; set; }
        [Display(Name = "GRN No *")]       
        public int? grn_id { get; set; }
        [ForeignKey("grn_id")]
        public virtual pur_grn pur_grn { get; set; }
        [Display(Name = "Net Value *")]
        public double incoming_excise_net_value { get; set; }
        public int? net_currency_id { get; set; }
        [ForeignKey("net_currency_id")]
        public virtual REF_CURRENCY REF_CURRENCY_net { get; set; }
        [Display(Name = "Gross Value")]
        public double? incoming_excise_gross_value { get; set; }
        public int? gross_currency_id { get; set; }
        [ForeignKey("gross_currency_id")]
        public virtual REF_CURRENCY REF_CURRENCY_gross { get; set; }
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
        public string item_id1 { get; set; }
        public string tax_id1 { get; set; }
        public string document_numbring_name { get; set; }
        public string business_unit_name { get; set; }
        public string plant_name { get; set; }
        public string freight_terms_name { get; set; }
        public string state_name { get; set; }
        public string country_name { get; set; }
        public string payment_cycle_name { get; set; }
        public string payment_terms_name { get; set; }
        public string payment_cycle_type_name { get; set; }
        public string net_currency_name { get; set; }
        public string gross_currency_name { get; set; }
        public string  detailitems { get; set; }
        public string grn_no { get; set; }
        public string category_name { get; set; }
        public string vendor_name { get; set; }
        public virtual List<pur_incoming_excise_detail> pur_incoming_excise_detail { get; set; }
        public List<string> incoming_excise_detail_id1 { get; set; }
        public List<string> grn_detail_id1 { get; set; }
        public List<string> item_id11 { get; set; }
        public List<string> delivery_date1 { get; set; }
        public List<string> storage_location_id1 { get; set; }
        public List<string> quantity1 { get; set; }
        public List<string> uom_id1 { get; set; }
        public List<string> unit_price1 { get; set; }
        public List<string> discount1 { get; set; }
        public List<string> eff_unit_price1 { get; set; }
        public List<string> purchase_value1 { get; set; }
        public List<string> assessable_rate1 { get; set; }
        public List<string> assessable_value1 { get; set; }
        public List<string> tax_id11 { get; set; }
        public string business_code { get; set; }
        public string vendor_code { get; set; }
        public string plant_code { get; set; }
        public string business_name { get; set; }
        public List<string> sr_no { get; set; }
        public List<string> tax_value { get; set; }
        public List<string> srno { get; set; }
        public List<string> tax_element_id { get; set; }
        public List<string> assessable_rate { get; set; }
        public List<string> assessable_value { get; set; }
        public List<string> tax_element_rate { get; set; }
        public List<string> tax_element_value { get; set; }
        [Display(Name = "Internal Remarks")]
        [DataType(DataType.MultilineText)]
        public string internal_remarks { get; set; }
        [Display(Name = "Remarks On Document")]
        [DataType(DataType.MultilineText)]
        public string remarks_on_doc { get; set; }
        [NotMapped]
        //public HttpPostedFileBase FileUpload { get; set; }
        public int? cancellation_reason_id { get; set; }

        [Display(Name = "Cancellation Remarks")]
        [DataType(DataType.MultilineText)]
        public string cancellation_remarks { get; set; }
        public string attachment { get; set; }
        public DateTime created_ts { get; set; }
        public int? modify_by { get; set; }
        public DateTime? modify_ts { get; set; }
        public int? cancelled_by { get; set; }
        public DateTime? cancelled_date { get; set; }
    }
    public class pur_incoming_excise_report_vm
    {
        public string Doc_Category { get; set; }
        public string Doc_No { get; set; }
        public DateTime Doc_PostingDate { get; set; }
        public DateTime GRNDate { get; set; }
        public string Vendor_Code { get; set; }
        public string VendorName { get; set; }
        public string excise_refno { get; set; }
        public DateTime excise_date { get; set; }
        public string Vendor_ECC { get; set; }
        public string Purchase_Order { get; set; }
        public string Company_display_Name { get; set; }
        public string Regd_Address { get; set; }
        public string PlantName_Details { get; set; }
        public string GRN_DocNo { get; set; }
        public int iexid { get; set; }
    }
    public class pur_incoming_excise_tax
    {
        public int sr_no { get; set; }
        public int tax_element_id { get; set; }
        public string tax_element_name { get; set; }
        public decimal assessable_rate { get; set; }
        public decimal assessable_value { get; set; }
        public decimal tax_rate { get; set; }
        public decimal tax_value { get; set; }
    }
}
