using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sciffer.Erp.Domain.Model
{
   public  class REF_VENDOR
    {
        [Key]
        public int VENDOR_ID { get; set; }
        [Required]
        public int VENDOR_CATEGORY_ID { get; set; }
        [ForeignKey("VENDOR_CATEGORY_ID")]
        public virtual REF_VENDOR_CATEGORY REF_VENDOR_CATEGORY { get; set; }
        [Display(Name = "Has Parent")]
        public bool HAS_PARENT { get; set; }
        public int? VENDOR_PARENT_ID { get; set; }
        [ForeignKey("VENDOR_PARENT_ID")]
        public virtual REF_VENDOR_PARENT REF_VENDOR_PARENT { get; set; }
        [Required]
        [Display(Name = "Vendor Code")]
        public string VENDOR_CODE { get; set; }
        [Required]
        [Display(Name = "Vendor Name")]
        public string VENDOR_NAME { get; set; }
        [Required]
        [Display(Name = "Vendor Display Name")]
        public string VENDOR_DISPLAY_NAME { get; set; }
        public int ORG_TYPE_ID { get; set; }
        [ForeignKey("ORG_TYPE_ID")]
        public virtual REF_ORG_TYPE REF_ORG_TYPE { get; set; }
        public int? PRIORITY_ID { get; set; }
        [ForeignKey("PRIORITY_ID")]
        public virtual REF_PRIORITY REF_PRIORITY { get; set; }       
        [Display(Name = "Billing Address")]
        [DataType(DataType.MultilineText)]
        [Required]
        public string BILLING_ADDRESS { get; set; }
        [Display(Name = "Billing City")]
        [Required]
        public string BILLING_CITY { get; set; }
        [Display(Name = "Billing Pincode")]      
        public string BILLING_PINCODE { get; set; }
        public int BILLING_STATE_ID { get; set; }
        [ForeignKey("BILLING_STATE_ID")]
        public virtual REF_STATE REF_STATE { get; set; }
        [Display(Name = "Correspondence Address")]
        [DataType(DataType.MultilineText)]
        public string CORR_ADDRESS { get; set; }
        [Display(Name = "Correspondence City")]
        [Required]
        public string CORR_CITY { get; set; }
        [Display(Name = "Correspondence Pincode")]      
        public string CORR_PINCODE { get; set; }
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
        public int PAYMENT_TERMS_ID { get; set; }
        [ForeignKey("PAYMENT_TERMS_ID")]
        public virtual REF_PAYMENT_TERMS REF_PAYMENT_TERMS { get; set; }
        [Display(Name = "Credit Limit")]
        public double? CREDIT_LIMIT { get; set; }
        public int CREDIT_LIMIT_CURRENCY_ID { get; set; }
        [ForeignKey("CREDIT_LIMIT_CURRENCY_ID")]
        public virtual REF_CURRENCY REF_CURRENCY { get; set; }
        [Display(Name = "IS Block")]
        public bool IS_BLOCKED { get; set; }
        public int PAYMENT_CYCLE_ID { get; set; }
        [ForeignKey("PAYMENT_CYCLE_ID")]
        public virtual REF_PAYMENT_CYCLE REF_PAYMENT_CYCLE { get; set; }
        [Display(Name = "TDS Applicable")]
        public bool TDS_APPLICABLE { get; set; }
        public int FREIGHT_TERMS_ID { get; set; }
        [ForeignKey("FREIGHT_TERMS_ID")]
        public virtual REF_FREIGHT_TERMS REF_FREIGHT_TERMS { get; set; }
        [Display(Name = "Created on")]
        public DateTime CREATED_ON { get; set; }
        [Display(Name = "Created by")]
        public int? CREATED_BY { get; set; }
        public virtual ICollection<REF_VENDOR_CONTACTS> REF_VENDOR_CONTACTS { get; set; }       
        public string pan_no { get; set; }
        public string ecc_no { get; set; }
        public string vat_tin_no { get; set; }
        public string cst_tin_no { get; set; }
        public string service_tax_no { get; set; }
        public string gst_no { get; set; }
        public int? tds_id { get; set; }
        public string phone_code { get; set; }
        public string attachment { get; set; }
        public int bank_id { get; set; }       
        public string bank_account_number { get; set; }
        public string ifsc_code { get; set; }
        public int? gst_vendor_type_id { get; set; }
        public DateTime? gst_registration_date { get; set; }
        public double? overall_discount { get; set; }
        public bool gst_tds_applicable { get; set; }
        public int? gst_tds_id { get; set; }

        public virtual ICollection<ref_vendor_item_group> ref_vendor_item_group { get; set; }
    }
}
