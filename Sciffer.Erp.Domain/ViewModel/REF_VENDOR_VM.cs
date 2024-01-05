using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace Sciffer.Erp.Domain.ViewModel
{
     public class REF_VENDOR_VM
    {
        public int VENDOR_ID { get; set; }
        [Required(ErrorMessage ="category is required")]
        [Display(Name = "Vendor Category *")]
        public int VENDOR_CATEGORY_ID { get; set; }
        [ForeignKey("VENDOR_CATEGORY_ID")]
        public virtual REF_VENDOR_CATEGORY REF_VENDOR_CATEGORY { get; set; }
        [Display(Name = "Has Parent")]
        [Required(ErrorMessage = "parent is required")]
        public bool HAS_PARENT { get; set; }
        [Display(Name = "Vendor Parent")]
        public int? VENDOR_PARENT_ID { get; set; }
        [ForeignKey("VENDOR_PARENT_ID")]
        public virtual REF_VENDOR_PARENT REF_VENDOR_PARENT { get; set; }
        [Display(Name = "Vendor Code *")]
        [Required(ErrorMessage ="vendor code is required")]
        public string VENDOR_CODE { get; set; }
        [Required]
        [Display(Name = "Vendor Name *")]
        public string VENDOR_NAME { get; set; }
        [Required]
        [Display(Name = "Vendor Display Name *")]
        public string VENDOR_DISPLAY_NAME { get; set; }
        [Display(Name = "Organization Type *")]
        public int ORG_TYPE_ID { get; set; }
        [ForeignKey("ORG_TYPE_ID")]
        public virtual REF_ORG_TYPE REF_ORG_TYPE { get; set; }
        [Display(Name = "Priority")]
        public int? PRIORITY_ID { get; set; }
        [ForeignKey("PRIORITY_ID")]
        public virtual REF_PRIORITY REF_PRIORITY { get; set; }       
        [Display(Name = "Billing Address *")]
        [DataType(DataType.MultilineText)]
        [Required]
        public string BILLING_ADDRESS { get; set; }
        [Display(Name = "Billing City *")]
        [Required]
        public string BILLING_CITY { get; set; }
        [Display(Name = "Billing Pincode ")]
        public string BILLING_PINCODE { get; set; }
        [Display(Name = "Billing Country *")]
        [Required(ErrorMessage ="billing country is required")]
        public int BILLING_COUNTRY_ID { get; set; }
        [ForeignKey("BILLING_COUNTRY_ID")]
        public virtual REF_COUNTRY REF_COUNTRY { get; set; }
        [Display(Name ="Billing State *")]
        [Required(ErrorMessage = "billing state is required")]
        public int BILLING_STATE_ID { get; set; }
        [ForeignKey("BILLING_STATE_ID")]
        public virtual REF_STATE REF_STATE { get; set; }
        [Display(Name = "Correspondence Address *")]
        [DataType(DataType.MultilineText)]
        [Required]
        public string CORR_ADDRESS { get; set; }
        [Display(Name = "Correspondence City *")]
        [Required]
        public string CORR_CITY { get; set; }
        [Display(Name = "Correspondence Pincode ")]      
        public string CORR_PINCODE { get; set; }
        [Required]
        [Display(Name = "Correspondence Country *")]
        public int CORR_COUNTRY_ID { get; set; }
        [ForeignKey("CORR_COUNTRY_ID")]
        public virtual REF_COUNTRY REF_COUNTRY1 { get; set; }
        [Display(Name = "Correspondence State *")]
        [Required]
        public int CORR_STATE_ID { get; set; }
        [ForeignKey("CORR_STATE_ID")]
        public virtual REF_STATE REF_STATE1 { get; set; }
        [Display(Name = "Additional Information")]
        [DataType(DataType.MultilineText)]
        public string ADDITIONAL_INFO { get; set; }
        [Display(Name = "Email")]    
      
        public string EMAIL_ID_PRIMARY { get; set; }
        [Display(Name = "Phone")]
        public string TELEPHONE_PRIMARY { get; set; }
        [Display(Name = "Fax")]
        public string FAX { get; set; }
        [Display(Name = "Website")]       
        public string WEBSITE_ADDRESS { get; set; }
        [Display(Name = "Payment Terms *")]
        [Required]
        public int PAYMENT_TERMS_ID { get; set; }
        [ForeignKey("PAYMENT_TERMS_ID")]
        public virtual REF_PAYMENT_TERMS REF_PAYMENT_TERMS { get; set; }
        [Display(Name = "Credit Limit")]
        public double? CREDIT_LIMIT { get; set; }
        [Display(Name = "Default Currency")]
      
        public int CREDIT_LIMIT_CURRENCY_ID { get; set; }
        [ForeignKey("CREDIT_LIMIT_CURRENCY_ID")]
        public virtual REF_CURRENCY REF_CURRENCY { get; set; }
        [Display(Name = "Blocked")]
        public bool IS_BLOCKED { get; set; }
        [Display(Name = "Payment Cycle Type *")]
        [Required]
        public int PAYMENT_CYCLE_TYPE_ID { get; set; }
        [ForeignKey("PAYMENT_CYCLE_TYPE_ID")]
        public virtual REF_PAYMENT_CYCLE_TYPE REF_PAYMENT_CYCLE_TYPE { get; set; }
        [Display(Name = "Payment Cycle *")]
        [Required]
        public int PAYMENT_CYCLE_ID { get; set; }
        [ForeignKey("PAYMENT_CYCLE_ID")]
        public virtual REF_PAYMENT_CYCLE REF_PAYMENT_CYCLE { get; set; }
        [Display(Name = "TDS Applicable")]
       
        public bool TDS_APPLICABLE { get; set; }
        [Display(Name = "Freight Terms *")]
        [Required]
        public int FREIGHT_TERMS_ID { get; set; }
        [ForeignKey("FREIGHT_TERMS_ID")]
        public virtual REF_FREIGHT_TERMS REF_FREIGHT_TERMS { get; set; }
        [Display(Name = "Created on")]
        public DateTime CREATED_ON { get; set; }
        [Display(Name = "Created by")]
        public int? CREATED_BY { get; set; }
        public virtual REF_USER REF_USER1 { get; set; }
        public virtual List<REF_VENDOR_CONTACTS> REF_VENDOR_CONTACTS { get; set; }
        //public virtual List<ref_vendor_item_category> ref_vendor_item_category { get; set; }
        public virtual List<ref_vendor_item_group> ref_vendor_item_group { get; set; }
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
        [Display(Name = "GSTIN *")]
        public string gst_no { get; set; }
        [Display(Name = "TDS Number")]
        public int? tds_id { get; set; }
        [Display(Name = "Overall Discount")]
        public double? overall_discount { get; set; }
        public string phone_code { get; set; }
        [Display(Name = "Down Payment Account *")]       
        public string attachment { get; set; }
        [NotMapped]
        public HttpPostedFileBase FileUpload { get; set; }
        public string ledgeraccounttype { get; set; }
        public int? bank_id { get; set; }
        public string bank_account_number { get; set; }
        public string ifsc_code { get; set; }
        [Display(Name = "GST Vendor Type *")]
        public int? gst_vendor_type_id { get; set; }
        [Display(Name = "Date of Registration *")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? gst_registration_date { get; set; }
        [Display(Name = "GST TDS Applicable")]
        public bool gst_tds_applicable { get; set; }
        [Display(Name = "GST TDS")]
        public int? gst_tds_id { get; set; }
        public string deleteids { get; set; }
        public List<string> gl_ledger_id { get; set; }
        public List<string> ledger_account_type_id { get; set; }
        public List<string> item_category_id { get; set; }
        public List<string> rate { get; set; }
        public string VENDOR_CODE_TEXT { get; set; }
        public REF_VENDOR_VM()
        {
            this.REF_STATE = new REF_STATE();
            REF_VENDOR_CATEGORY = new REF_VENDOR_CATEGORY();
        }
    }
    public class VendorVM
    {
        public int vendor_id { get; set; }
        public string vendor_category_name { get; set; }
        public string vendor_code { get; set; }
        public string vendor_name { get; set; }
        public string vendor_display_name { get; set; }
        public string org_type { get; set; }
        public string priority { get; set; }
        public string billing_address { get; set; }
        public string billing_city { get; set; }
        public string billing_pincode { get; set; }
        public string billing_state { get; set; }
        public string billing_country { get; set; }
        public string corr_address { get; set; }
        public string corr_city { get; set; }
        public string corr_pincode { get; set; }
        public string corr_state { get; set; }
        public string corr_country { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string fax { get; set; }
        public string website { get; set; }
        public string payment_terms { get; set; }
        public string payment_cycle_type { get; set; }
        public string payment_cycle { get; set; }
        public string freight_terms { get; set; }
        public double? credit_limit { get; set; }
        public string pan_no { get; set; }
        public string ecc_no { get; set; }
        public string vat_tin_no { get; set; }
        public string cst_tin_no { get; set; }
        public string service_tax_no { get; set; }
        public string gst_no { get; set; }
        public int? tds_id { get; set; }
        public string attachment { get; set; }      
        public double? overall_discount { get; set; }
        public int bank_id { get; set; }
        public string bank_account_number { get; set; }
        public string ifsc_code { get; set; }
        public bool has_parent { get; set; }
        public string vendor_parent_name { get; set; }
        public bool blocked { get; set; }
        public string bank_name { get; set; }
        public string telephone_code { get; set; }
        public string telephone { get; set; }
        public string default_currency { get; set; }
        public bool tds_applicable { get; set; }
        public string tds_name { get; set; }
        public string tds_desc { get; set; }
        public string aditional_info { get; set; }
        public DateTime? gst_registration_date { get; set; }
        public string gst_vendor_type_name { get; set; }
        public string gst_tds_code { get; set; }

        public int payment_terms_id { get; set; }
        public int payment_cycle_id { get; set; }
        public int billing_state_id { get; set; }
        public int payment_cycle_type_id { get; set; }
        public int billing_country_id { get; set; }
        public int default_currency_id { get; set; }
    }
    public class vendor_excel
    {
        public int VENDOR_ID { get; set; }
        public int VENDOR_CATEGORY_ID { get; set; }
        public bool HAS_PARENT { get; set; }
        public int? VENDOR_PARENT_ID { get; set; }
        public string VENDOR_CODE { get; set; }
        public string VENDOR_NAME { get; set; }
        public string VENDOR_DISPLAY_NAME { get; set; }
        public int ORG_TYPE_ID { get; set; }
        public int? PRIORITY_ID { get; set; }
        public string BILLING_ADDRESS { get; set; }
        public string BILLING_CITY { get; set; }
        public int BILLING_PINCODE { get; set; }
        public int BILLING_STATE_ID { get; set; }
        public string CORR_ADDRESS { get; set; }
        public string CORR_CITY { get; set; }
        public int CORR_PINCODE { get; set; }
        public int CORR_STATE_ID { get; set; }
        public string ADDITIONAL_INFO { get; set; }
        public string EMAIL_ID_PRIMARY { get; set; }
        public string TELEPHONE_PRIMARY { get; set; }
        public string FAX { get; set; }
        public string WEBSITE_ADDRESS { get; set; }
        public int PAYMENT_TERMS_ID { get; set; }
        public int PAYMENT_CYCLE_TYPE_ID { get; set; }
        public double? CREDIT_LIMIT { get; set; }
        public int CREDIT_LIMIT_CURRENCY_ID { get; set; }
        public bool IS_BLOCKED { get; set; }
        public int PAYMENT_CYCLE_ID { get; set; }
        public bool TDS_APPLICABLE { get; set; }
        public int FREIGHT_TERMS_ID { get; set; }
        public DateTime CREATED_ON { get; set; }
        public int? CREATED_BY { get; set; }
        public string pan_no { get; set; }
        public string ecc_no { get; set; }
        public string vat_tin_no { get; set; }
        public string cst_tin_no { get; set; }
        public string service_tax_no { get; set; }
        public string gst_no { get; set; }
        public int tds_id { get; set; }
        public string phone_code { get; set; }
        public string attachment { get; set; }
        public double? overall_discount { get; set; }
        public HttpPostedFileBase FileUpload { get; set; }
        public int? bank_id { get; set; }
        public string bank_account_number { get; set; }
        public string ifsc_code { get; set; }

    }
    public class vendor_contact_excel
    {
        public int? VENDOR_CONTACT_ID { get; set; }
        public string VENDOR_CODE { get; set; }
        public int VENDOR_ID { get; set; }
        public string CONTACT_NAME { get; set; }
        public string DESIGNATION { get; set; }
        public string MOBILE_NO { get; set; }
        public string PHONE_NO { get; set; }
        public bool SEND_SMS_FLAG { get; set; }
        public bool SEND_EMAIL_FLAG { get; set; }
        public bool? IS_ACTIVE { get; set; }
        public string EMAIL_ADDRESS { get; set; }
    }
    public class vendor_item_excel
    {
        public int vendor_id { get; set; }
        public string VENDOR_CODE { get; set; }
        public int item_category_id { get; set; }
        public double rate { get; set; }
    }
    public class vendor_gl_excel
    {
        public int vendor_id { get; set; }
        public string VENDOR_CODE { get; set; }
        public int ledger_account_type_id { get; set; }
        public int? gl_ledger_id { get; set; }
    }
    public class vendor_duplicateglexcle
    {
        public string VENDOR_CODE { get; set; }
    }
}
