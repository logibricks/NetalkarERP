using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class REF_CUSTOMER_VM
    {
        public int CUSTOMER_ID { get; set; }
        [Display(Name = "Customer Category *")]
        public int CUSTOMER_CATEGORY_ID { get; set; }
        //[ForeignKey("CUSTOMER_CATEGORY_ID")]
       // public virtual REF_CUSTOMER_CATEGORY REF_CUSTOMER_CATEGORY { get; set; }
        [Display(Name = "Has Parent")]
        public bool HAS_PARENT { get; set; }
        public int? CUSTOMER_PARENT_ID { get; set; }
        //[ForeignKey("CUSTOMER_PARENT_ID")]
        //public virtual REF_CUSTOMER_PARENT REF_CUSTOMER_PARENT { get; set; }
        [Required]
        [Display(Name = "Customer Code *")]
        public string CUSTOMER_CODE { get; set; }
        [Required]
        [Display(Name = "Customer Name *")]
        public string CUSTOMER_NAME { get; set; }
        [Required]
        [Display(Name = "Customer Display Name *")]
        public string CUSTOMER_DISPLAY_NAME { get; set; }
        [Display(Name = "Organization Type *")]
        public int ORG_TYPE_ID { get; set; }
        //[ForeignKey("ORG_TYPE_ID")]
        //public virtual REF_ORG_TYPE REF_ORG_TYPE { get; set; }
        [Display(Name = "Priority")]
        public int? PRIORITY_ID { get; set; }
        //[ForeignKey("PRIORITY_ID")]
        //public virtual REF_PRIORITY REF_PRIORITY { get; set; }
        [Display(Name = "Territory")]
        public int? TERRITORY_ID { get; set; }
        [Display(Name = "Billing Address *")]
        [DataType(DataType.MultilineText)]
        [Required]
        public string BILLING_ADDRESS { get; set; }
        [Display(Name = "Billing City *")]
        [Required]
        public string BILLING_CITY { get; set; }
        [Display(Name = "Billing Pincode *")]
        [Range(100000, 999999, ErrorMessage = "Please enter correct Pincode.")]
        [Required]
        public int? BILLING_PINCODE { get; set; }
        [Display(Name = "Billing Country *")]
        public int BILLING_COUNTRY_ID { get; set; }
        //[ForeignKey("BILLING_COUNTRY_ID")]
        //public virtual REF_COUNTRY REF_COUNTRY { get; set; }
        [Display(Name = "Billing State *")]
        public int BILLING_STATE_ID { get; set; }
        //[ForeignKey("BILLING_STATE_ID")]
        //public virtual REF_STATE REF_STATE { get; set; }
        [Display(Name = "Correspondence Address *")]
        [DataType(DataType.MultilineText)]
        [Required]
        public string CORR_ADDRESS { get; set; }
        [Display(Name = "Correspondence City *")]
        [Required]
        public string CORR_CITY { get; set; }
        [Display(Name = "Correspondence Pincode *")]
        [Range(100000, 999999, ErrorMessage = "Please enter correct Pincode.")]
        [Required]
        public int? CORR_PINCODE { get; set; }
        [Display(Name = "Correspondence Country *")]
        public int CORR_COUNTRY_ID { get; set; }
        //[ForeignKey("CORR_COUNTRY_ID")]
        //public virtual REF_COUNTRY REF_COUNTRY1 { get; set; }
        [Display(Name = "Correspondence State *")]
        public int CORR_STATE_ID { get; set; }
        //[ForeignKey("CORR_STATE_ID")]
        //public virtual REF_STATE REF_STATE1 { get; set; }
        [Display(Name = "Additional Information")]
        [DataType(DataType.MultilineText)]
        public string ADDITIONAL_INFO { get; set; }
        [Display(Name = "Email")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]

        public string EMAIL_ID_PRIMARY { get; set; }
        [Display(Name = "Phone")]
        public string std_code { get; set; }
        [Display(Name = "Phone")]
        public string TELEPHONE_PRIMARY { get; set; }
        [Display(Name = "Fax")]
        public string FAX { get; set; }
        [Display(Name = "Website")]
        public string WEBSITE_ADDRESS { get; set; }
        [Display(Name = "Sales RM")]
        public int? SALES_EXEC_ID { get; set; }
        [Display(Name = "Payment Terms *")]
        public int PAYMENT_TERMS_ID { get; set; }
        //[ForeignKey("PAYMENT_TERMS_ID")]
        //public virtual REF_PAYMENT_TERMS REF_PAYMENT_TERMS { get; set; }
        [Display(Name = "Credit Limit")]
        public double? CREDIT_LIMIT { get; set; }
        public int CREDIT_LIMIT_CURRENCY_ID { get; set; }
        //[ForeignKey("CREDIT_LIMIT_CURRENCY_ID")]
        //public virtual REF_CURRENCY REF_CURRENCY { get; set; }
        [Display(Name = "Blocked")]
        public bool IS_BLOCKED { get; set; }
        [Display(Name = "Payment Cycle Type *")]
        public int PAYMENT_CYCLE_TYPE_ID { get; set; }
        //[ForeignKey("PAYMENT_CYCLE_TYPE_ID")]
        //public virtual REF_PAYMENT_CYCLE_TYPE REF_PAYMENT_CYCLE_TYPE { get; set; }
        [Display(Name = "Payment Cycle *")]
        public int PAYMENT_CYCLE_ID { get; set; }
        //[ForeignKey("PAYMENT_CYCLE_ID")]
        //public virtual REF_PAYMENT_CYCLE REF_PAYMENT_CYCLE { get; set; }
        [Display(Name = "TDS Applicable")]
        public bool TDS_APPLICABLE { get; set; }
        [Display(Name = "Overall Discount %")]
        public double? OVERALL_DISCOUNT { get; set; }
        [Display(Name = "Freight Terms *")]
        public int FREIGHT_TERMS_ID { get; set; }
        //[ForeignKey("FREIGHT_TERMS_ID")]
        //public virtual REF_FREIGHT_TERMS REF_FREIGHT_TERMS { get; set; }
        [Display(Name = "Created on")]
        public DateTime? CREATED_ON { get; set; }
        public int? CREATED_BY { get; set; }
        //[ForeignKey("CREATED_BY")]
        //public virtual REF_USER REF_USER1 { get; set; }
        public virtual List<REF_CUSTOMER_CONTACTS> REF_CUSTOMER_CONTACTS { get; set; }
        public virtual List<ref_customer_item_group> ref_customer_item_group { get; set; }
        [Display(Name = "PAN No *")]
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
        public string attachment { get; set; }
        [NotMapped]
        public HttpPostedFileBase FileUpload { get; set; }
        public string deleteids { get; set; }
        public string ledgeraccounttype { get; set; }
        [Display(Name = "Bank")]
        public int? bank_id { get; set; }
        [Display(Name = "Bank Account Number")]
        public string bank_account_number { get; set; }
        [Display(Name = "IFSC Code")]
        public string ifsc_code { get; set; }
        [Display(Name = "Commisionerate")]
        public string commisionerate { get; set; }
        [Display(Name = "Range")]
        public string range { get; set; }
        [Display(Name = "Division")]
        public string division { get; set; }
        [Display(Name ="Vendor Code")]
        public string vendor_code { get; set; }
        [Display(Name = "GST Customer Type *")]
        public int? gst_customer_type_id { get; set; }
        [Display(Name = "Date of Registration *")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? gst_registration_date { get; set; }
        [Display(Name = "GST TDS Applicable")]
        public bool gst_tds_applicable { get; set; }
        [Display(Name = "GST TDS")]
        public int? gst_tds_id { get; set; }
        public List<string> gl_ledger_id { get; set; }
        public List<string> ledger_account_type_id { get; set; }
        public List<string> item_category_id { get; set; }
        public List<string> rate { get; set; }
    }

    public class customer_excel
    {
        public int CUSTOMER_ID { get; set; }
        public int CUSTOMER_CATEGORY_ID { get; set; }
        public bool HAS_PARENT { get; set; }
        public int? CUSTOMER_PARENT_ID { get; set; }
        public string CUSTOMER_CODE { get; set; }
        public string CUSTOMER_NAME { get; set; }
        public string CUSTOMER_DISPLAY_NAME { get; set; }
        public int ORG_TYPE_ID { get; set; }
        public int? PRIORITY_ID { get; set; }
        public int? TERRITORY_ID { get; set; }
        public string BILLING_ADDRESS { get; set; }
        public string BILLING_CITY { get; set; }
        public int? BILLING_PINCODE { get; set; }
        public int BILLING_STATE_ID { get; set; }
        public string CORR_ADDRESS { get; set; }
        public string CORR_CITY { get; set; }
        public int? CORR_PINCODE { get; set; }
        public int CORR_STATE_ID { get; set; }
        public string ADDITIONAL_INFO { get; set; }
        public string EMAIL_ID_PRIMARY { get; set; }
        public string std_code { get; set; }
        public string TELEPHONE_PRIMARY { get; set; }
        public string FAX { get; set; }
        public string WEBSITE_ADDRESS { get; set; }
        public int? SALES_EXEC_ID { get; set; }
        public int PAYMENT_TERMS_ID { get; set; }
        public double? CREDIT_LIMIT { get; set; }
        public int CREDIT_LIMIT_CURRENCY_ID { get; set; }
        public bool IS_BLOCKED { get; set; }
        public int PAYMENT_CYCLE_TYPE_ID { get; set; }
        public int PAYMENT_CYCLE_ID { get; set; }
        public bool TDS_APPLICABLE { get; set; }
        public double? OVERALL_DISCOUNT { get; set; }
        public int FREIGHT_TERMS_ID { get; set; }
        public DateTime? CREATED_ON { get; set; }
        public int? CREATED_BY { get; set; }
        public string pan_no { get; set; }
        public string ecc_no { get; set; }
        public string vat_tin_no { get; set; }
        public string cst_tin_no { get; set; }
        public string service_tax_no { get; set; }
        public string gst_no { get; set; }
        public bool tds_applicable { get; set; }
        public int? tds_id { get; set; }
        public string attachment { get; set; }
        public HttpPostedFileBase FileUpload { get; set; }
        public int bank_id { get; set; }
        public string bank_account_code { get; set; }
        public int account_type_id { get; set; }
        public string bank_account_no { get; set; }
        public string ifsc_code { get; set; }
        public string commisionerate { get; set; }
        public string range { get; set; }
        public string division { get; set; }
        public string vendor_code { get; set; }
    }
    public class contact_excel
    {
        public int? CUSTOMER_CONTACT_ID { get; set; }
        public string CUSTOMER_CODE { get; set; }
        public int CUSTOMER_ID { get; set; }
        public string CONTACT_NAME { get; set; }
        public string DESIGNATION { get; set; }
        public string MOBILE_NO { get; set; }
        public string PHONE_NO { get; set; }
        public bool SEND_SMS_FLAG { get; set; }
        public bool SEND_EMAIL_FLAG { get; set; }
        public bool? IS_ACTIVE { get; set; }
        public string EMAIL_ADDRESS { get; set; }
    }
    public class item_excel
    {
        public int customer_id { get; set; }
        public string CUSTOMER_CODE { get; set; }
        public virtual REF_CUSTOMER REF_CUSTOMER { get; set; }
        public int item_category_id { get; set; }
        public double rate { get; set; }
    }
    public class gl_excel
    {
        public int customer_id { get; set; }
        public string CUSTOMER_CODE { get; set; }
        public int ledger_account_type_id { get; set; }
        public int? gl_ledger_id { get; set; }
    }
    public class duplicateglexcle
    {
        public string CUSTOMER_CODE { get; set; }
    }
}
