using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class SAL_QUOTATION
    {
        [Key]
        public int QUOTATION_ID { get; set; }
        public int SALES_CATEGORY_ID { get; set; }
        [ForeignKey("SALES_CATEGORY_ID")]
        public virtual REF_SALES_CATEGORY REF_SALES_CATEGORY { get; set; }
        [Display(Name ="Quotation Number")]
        public string QUOTATION_NUMBER { get; set; }
        [Display(Name = "Quotation Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime QUOTATION_DATE { get; set; }
        public int? BUYER_ID { get; set; }
        [ForeignKey("BUYER_ID")]
        public virtual REF_CUSTOMER REF_CUSTOMER { get; set; }
        public int? CONSIGNEE_ID { get; set; }
        [ForeignKey("CONSIGNEE_ID")]
        public virtual REF_CUSTOMER REF_CUSTOMER1 { get; set; }
        [Display(Name ="Net Value")]
        public double SAL_NET_VALUE { get; set; }
        public int NET_VALUE_CURRENCY_ID { get; set; }
        [ForeignKey("NET_VALUE_CURRENCY_ID")]
        public virtual REF_CURRENCY REF_CURRENCY { get; set; }
        [Display(Name = "Gross Value")]
        public double SAL_GROSS_VALUE { get; set; }
        public int GROSS_VALUE_CURRENCY_ID { get; set; }
        [ForeignKey("GROSS_VALUE_CURRENCY_ID")]
        public virtual REF_CURRENCY REF_CURRENCY1 { get; set; }
        public int BUSINESS_UNIT_ID { get; set; }
        public virtual REF_BUSINESS_UNIT REF_BUSINESS_UNIT { get; set; }
        public int PLANT_ID { get; set; }
        public virtual REF_PLANT REF_PLANT { get; set; }
        public int FREIGHT_TERMS_ID { get; set; }
        public virtual REF_FREIGHT_TERMS REF_FREIGHT_TERMS { get; set; }
        public int? TERRITORY_ID { get; set; }
        public virtual REF_TERRITORY REF_TERRITORY { get; set; }
        public int? SALES_RM_ID { get; set; }
        [ForeignKey("SALES_RM_ID")]
        public virtual REF_USER REF_USER { get; set; }
        [Display(Name = "Delivery Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DELIVERY_DATE { get; set; }
        [Display(Name = "Customer RFQ No")]
        public string CUSTOMER_RFQ_NO { get; set; }
        [Display(Name = "Customer RFQ Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? CUSTOMER_RFQ_DATE { get; set; }
        [Display(Name = "Quotation Expiry Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? QUOTATION_EXPIRY_DATE { get; set; }
        public int PAYMENT_TERMS_ID { get; set; }
        public virtual REF_PAYMENT_TERMS REF_PAYMENT_TERMS { get; set; }
        public int PAYMENT_CYCLE_ID { get; set; }
        public virtual REF_PAYMENT_CYCLE REF_PAYMENT_CYCLE { get; set; }
        [Display(Name = "Internal Remarks")]
        public string INTERNAL_REMARKS { get; set; }
        [Display(Name = "Remarks on Document")]
        public string REMARKS { get; set; }
        [Display(Name = "Billing Address")]
        [DataType(DataType.MultilineText)]
        public string BILLING_ADDRESS { get; set; }
        [Display(Name = "Billing City")]
        public string BILLING_CITY { get; set; }
        [Display(Name = "Billing Pincode")]
        public int BILLING_PINCODE { get; set; }
        public int BILLING_STATE_ID { get; set; }
        [ForeignKey("BILLING_STATE_ID")]
        public virtual REF_STATE REF_STATE { get; set; }
        [Display(Name = "Email")]
        public string BILLING_EMAIL_ID { get; set; }
        [Display(Name = "Shipping Address")]
        [DataType(DataType.MultilineText)]
        public string SHIPPING_ADDRESS { get; set; }
        [Display(Name = "Shipping City")]
        public string SHIPPING_CITY { get; set; }
        [Display(Name = "Shipping Pincode")]
        public int SHIPPING_PINCODE { get; set; }
        public string SHIPPING_EMAIL_ID { get; set; }
        public int SHIPPING_STATE_ID { get; set; }
        [ForeignKey("SHIPPING_STATE_ID")]
        public virtual REF_STATE REF_STATE1 { get; set; }
        [Display(Name = "PAN *")]
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
        public string attachment { get; set; }
        [Display(Name = "Commisionerate")]
        public string commisionerate { get; set; }
        [Display(Name = "Range")]
        public string range { get; set; }
        [Display(Name = "Division")]
        public string division { get; set; }
        public int doc_currency_id { get; set; }
        [ForeignKey("doc_currency_id")]
        public virtual REF_CURRENCY REF_CURRENCY2 { get; set; }
        public double? exchange_rate { get; set; }
        public double net_value_local { get; set; }
        public double gross_value_local { get; set; }
        public bool? order_status { get; set; }
        public int? supply_state_id { get; set; }
        public int? delivery_state_id { get; set; }
        public string shipping_pan_no { get; set; }
        public string shipping_gst_no { get; set; }
        public virtual ICollection<SAL_QUOTATION_DETAIL> SAL_QUOTATION_DETAIL { get; set; }
        public virtual ICollection<SAL_QUOTATION_FORM> SAL_QUOTATION_FORM { get; set; }    
       
    }
}
