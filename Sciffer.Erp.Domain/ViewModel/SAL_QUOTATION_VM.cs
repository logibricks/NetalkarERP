using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sciffer.Erp.Domain.ViewModel
{
     public class SAL_QUOTATION_VM
    {
        public int QUOTATION_ID { get; set; }
        public string SALES_CATEGORY { get; set; }
        [Display(Name = "Category *")]
        public int SALES_CATEGORY_ID { get; set; }
        [ForeignKey("SALES_CATEGORY_ID")]
        public virtual REF_SALES_CATEGORY REF_SALES_CATEGORY { get; set; }
        [Display(Name = "Quotation Number *")]
        public string QUOTATION_NUMBER { get; set; }
        [Display(Name = "Posting Date *")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime QUOTATION_DATE { get; set; }
        public string BUYER_NAME { get; set; }
        public string BUYER_CODE { get; set; }
        [Display(Name = "Bill to Party *")]
        public int? BUYER_ID { get; set; }
        [ForeignKey("BUYER_ID")]
        public virtual REF_CUSTOMER REF_CUSTOMER { get; set; }
        public string BUYER_ID1 { get; set; }
        public string CONSIGNEE_NAME { get; set; }
        public string CONSIGNEE_CODE { get; set; }
        public int? CONSIGNEE_ID { get; set; }
        [ForeignKey("CONSIGNEE_ID")]
        public virtual REF_CUSTOMER REF_CUSTOMER1 { get; set; }
        public string CONSIGNEE_ID1 { get; set; }
        [Display(Name = "Net Value")]
        [RegularExpression(@"(^\d+$)|(^\.\d{1,4}$)|(^\d*\.\d{0,4}$)")]
        public double SAL_NET_VALUE { get; set; }
        public string NET_VALUE_CURRENCY_NAME { get; set; }
        public int NET_VALUE_CURRENCY_ID { get; set; }
        [ForeignKey("NET_VALUE_CURRENCY_ID")]
        public virtual REF_CURRENCY REF_CURRENCY { get; set; }
        [Display(Name = "Gross Value")]
        [RegularExpression(@"(^\d+$)|(^\.\d{1,4}$)|(^\d*\.\d{0,4}$)")]
        public double SAL_GROSS_VALUE { get; set; }
        public string GROSS_VALUE_CURRENCY_NAME { get; set; }
        public int GROSS_VALUE_CURRENCY_ID { get; set; }
        [ForeignKey("GROSS_VALUE_CURRENCY_ID")]
        public virtual REF_CURRENCY REF_CURRENCY1 { get; set; }       
        public string BUSINESS_UNIT_NAME { get; set; }
        [Display(Name = "Business Unit *")]
        public int BUSINESS_UNIT_ID { get; set; }
        public virtual REF_BUSINESS_UNIT REF_BUSINESS_UNIT { get; set; }
        public string PLANT_NAME { get; set; }
        [Display(Name = "Plant *")]
        public int PLANT_ID { get; set; }
        public virtual REF_PLANT REF_PLANT { get; set; }       
        public string FREIGHT_TERMS_NAME { get; set; }
        [Display(Name = "Freight Terms *")]
        public int FREIGHT_TERMS_ID { get; set; }
        public virtual REF_FREIGHT_TERMS REF_FREIGHT_TERMS { get; set; }
        public string TERRITORY_NAME { get; set; }
        public int? TERRITORY_ID { get; set; }
        public virtual REF_TERRITORY REF_TERRITORY { get; set; }
        public string SALES_RM_NAME { get; set; }
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
        [DataType(DataType.Date)]
        [Display(Name = "Quotation Expiry Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? QUOTATION_EXPIRY_DATE { get; set; }
        public string PAYMENT_TERMS_NAME { get; set; }
        [Display(Name = "Payment Terms *")]
        public int PAYMENT_TERMS_ID { get; set; }
        public virtual REF_PAYMENT_TERMS REF_PAYMENT_TERMS { get; set; }
        public string PAYMENT_CYCLE_TYPE_NAME { get; set; }
        [Display(Name = "Payment Cycle Type *")]
        public int PAYMENT_CYCLE_TYPE_ID { get; set; }
        public virtual REF_PAYMENT_CYCLE_TYPE REF_PAYMENT_CYCLE_TYPE { get; set; }
        public string PAYMENT_CYCLE_NAME { get; set; }
        [Display(Name = "Payment Cycle *")]
        public int PAYMENT_CYCLE_ID { get; set; }
        public virtual REF_PAYMENT_CYCLE REF_PAYMENT_CYCLE { get; set; }
        [Display(Name = "Available Credit Limit")]
        public string AVAIL_CREDIT_LIMIT { get; set; }
        [Display(Name = "Credit Available After Order")]
        public string CREDIT_AVAIL_AFTER_ORDER { get; set; }
        [Display(Name = "Internal Remarks")]
        [DataType(DataType.MultilineText)]
        public string INTERNAL_REMARKS { get; set; }
        [Display(Name = "Remarks on Document")]
        [DataType(DataType.MultilineText)]
        public string REMARKS { get; set; }
        public int? FORM_ID { get; set; }
        [Display(Name ="Billing Address *")]
        [DataType(DataType.MultilineText)]
        public string BILLING_ADDRESS { get; set; }
        [Display(Name = "Billing City *")]
        public string BILLING_CITY { get; set; }
        [Display(Name = "Billing Pincode *")]
        public int BILLING_PINCODE { get; set; }
        public string BILLING_STATE_NAME { get; set; }
        [Display(Name = "Billing State *")]
        public int BILLING_STATE_ID { get; set; }
        [ForeignKey("BILLING_STATE_ID")]
        public virtual REF_STATE REF_STATE { get; set; }
        public string BILLING_COUNTRY_NAME { get; set; }
        [Display(Name = "Billing Country *")]
        public int BILLING_COUNTRY_ID { get; set; }
        [ForeignKey("BILLING_COUNTRY_ID")]
        public virtual REF_COUNTRY REF_COUNTRY { get; set; }
        [Display(Name ="Billing Email")]
        public string BILLING_EMAIL_ID { get; set; }
        [Display(Name = "Shipping Address *" )]
        [DataType(DataType.MultilineText)]
        public string SHIPPING_ADDRESS { get; set; }
        [Display(Name = "Shipping City *")]
        public string SHIPPING_CITY { get; set; }
        [Display(Name = "Shipping Pincode *")]
        public int SHIPPING_PINCODE { get; set; }
        public string SHIPPING_STATE_NAME { get; set; }
        [Display(Name = "Shipping State *")]
        public int SHIPPING_STATE_ID { get; set; }
        [ForeignKey("SHIPPING_STATE_ID")]
        public virtual REF_STATE REF_STATE1 { get; set; }
        [Display(Name = "Shipping Email")]
        public string SHIPPING_EMAIL_ID { get; set; }
        public string SHIPPING_COUNTRY_NAME { get; set; }
        [Display(Name = "Shipping Country *")]
        public int SHIPPING_COUNTRY_ID { get; set; }
        [ForeignKey("SHIPPING_COUNTRY_ID")]
        public virtual REF_COUNTRY REF_COUNTRY1 { get; set; }
        public string DELETEIDS { get; set; }
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
        public string attachment { get; set; }
        [Display(Name = "Commisionerate")]
        public string commisionerate { get; set; }
        [Display(Name = "Range")]
        public string range { get; set; }
        [Display(Name = "Division")]
        public string division { get; set; }
        [Display(Name ="Currency *")]
        public int doc_currency_id { get; set; }
        [Display(Name = "Place of Supply *")]
        public int? supply_state_id { get; set; }
        [Display(Name = "Place of Delivery *")]
        public int? delivery_state_id { get; set; }
        public string delivery_state { get; set; }
        public string supply_state { get; set; }
        public double? exchange_rate { get; set; }
        public double net_value_local { get; set; }
        public double gross_value_local { get; set; }
        public string sq_date { get; set; }
        [Display(Name = "PAN No *")]
        public string shipping_pan_no { get; set; }
        [Display(Name = "GSTIN *")]
        public string shipping_gst_no { get; set; }
        public List<string> quotation_detail_id1 { get; set; }
        public List<string> sr_no1 { get; set; }
        public List<string> item_id1 { get; set; }
        public List<string> delivery_date1 { get; set; }
        public List<string> quantity1 { get; set; }
        public List<string> uom_id1 { get; set; }
        public List<string> unit_price1 { get; set; }
        public List<string> discount1 { get; set; }
        public List<string> effective_unit_price1 { get; set; }
        public List<string> sales_value1 { get; set; }
        public List<string> assessable_rate1 { get; set; }
        public List<string> assessable_value1 { get; set; }
        public List<string> tax_id1 { get; set; }
        public List<string> sloc_id1 { get; set; }
        //public List<string> sales_value_local { get; set; }
        //public List<string> asessable_value_local { get; set; }
        public List<string> drawing_no1 { get; set; }
        public List<string> material_cost_per_unit1 { get; set; }
        public List<string> sac_hsn_id1 { get; set; }
        public bool? order_status { get; set; }
        [NotMapped]
        public HttpPostedFileBase FileUpload { get; set; }
        public virtual List<SAL_QUOTATION_DETAIL> SAL_QUOTATION_DETAIL { get; set; }
        public virtual List<sal_quotation_detail_vm> sal_quotation_detail_vm { get; set; }
        public virtual List<SAL_QUOTATION_FORM> SAL_QUOTATION_FORM { get; set; }
        public string quotation_No_Date { get; set; }

        public string business_desc { get; set; }
        public string plant_code { get; set; }
        public string currency_name { get; set; }
        public string form_name { get; set; }
    }
    public class sal_quotation_detail_vm
    {
        public int? quotation_detail_id { get; set; }
        public int quotation_id { get; set; }       
        public int sr_no { get; set; }
        public int item_id { get; set; }
        public string item_code { get; set; }
        public string item_name { get; set; }
        public DateTime delivery_date { get; set; }
        public double quantity { get; set; }
        public int sloc_id { get; set; }
        public string sloc_name { get; set; }
        public int uom_id { get; set; }
        public string uom_name { get; set; }      
        public double unit_price { get; set; }
        public double discount { get; set; }
        public double effective_unit_price { get; set; }
        public double sales_value { get; set; }
        public double assessable_rate { get; set; }
        public double assessable_value { get; set; }
        public int tax_id { get; set; }
        public string tax_name { get; set; }
        public string tax_code { get; set; }
        public double balance_quantity { get; set; }
        public string drawing_no { get; set; }
        public double material_cost_per_unit { get; set; }
        public int? sac_hsn_id { get; set; }
        public string sac_hsn_name { get; set; }
    }
    public class sal_quotation_report_vm
    {
        public int quotation_id { get; set; }
        public string quotation_number { get; set; }
        public DateTime quotation_date { get; set; }
        public string buyer_name { get; set; }
        public string buyer_address { get; set; }
        public int? buyer_pin_code { get; set; }
        public string buyer_ecc_no { get; set; }
        public string buyer_vat_no { get; set; }
        public string buyer_cst_no { get; set; }
        public string buyer_city { get; set; }
        public string buyer_pan_no { get; set; }
        public string buyer_state { get; set; }
        public string buyer_country { get; set; }
        public string consignee_name { get; set; }
        public string consignee_address { get; set; }
        public string consignee_pin_code { get; set; }
        public string consignee_ecc_no { get; set; }
        public string consignee_vat_no { get; set; }
        public string consignee_cst_no { get; set; }
        public string consignee_city { get; set; }
        public string consignee_pan_no { get; set; }
        public string consignee_state { get; set; }
        public string consignee_country { get; set; }
        public string plant_name { get; set; }
        public string plant_range { get; set; }
        public string plant_division { get; set; }
        public string plant_commissionarate { get; set; }
        public string compnay_name { get; set; }
        public string company_display_name { get; set; }
        public string company_address { get; set; }
        public string company_state { get; set; }
        public string company_country { get; set; }
        public string company_telephone { get; set; }
        public string company_email { get; set; }
        public string company_website { get; set; }
        public int? company_pincode { get; set; }
        public string company_city { get; set; }
        public string company_cin_no { get; set; }
        public string company_tan_no { get; set; }
        public string company_pan_no { get; set; }       
        public byte[] company_logo { get; set; }
        public double sal_gross_value { get; set; }
    }
}
