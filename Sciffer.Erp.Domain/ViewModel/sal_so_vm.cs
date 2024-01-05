using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class sal_so_vm
    {
        public int so_id { get; set; }
        [Display(Name ="Quotation Number")]
        public int? quotation_id { get; set; }
        public string sales_category_name { get; set; }
        [Display(Name ="Category *")]
        public int sales_category_id { get; set; }
        [ForeignKey("sales_category_id")]
        public virtual ref_document_numbring ref_document_numbring { get; set; }
        [Display(Name = "SO Number *")]
        public string so_number { get; set; }
        [Display(Name = "Posting Date *")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? so_date { get; set; }
        public string buyer_name { get; set; }
        public string buyer_code { get; set; }
        [Display(Name = "Bill to Party *")]
        public int buyer_id { get; set; }
        [ForeignKey("buyer_id")]
        public virtual REF_CUSTOMER REF_CUSTOMER { get; set; }
        [Display(Name = "Bill to Party *")]
        public string buyer_id1 { get; set; }
        public string consignee_name { get; set; }
        public string consignee_code { get; set; }
        public int? consignee_id { get; set; }
        [ForeignKey("consignee_id")]
        public virtual REF_CUSTOMER REF_CUSTOMER1 { get; set; }
        [Display(Name = "Ship to Party")]
        public string consignee_id1 { get; set; }
        [Display(Name = "Net Value *")]
        [RegularExpression(@"(^\d+$)|(^\.\d{1,4}$)|(^\d*\.\d{0,4}$)")]
        public double sal_net_value { get; set; }
        public string net_value_currency_name { get; set; }
        public int net_value_currency_id { get; set; }
        [ForeignKey("net_value_currency_id")]
        public virtual REF_CURRENCY REF_CURRENCY { get; set; }
        [Display(Name = "Gross Value *")]
        [RegularExpression(@"(^\d+$)|(^\.\d{1,4}$)|(^\d*\.\d{0,4}$)")]
        public double sal_gross_value { get; set; }
        public string gross_value_currency_name { get; set; }
        public int gross_value_currency_id { get; set; }
        [ForeignKey("gross_value_currency_id")]
        public virtual REF_CURRENCY REF_CURRENCY1 { get; set; }
        public string business_unit_name { get; set; }
        [Display(Name = "Business Unit *")]
        public int business_unit_id { get; set; }
        public virtual REF_BUSINESS_UNIT REF_BUSINESS_UNIT { get; set; }
        public string plant_name { get; set; }
        [Display(Name ="Plant *")]
        public int plant_id { get; set; }
        public virtual REF_PLANT REF_PLANT { get; set; }
        [Display(Name = "Customer PO Number *")]
        public string customer_po_no { get; set; }


        [Display(Name = "Customer PO Date *")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? customer_po_date { get; set; }

        //[Display(Name = "Customer PO Date *")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //public DateTime? customer_po_date { get; set; }
        public string freight_terms_name { get; set; }
        [Display(Name = "Freight Terms *")]
        public int freight_terms_id { get; set; }
        public virtual REF_FREIGHT_TERMS REF_FREIGHT_TERMS { get; set; }
        public string territory_name { get; set; }
        [Display(Name = "Territory")]
        public int? territory_id { get; set; }
        public virtual REF_TERRITORY REF_TERRITORY { get; set; }
        public string sales_rm_name { get; set; }
        [Display(Name = "Sales RM")]
        public int? sales_rm_id { get; set; }
        [ForeignKey("sales_rm_id")]
        public virtual REF_USER REF_USER { get; set; }
        [Display(Name = "Delivery Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? delivery_date { get; set; }
        public string payment_terms_name { get; set; }
        [Display(Name = "Payment Terms *")]
        public int payment_terms_id { get; set; }
        public virtual REF_PAYMENT_TERMS REF_PAYMENT_TERMS { get; set; }
        public string payment_cycle_type_name { get; set; }
        [Display(Name = "Payment Cycle Type *")]
        public int payment_cycle_type_id { get; set; }
        public virtual REF_PAYMENT_CYCLE_TYPE REF_PAYMENT_CYCLE_TYPE { get; set; }
        public string payment_cycle_name { get; set; }
        [Display(Name = "Payment Cycle *")]
        public int payment_cycle_id { get; set; }
        public virtual REF_PAYMENT_CYCLE REF_PAYMENT_CYCLE { get; set; }
        [Display(Name = "Available Credit Limit")]
        public double? avail_credit_limit { get; set; }
        [Display(Name = "Credit Available After Order")]
        public double? credit_avail_after_order { get; set; }
        [Display(Name = "Internal Remarks")]
        [DataType(DataType.MultilineText)]
        public string internal_remarks { get; set; }
        [Display(Name = "Remarks on Document")]
        [DataType(DataType.MultilineText)]
        public string remarks { get; set; }
        [Display(Name = "Sale against Form")]
        public int? form_id { get; set; }
        [Display(Name = "Billing Address")]
        [DataType(DataType.MultilineText)]
        public string billing_address { get; set; }
        [Display(Name = "Billing City")]
        public string billing_city { get; set; }
        [Display(Name = "Billing Pincode")]
        public int? billing_pincode { get; set; }
        public string billing_state_name { get; set; }
        [Display(Name ="Billing State")]
        public int billing_state_id { get; set; }       
        [ForeignKey("billing_state_id")]
        public virtual REF_STATE REF_STATE { get; set; }
        public string billing_country_name { get; set; }
        [Display(Name ="Billing Country")]
        public int billing_country_id { get; set; }
        [ForeignKey("billing_country_id")]
        public virtual REF_COUNTRY REF_COUNTRY { get; set; }
        [Display(Name = "Billing Email")]
        public string billing_email_id { get; set; }
        [Display(Name = "Shipping Address")]
        [DataType(DataType.MultilineText)]
        public string shipping_address { get; set; }
        [Display(Name = "Shipping City")]
        public string shipping_city { get; set; }
        [Display(Name = "Shipping Pincode")]
        public int? shipping_pincode { get; set; }
        public string shipping_state_name { get; set; }
        [Display(Name = "Shipping State")]
        public int? shipping_state_id { get; set; }
        [ForeignKey("shipping_state_id")]
        public virtual REF_STATE REF_STATE1 { get; set; }
        [Display(Name = "Shipping Email")]
        public string shipping_email_id { get; set; }
        public string shipping_country_name { get; set; }
        [Display(Name = "Shipping Country")]
        public int? shipping_country_id { get; set; }
        [ForeignKey("shipping_country_id")]
        public virtual REF_COUNTRY REF_COUNTRY1 { get; set; }
        public string deleteids { get; set; }
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
        public bool? order_status { get; set; }
        [Display(Name = "Place of Supply *")]
        public int? supply_state_id { get; set; }
        [Display(Name = "Place of Delivery *")]
        public int? delivery_state_id { get; set; }
        public string sodate { get; set; }
        public string delivery_state { get; set; }
        public string supply_state { get; set; }
        [Display(Name = "PAN No *")]
        public string shipping_pan_no { get; set; }
        [Display(Name = "GSTIN *")]
        public string shipping_gst_no { get; set; }
        [Display(Name = "TDS Code")]
        public int? tds_code_id { get; set; }
        [Display(Name = "GST TDS")]
        public int? gst_tds_code_id { get; set;}
        public string tds_code { get; set; }
        public string gst_tds_code { get; set; }
        [NotMapped]
        public HttpPostedFileBase FileUpload { get; set; }
        public virtual List<sal_so_detail> sal_so_detail { get; set; }
        public virtual List<sal_so_detail_report_vm> sal_so_detail_report_vm { get; set; }
        public virtual List<sal_so_form> sal_so_form { get; set; }
        public List<string> so_detail_id { get; set; }
        public List<string> item_id { get; set; }
        public List<string> delivery_date1 { get; set; }
        public List<string> quantity { get; set; }
        public List<string> uom_id { get; set; }
        public List<string> unit_price { get; set; }
        public List<string> discount { get; set; }
        public List<string> effective_unit_price { get; set; }
        public List<string> sales_value { get; set; }
        public List<string> assessable_rate { get; set; }
        public List<string> assessable_value { get; set; }
        public List<string> tax_id { get; set; }
        public List<string> sloc_id { get; set; }
        public List<string> quotation_detail_id { get; set; }
        public List<string> drawing_no { get; set; }
        public List<string> material_cost_per_unit { get; set; }
        public List<string> sac_hsn_id1 { get; set; }
        public string business_desc { get; set; }
        public string plant_code { get; set; }
        public string currency_name { get; set; }
        public string form_name { get; set; }
        public string sales_rm_code { get; set; }
        public string quotation_number { get; set; }
        public string payment_terms_desc { get; set; }
        public string closed_remarks { get; set; }
        public int? closed_by { get; set; }
        public DateTime? closed_ts { get; set; }
        public string closed_user { get; set; }
        public List<string> machine_charges { get; set; }
        public string item_name { get; set; }

    }

    public class sal_so_report_vm
    {
        public int so_id { get; set; }
        public string so_number { get; set; }
        public DateTime so_date { get; set; }
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
        public int company_vat_no { get; set; }    
        public string company_cin_no { get; set; }
        public string company_ecc_no { get; set; }
        public string company_pan_no { get; set; }
        public string consignee_category { get; set; }
        public string excisable_commidity { get; set; }
        public int tariff_no { get; set; }
        public string tariff_rate { get; set; }
        public string mode_of_transport { get; set; }
        public string vehicle_no { get; set; }
        public string vendor_code { get; set; }
        public int tds_code_id { get; set; }
        public byte[] company_logo { get; set; }
        public double sal_gross_value { get; set; }
    }


    public class sal_so_detail_report_vm
    {
        public int so_id { get; set; }
        public int so_detail_id { get; set; }
        public DateTime delivery_date { get; set; }
        public double quantity { get; set; }
        public double unit_price { get; set; }
        public double discount { get; set; }
        public double effective_unit_price { get; set; }
        public double sales_value { get; set; }
        public double assessable_rate { get; set; }
        public double assessable_value { get; set; }
        public string tax_code { get; set; }
        public int item_id { get; set; }
        public string item_name { get; set; }
        public string item_code { get; set; }
        public string uom_name { get; set; }
        public int storage_location_id { get; set; }
        public string storage_location_name { get; set; }
        public int tax_id { get; set; }
        public string tax_name { get; set; }
        public int uom_id { get; set; }
        public int sr_no { get; set; }
        public double balance_quantity { get; set; }
        public string drawing_no { get; set; }
        public double material_cost_per_unit { get; set; }
        public int rowIndex { get; set; }
        public int plant_id { get; set; }
        public bool tag_managed { get; set; }
        public bool batch_managed { get; set; }
        public int tag_quantity { get; set; }
        public int batch_quantity { get; set; }
        public int? sac_hsn_id { get; set; }
        public string sac_hsn_name { get; set; }
        public int quotation_detail_id { get; set; }
        public decimal? machine_charges { get; set; }
    }
    public class sales_order_report_vm
    {
        public string gate_entry_no { get; set; }
        public DateTime? gate_entry_date { get; set; }
        public string invoice_number { get; set; }
        public DateTime? invoice_date { get; set; }
        public string reason_for_return { get; set; }
        public string billing_address { get; set; }
        public string billing_pincode { get; set; }
        public string billing_state { get; set; }
        public string billing_country { get; set; }
        public string shipping_address { get; set; }
        public string shipping_city { get; set; }
        public string shipping_pincode { get; set; }
        public string shipping_state { get; set; }
        public string shipping_country { get; set; }
        public string plant_code { get; set; }
        public string return_number { get; set; }
        public double? credit_avail_after_invoice { get; set; }
        public string tds_code { get; set; }
        public double? available_credit_limit { get; set; }
        public string tds_code_description { get; set; }
        public string shipping_customer_code { get; set; }
        public string shipping_customer_name { get; set; }
        public string gros_currency { get; set; }
        public double? issue_quantity { get; set; }
        public double? quotation_quantity { get; set; }
        public string so_number { get; set; }
        public string category { get; set; }
        public string document_no { get; set; }
        public DateTime? posting_date { get; set; }
        public string plant_name { get; set; }
        public string item_code { get; set; }
        public string item_name { get; set; }
        public DateTime? header_delivery_date { get; set; }
        public DateTime? delivery_date { get; set; }
        public double? quantity { get; set; }
        public double? grn_quantity { get; set; }
        public double? balance_quantity { get; set; }
        public string customer_code { get; set; }
        public string customer_name { get; set; }
        public string billing_city { get; set; }
        public string priority_name { get; set; }
        public int? customer_id { get; set; }
        public string parent_code { get; set; }
        public string parent_name { get; set; }
        public string parent_billing_city { get; set; }
        public string item_service { get; set; }
        public string user_description { get; set; }
        public string storage_location_name { get; set; }
        public string uom_name { get; set; }
        public double? unit_price { get; set; }
        public double? discount { get; set; }
        public double? eff_unit_price { get; set; }
        public double? effective_unit_price { get; set; }
        public double? purchase_value { get; set; }
        public double? assessable_rate { get; set; }
        public double? assessable_value { get; set; }
        public double? assessable_value_local { get; set; }
        public string tax_name { get; set; }
        public string tax_code { get; set; }
        public string pr_doc_category { get; set; }
        public string pur_requisition_number { get; set; }
        public DateTime? pur_requisition_date { get; set; }
        public string status_name { get; set; }
        public string so_doc_category { get; set; }
        public string so_no { get; set; }
        public string customer_po_no { get; set; }
        public DateTime? customer_po_date { get; set; }
        public DateTime? so_date { get; set; }
        public double? so_quantity { get; set; }
        public string si_no { get; set; }
        public DateTime? si_date { get; set; }
        public double? si_quantity { get; set; }
        public double? pi_quantity { get; set; }
        public string employee_name { get; set; }
        public string source_name { get; set; }
        public string freight_terms_name { get; set; }
        public string form_name { get; set; }
        public string territory_name { get; set; }
        public string currency { get; set; }
        public double? info_price { get; set; }
        public string business_unit_name { get; set; }
        public string customer_category_name { get; set; }
        public string email_id_primary { get; set; }
        public string telephone_primary { get; set; }
        public string contact_name { get; set; }
        public string designation { get; set; }
        public string mobile_no { get; set; }
        public string telephone_secondary { get; set; }
        public string email_id_secondary { get; set; }
        public double? net_value { get; set; }
        public double? gross_value { get; set; }
        public double? round_off { get; set; }
        public double? tax_value { get; set; }
        public double? tds_value { get; set; }
        public double? sal_net_value { get; set; }
        public double? invoice_net_value { get; set; }
        public double? open_value { get; set; }
        public double? total_quantity { get; set; }
        public double? invoice_quantity { get; set; }
        public double? balance_qty { get; set; }
        public double? total_amount { get; set; }
        public double? balance_amount { get; set; }
        public string si_number { get; set; }
        public int? parent_id { get; set; }
        public string payment_terms_code { get; set; }
        public string payment_terms_description { get; set; }
        public string due_date_based { get; set; }
        public string payment_cycle_type_name { get; set; }
        public string payment_cycle_name { get; set; }
        public string bank_code { get; set; }

        public string bank_name { get; set; }
        public string bank_ifsc_code { get; set; }
        public string bank_account_number { get; set; }
        public double? overall_discount { get; set; }
        public string item_group_name { get; set; }
        public double? rate { get; set; }
        public double? avg_rate { get; set; }
        public double? sales_value { get; set; }
        public double? original_price { get; set; }
        public double? discount_value { get; set; }
        public int discount_type_id { get; set; }
        public double? price_after_discount { get; set; }
        public DateTime? line_item_delivery_date { get; set; }
        public string qo_no { get; set; }
        public DateTime? qo_date { get; set; }
        public string customer_rfq_no { get; set; }
        public DateTime? customer_rfq_date { get; set; }
        public DateTime? quotation_expiry_date { get; set; }
        public DateTime? ledger_date { get; set; }
        public DateTime? due_date { get; set; }
        public DateTime? document_date { get; set; }
        public string source_document_no { get; set; }
        public string narration { get; set; }
        public string ref1 { get; set; }
        public string ref2 { get; set; }
        public string ref3 { get; set; }
        public double? credit { get; set; }
        public double? debit { get; set; }
        public double? credit_fr { get; set; }
        public double? debit_fr { get; set; }
        public double? tax_type_name { get; set; }
        public double? Excise { get; set; }
        public double? VAT { get; set; }
        public double? CST { get; set; }
        public double? ServiceTax { get; set; }
        public double? SBC { get; set; }
        public double? KKC { get; set; }
        public double? TCS { get; set; }
        public double? NoTax { get; set; }
        public double? AED { get; set; }
        public double? IGST { get; set; }
        public double? CGST { get; set; }
        public double? SGST { get; set; }
        public double? CVD { get; set; }
        public double? Cess { get; set; }
        public double? tds_code_value { get; set; }
        public string PHONE_NO { get; set; }
        public string EMAIL_ADDRESS { get; set; }
        public DateTime? quotation_date { get; set; }
        public string quotation_number { get; set; }
        public string customer_chalan_no { get; set; }

        public DateTime customer_chalan_date { get; set; }
        public double? dispatch { get; set; }

        public double? bal_quantity { get; set; }
        public double? qty { get; set; }
        public double? bal_qty { get; set; }
        public double? dispatch_qty { get; set; }
        public string challan_no { get; set; }
        public double? fg_quantity { get; set; }

        public int? Ageing { get; set; }

        public DateTime? out_date { get; set; }
        public string out_time { get; set; }

    }
}
