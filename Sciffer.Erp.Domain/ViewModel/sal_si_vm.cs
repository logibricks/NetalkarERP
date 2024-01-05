using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class sal_si_vm
    {
        public int si_id { get; set; }
        [Display(Name = "SO Number *")]
        public int so_id { get; set; }
        [ForeignKey("so_id")]
        public virtual sal_so sal_so { get; set; }
        [Display(Name = "Category *")]
        public int sales_category_id { get; set; }
        [ForeignKey("sales_category_id")]
        public virtual ref_document_numbring ref_document_numbring { get; set; }
        [Display(Name = "SI Number")]
        public string si_number { get; set; }
        [Display(Name = "SI Date *")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? si_date { get; set; }
        public string buyer_name { get; set; }
        public string buyer_code { get; set; }
        [Display(Name = "Bill to Party *")]
        public int buyer_id { get; set; }
        [ForeignKey("buyer_id")]
        public virtual REF_CUSTOMER REF_CUSTOMER { get; set; }
        public string consignee_name { get; set; }
        [Display(Name = "Ship to Party *")]
        public int consignee_id { get; set; }
        [ForeignKey("consignee_id")]
        public virtual REF_CUSTOMER REF_CUSTOMER1 { get; set; }
        [Display(Name = "Net Value *")]
        public double sal_net_value { get; set; }
        public int net_value_currency_id { get; set; }
        [ForeignKey("net_value_currency_id")]
        public virtual REF_CURRENCY REF_CURRENCY { get; set; }
        [Display(Name = "Gross Value *")]
        public double sal_gross_value { get; set; }
        public int gross_value_currency_id { get; set; }
        [ForeignKey("gross_value_currency_id")]
        public virtual REF_CURRENCY REF_CURRENCY1 { get; set; }
        [Display(Name = "Business Unit *")]
        public int business_unit_id { get; set; }
        [ForeignKey("business_unit_id")]
        public virtual REF_BUSINESS_UNIT REF_BUSINESS_UNIT { get; set; }
        [Display(Name = "Plant *")]
        public int plant_id { get; set; }
        [ForeignKey("plant_id")]
        public virtual REF_PLANT REF_PLANT { get; set; }
        [Display(Name = "Freight Terms *")]
        public int freight_terms_id { get; set; }
        [ForeignKey("freight_terms_id")]
        public virtual REF_FREIGHT_TERMS REF_FREIGHT_TERMS { get; set; }
        [Display(Name = "Territory")]
        public int? territory_id { get; set; }
        [ForeignKey("territory_id")]
        public virtual REF_TERRITORY REF_TERRITORY { get; set; }
        [Display(Name = "Sales RM")]
        public int? sales_rm_id { get; set; }
        [ForeignKey("sales_rm_id")]
        public virtual REF_USER REF_USER { get; set; }
        [Display(Name = "Removal Date *")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? removal_date { get; set; }
        [Display(Name = "Removal Time *")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:t}", ApplyFormatInEditMode = true)]
        public DateTime? removal_time { get; set; }
        [Display(Name = "Customer PO Number")]
        public string customer_po_no { get; set; }
        [Display(Name = "Customer PO Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? customer_po_date { get; set; }
        [Display(Name = "Payment Terms *")]
        public int payment_terms_id { get; set; }
        [ForeignKey("payment_terms_id")]
        public virtual REF_PAYMENT_TERMS REF_PAYMENT_TERMS { get; set; }
        [Display(Name = "Payment Cycle Type *")]
        public int payment_cycle_type_id { get; set; }
        [Display(Name = "Payment Cycle *")]
        public int payment_cycle_id { get; set; }
        [ForeignKey("payment_cycle_id")]
        public virtual REF_PAYMENT_CYCLE REF_PAYMENT_CYCLE { get; set; }
        [Display(Name = "Credit Available After Invoice")]
        public double? credit_avail_after_invoice { get; set; }
        [Display(Name = "Internal Remarks")]
        [DataType(DataType.MultilineText)]
        public string internal_remarks { get; set; }
        [Display(Name = "Remarks on Document")]
        [DataType(DataType.MultilineText)]
        public string remarks { get; set; }
        [Display(Name = "Billing Address *")]
        [DataType(DataType.MultilineText)]
        public string billing_address { get; set; }
        [Display(Name = "Billing City *")]
        public string billing_city { get; set; }
        [Display(Name = "Billing Pincode *")]
        public int billing_pincode { get; set; }
        [Display(Name = "Billing Country *")]
        public int billing_country_id { get; set; }
        [Display(Name = "Billing State *")]
        public int billing_state_id { get; set; }
        [ForeignKey("billing_state_id")]
        public virtual REF_STATE REF_STATE { get; set; }
        [Display(Name = "Billing Email")]
        public string billing_email_id { get; set; }
        [Display(Name = "Shipping Address *")]
        [DataType(DataType.MultilineText)]
        public string shipping_address { get; set; }
        [Display(Name = "Shipping City *")]
        public string shipping_city { get; set; }
        [Display(Name = "Shipping Pincode *")]
        public int shipping_pincode { get; set; }
        [Display(Name = "Shipping Country *")]
        public int shipping_country_id { get; set; }
        [Display(Name = "Shipping State *")]
        public int shipping_state_id { get; set; }
        [ForeignKey("shipping_state_id")]
        public virtual REF_STATE REF_STATE1 { get; set; }
        public bool is_active { get; set; }
        [Display(Name = "Vehicle Number")]
        public string vehicle_no { get; set; }
        public string deleteids { get; set; }
        public virtual List<sal_si_detail> SAL_SI_DETAIL { get; set; }
        public virtual List<sal_si_detail_challan> sal_si_detail_challan { get; set; }
        public virtual List<sal_si_detail_report_vm> sal_si_detail_report_vm { get; set; }
        public virtual List<sal_si_form> SAL_SI_FORM { get; set; }
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
        [Display(Name = "Attachment")]
        public string attachment { get; set; }
        [Display(Name = "Commisionerate")]
        public string commisionerate { get; set; }
        [Display(Name = "Range")]
        public string range { get; set; }
        [Display(Name = "Division")]
        public string division { get; set; }
        [Display(Name = "Currency *")]
        public int? doc_currency_id { get; set; }
        [Display(Name = "Sale against Form")]
        public int? form_id { get; set; }
        [Display(Name = "TDS Code")]
        public int? tds_code_id { get; set; }
        [NotMapped]
        public HttpPostedFileBase FileUpload { get; set; }
        public string sales_category_name { get; set; }
        public string customer_code { get; set; }
        public string consignee_code { get; set; }
        public string net_currency_name { get; set; }
        public string gross_currency_name { get; set; }
        public string business_unit_name { get; set; }
        public string plant_name { get; set; }
        public string freight_terms_name { get; set; }
        public string territory_name { get; set; }
        public string sales_rm { get; set; }
        public string payment_terms_name { get; set; }
        public string payment_cycle_name { get; set; }
        public string payment_cycle_type_name { get; set; }
        public string billing_state_name { get; set; }
        public string billing_country_name { get; set; }
        public string shipping_state_name { get; set; }
        public string shipping_country_name { get; set; }
        public string storage_location { get; set; }
        public string SI_DATE_STR { get; set; }
        [Display(Name = "ASN No ")]
        public string asn_no { get; set; }
        [Display(Name = "Transporter ")]
        public string transporter { get; set; }
        [Display(Name = "LR No ")]
        public string lr_no { get; set; }
        public virtual ref_status ref_status { get; set; }
        public string status_name { get; set; }
        //public int? status_id { get; set; }
        public int? cancellation_reason_id { get; set; }
        [Display(Name = "Cancelled Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? cancelled_date { get; set; }
        [Display(Name = "Cancelled By")]
        public int? cancelled_by { get; set; }
        [Display(Name = "Last Modified Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string cancellation_reason { get; set; }
        public double? round_off { get; set; }
        [Display(Name = "Place of Supply *")]
        public int? supply_state_id { get; set; }
        public string supply_state { get; set; }
        [Display(Name = "Place of Delivery *")]
        public int? delivery_state_id { get; set; }
        public string delivery_state { get; set; }
        [Display(Name = "Mode of Transport *")]
        public int? mode_of_transport { get; set; }
        public string transport_mode { get; set; }
        [Display(Name = "LR Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? lr_date { get; set; }
        [Display(Name = "E Way Bill")]
        public string e_way_bill { get; set; }
        [Display(Name = "E Way Bill Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? e_way_bill_date { get; set; }
        public List<string> si_detail_id { get; set; }
        public List<string> so_detail_id { get; set; }
        public List<string> item_id { get; set; }
        public List<string> quantity { get; set; }
        public List<string> uom_id { get; set; }
        public List<string> unit_price { get; set; }
        public List<string> discount { get; set; }
        public List<string> effective_unit_price { get; set; }
        public List<string> sales_value { get; set; }
        public List<string> assessable_rate { get; set; }
        public List<string> assessable_value { get; set; }
        public List<string> tax_id { get; set; }
        public List<string> storage_location_id { get; set; }
        public List<string> drawing_no { get; set; }
        public List<string> material_cost_per_unit { get; set; }
        public List<string> item_tag_id { get; set; }
        public List<string> item_tag_quantity { get; set; }
        public List<string> item_tag_batch_detail_id { get; set; }
        public List<string> tag_item_id { get; set; }
        public List<string> item_batch_quantity { get; set; }
        public List<string> item_batch_detail_id { get; set; }
        public List<string> batch_item_id { get; set; }
        public List<string> sac_hsn_id1 { get; set; }
        public List<string> no_of_boxes { get; set; }

        public List<string> job_work_detail_in_id { get; set; }
        public List<string> job_work_quantity { get; set; }

        public string business_desc { get; set; }
        public string plant_code { get; set; }
        public string form_name { get; set; }
        public string so_number { get; set; }
        public string tds_name { get; set; }
        [Display(Name = "Shipping Email")]
        public string shipping_email_id { get; set; }
        [Display(Name = "PAN No *")]
        public string shipping_pan_no { get; set; }
        [Display(Name = "GSTIN *")]
        public string shipping_gst_no { get; set; }
        [Display(Name = "Available Credit Limit")]
        public double? available_credit_limit { get; set; }
        [Display(Name = "GST TDS")]
        public int? gst_tds_code_id { get; set; }
        public string gst_tds_code { get; set; }
        public string item_name { get; set; }
        public double sales_quantity { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Out Pass Date")]
        public DateTime? out_date { get; set; }
        [Display(Name = "Out Pass Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:t}", ApplyFormatInEditMode = true)]
        public TimeSpan? out_time { get; set; }
        public string outtime { get; set; }
    }
    public class sal_si_report_vm
    {
        public int si_id { get; set; }
        public string si_number { get; set; }
        public DateTime si_date { get; set; }
        public string sidate { get; set; }
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
        public int? consignee_pin_code { get; set; }
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
        public string removal_date { get; set; }
        public string removal_time { get; set; }
        public int tariff_no { get; set; }
        public string tariff_rate { get; set; }
        public string mode_of_transport { get; set; }
        public int tds_code_id { get; set; }
        public string vehicle_no { get; set; }
        public byte[] company_logo { get; set; }
        public double sal_gross_value { get; set; }
        public double excise_amount { get; set; }
        public string buyer_range { get; set; }
        public string buyer_division { get; set; }
        public string buyer_commisionerate { get; set; }
        public string consignee_range { get; set; }
        public string consignee_division { get; set; }
        public string consignee_commisionerate { get; set; }
        public string plant_vat { get; set; }
        public string plant_cst { get; set; }
        public string plant_ecc { get; set; }
        public string transporter { get; set; }
        public string customer_po_date { get; set; }
        public string customer_po_no { get; set; }
        public string vendor_code { get; set; }
        public string customer_chalan_no { get; set; }
        public decimal qty { get; set; }
        public decimal balance_qty { get; set; }
        public string posting_date { get; set; }
        public decimal dis_quantity { get; set; }
        public string hsn_code { get; set; }
        public string item_name { get; set; }
        public string lr_no { get; set; }
        public string plant_gstin { get; set; }
        public string buyer_state_code { get; set; }
        public string buyer_gstin { get; set; }
        public string place_of_supply { get; set; }
        public string consignee_state_code { get; set; }
        public string freight_terms { get; set; }
        public string lr_date { get; set; }
        public string e_way_bill { get; set; }
        public string e_way_bill_date { get; set; }
        public string place_of_delivery { get; set; }
        public string consignee_gstin { get; set; }
        public string payment_terms { get; set; }
        public string asn_no { get; set; }
        public double round_off { get; set; }
        public string remarks { get; set; }
        public double? cgst_total { get; set; }
        public double? sgst_total { get; set; }
        public double? igst_total { get; set; }
        public double? tcs_total { get; set; }
        public string customer_delivery_chalan_no { get; set; }
        public string delivery_chalan_posting_date { get; set; }

        public string shipping_address { get; set; }
        public string shipping_city { get; set; }
        public string shipping_pincode { get; set; }
        public string shipping_pan_no { get; set; }
        public string shipping_email_id { get; set; }
        public string shipping_gst_no { get; set; }

    }
    public class sales_si_challan
    {
        public string customer_chalan_no { get; set; }
        public double? qty { get; set; }
        public double? balance_qty { get; set; }
        public string posting_date { get; set; }
        public double? dis_quantity { get; set; }
        public string hsn_code { get; set; }
        public string item_name { get; set; }
        public string customer_name { get; set; }
        public string billing_address { get; set; }
        public string gst_no { get; set; }
        public double? quantity { get; set; }
        public string uom_name { get; set; }
        public string si_number { get; set; }
        public string si_date { get; set; }
        public double? dispatch_qty { get; set; }
        public string nature_of_process { get; set; }
        public double? bal_quantity { get; set; }
        public string registered_city { get; set; }
        public string plant_address { get; set; }
        public string plant_gst { get; set; }
        public string document_date { get; set; }
        public string company_name { get; set; }
        public double sal_gross_value { get; set; }
        public double? rate { get; set; }
    }


    public class sal_si_detail_report_vm
    {
        public decimal assessable_rate { get; set; }
        public decimal assessable_value { get; set; }
        public decimal discount { get; set; }
        public decimal effective_unit_price { get; set; }
        public string item_code { get; set; }
        public string item_name { get; set; }
        public decimal quantity { get; set; }
        public decimal sales_value { get; set; }
        public double gross_value { get; set; }
        public int si_detail_id { get; set; }
        public int si_id { get; set; }
        public decimal unit_price { get; set; }
        public string uom_name { get; set; }
        public string ivendor_part_no { get; set; }
        public string ihsn_code { get; set; }
        public string ovendor_part_no { get; set; }
        public string ohsn_code { get; set; }
        public string drawing_no { get; set; }
        public decimal material_cost_per_unit { get; set; }
        public string asn_no { get; set; }
        public int item_id { get; set; }
        public int tax_id { get; set; }
        public string tax_name { get; set; }
        public int uom_id { get; set; }
        public string sac_hsn_code { get; set; }
        public int? storage_location_id { get; set; }
        public string storage_location_name { get; set; }
        public double cgst_tax_rate { get; set; }
        public decimal cgst_tax_value { get; set; }
        public double sgst_tax_rate { get; set; }
        public decimal sgst_tax_value { get; set; }
        public double igst_tax_rate { get; set; }
        public decimal igst_tax_value { get; set; }
        public string no_of_boxes { get; set; }
        public decimal? machine_charges { get; set; }
        public decimal? delivery_challan_value { get; set; }
        public decimal rate { get; set; }
    }

    public class GetBatchForSalesInvoice
    {
        public int job_work_detail_in_id { get; set; }
        public int job_work_in_id { get; set; }
        public string batch { get; set; }
        public double quantity { get; set; }
        public double bal_quantity { get; set; }
        public int item_id { get; set; }
        public string ITEM_NAME { get; set; }
        public long rowindex { get; set; }
    }
}
