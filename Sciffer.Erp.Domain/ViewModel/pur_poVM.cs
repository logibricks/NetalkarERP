using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class pur_poVM
    {
        [Key]
        public int po_id { get; set; }
        public string category_name { get; set; }
        [Display(Name = "Category *")]
        public int category_id { get; set; }
        [ForeignKey("category_id")]
        public virtual REF_CATEGORY REF_CATEGORY { get; set; }
        [Display(Name = "Item / Service *")]
        public int item_service_id { get; set; }
        public string item_service_name { get; set; }
        public string po_no { get; set; }
        public string vendor_code { get; set; }
        public string vendor_name { get; set; }
        [Display(Name = "Vendor *")]
        public int vendor_id { get; set; }
        [ForeignKey("vendor_id")]
        public virtual REF_VENDOR REF_VENDOR { get; set; }
        public string vendor_id1 { get; set; }
        [Display(Name = "Posting Date *")]
        [Required(ErrorMessage = "date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime po_date { get; set; }
        [Display(Name = "Net Value *")]
        [Required(ErrorMessage = "net value is required")]
        public double net_value { get; set; }
        public string net_value_currency_name { get; set; }
        public int net_value_currency_id { get; set; }
        [ForeignKey("net_value_currency_id")]
        public virtual REF_CURRENCY REF_CURRENCY { get; set; }
        [Required(ErrorMessage = "gross value is required")]
        [Display(Name = "Gross Value *")]
        public double gross_value { get; set; }
        public string gross_value_currency_name { get; set; }
        public int gross_value_currency_id { get; set; }
        [ForeignKey("gross_value_currency_id")]
        public virtual REF_CURRENCY REF_CURRENCY1 { get; set; }
        public string business_unit_name { get; set; }
        [Display(Name = "Business Unit *")]
        public int business_unit_id { get; set; }
        [ForeignKey("business_unit_id")]
        public virtual REF_BUSINESS_UNIT REF_BUSINESS_UNIT { get; set; }
        public string plant_name { get; set; }
        [Display(Name = "Plant *")]
        public int plant_id { get; set; }
        [ForeignKey("plant_id")]
        public virtual REF_PLANT REF_PLANT { get; set; }
        public string freight_terms_name { get; set; }
        [Display(Name = "Freight Terms *")]
        public int freight_terms_id { get; set; }
        [ForeignKey("freight_terms_id")]
        public virtual REF_FREIGHT_TERMS REF_FREIGHT_TERMS { get; set; }
        [Display(Name = "Delivery Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? delivery_date { get; set; }
        [Display(Name = "Vendor Quotation Number")]
        public string vendor_quotation_no { get; set; }
        [Display(Name = "Vendor Quotation Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? vendor_quotation_date { get; set; }
        [Display(Name = "PO Valid Until")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? valid_until { get; set; }
        [Display(Name = "Created Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime created_on { get; set; }
        [Display(Name = "Created By")]
        public int created_by { get; set; }
        [Required(ErrorMessage = "billing address is required")]
        [Display(Name = "Billing Address *")]
        [DataType(DataType.MultilineText)]
        public string billing_address { get; set; }
        [Required(ErrorMessage = "billing city is required")]
        [Display(Name = "Billing City *")]
        public string billing_city { get; set; }
        public string billing_state_name { get; set; }
        [Display(Name = "Billing State *")]
        public int billing_state_id { get; set; }
        [ForeignKey("billing_state_id")]
        public virtual REF_STATE REF_STATE { get; set; }
        [Display(Name = "Billing Pincode")]
        public String billing_pin_code { get; set; }
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string email_id { get; set; }

        [Display(Name = "Contact No")]
        public string mobile_number { get; set; }
        public string payment_terms_name { get; set; }
        [Display(Name = "Payment Terms *")]
        public int payment_terms_id { get; set; }
        [ForeignKey("payment_terms_id")]
        public virtual REF_PAYMENT_TERMS REF_PAYMENT_TERMS { get; set; }
        public string payment_cycle_name { get; set; }
        [Display(Name = "Payment Cycle *")]
        public int payment_cycle_id { get; set; }
        [ForeignKey("payment_cycle_id")]
        public virtual REF_PAYMENT_CYCLE REF_PAYMENT_CYCLE { get; set; }
        //public string status_name { get; set; }
        [Display(Name = "PO Status *")]
        public int status_id { get; set; }
        [ForeignKey("status_id")]
        public virtual ref_status ref_status { get; set; }
        public string status_name { get; set; }
        [Display(Name = "Internal Remarks")]
        [DataType(DataType.MultilineText)]
        public string internal_remarks { get; set; }
        [Display(Name = "Remarks On Document")]
        [DataType(DataType.MultilineText)]
        public string remarks_on_document { get; set; }
        public bool? order_status { get; set; }
        public string order_status_name { get; set; }
        [NotMapped]
        public HttpPostedFileBase FileUpload { get; set; }
        public string attachement { get; set; }
        public virtual List<pur_po_detail> pur_po_detail { get; set; }
        public virtual List<pur_po_detail_vm> pur_po_detail_vm { get; set; }
        public virtual List<pur_po_delivery_detail_vm> pur_po_delivery_detail_vm { get; set; }
        public virtual List<pur_po_staggered_delivery> pur_po_staggered_delivery { get; set; }
        public virtual List<pur_po_form> pur_po_form { get; set; }
        [Display(Name = "Purchase against form")]
        public int? form_id { get; set; }
        public string country_name { get; set; }
        [Display(Name = "Billing Country *")]
        public int country_id { get; set; }
        public string parment_cycle_type_name { get; set; }
        [Display(Name = "Payment Cycle Type *")]
        public int payment_cycle_type_id { get; set; }
        public String deleteids { get; set; }
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
        public int? pur_requisition_id { get; set; }
        public int tds_id { get; set; }
        [Display(Name = "Ref Doc No")]
        public string ref_doc_no { get; set; }
        [Display(Name = "Delivery Type *")]
        public int delivery_type_id { get; set; }
        public string delivery_type_name { get; set; }
        public string plant_service_tax_no { get; set; }
        public List<string> item_id { get; set; }
        public List<string> user_description { get; set; }
        public List<string> sloc_id { get; set; }
        public List<DateTime> Linedelivery_date { get; set; }
        public List<string> quantity { get; set; }
        public List<string> uom_id { get; set; }
        public List<string> unit_price { get; set; }
        public List<string> discount { get; set; }
        public List<string> eff_price { get; set; }
        public List<string> pur_value { get; set; }
        public List<string> ass_rate { get; set; }
        public List<string> ass_value { get; set; }
        public List<string> tax_id { get; set; }
        public List<string> pur_requisition_detail_id { get; set; }
        public List<string> po_detail_id { get; set; }
        public List<string> po_staggered_delivery_id { get; set; }
        public List<string> staggered_date { get; set; }
        public List<string> staggered_qty { get; set; }
        public List<string> staggered_item { get; set; }
        public List<string> srnos { get; set; }
        public List<string> sac_hsn_id { get; set; }
        public List<int> item_type_id { get; set; }
        public string pur_requisition_name { get; set; }
        public string business_unit_code { get; set; }
        public string plant_code { get; set; }
        public string form_name { get; set; }
        public string payment_cycle_type_name { get; set; }

        public double? maximum_limit_qty { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? valid_from_date { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? valid_to_date { get; set; }
        public int? approval_status { get; set; }
        public string approval_status_name { get; set; }
        public string approval_comments { get; set; }
        [Display(Name = "Place of Supply *")]
        public int place_of_supply_id { get; set; }
        [Display(Name = "GSTIN *")]
        public string gst_in { get; set; }
        [Display(Name = "GST Vendor Category *")]
        public int? gst_vendor_type_id { get; set; }
        public bool is_rcm { get; set; }
        [Display(Name = "Cancellation Remarks")]
        [DataType(DataType.MultilineText)]
        public string cancellation_remarks { get; set; }
        public DateTime? created_ts { get; set; }
        public int? modify_by { get; set; }
        public DateTime? modify_ts { get; set; }
        public int? cancelled_by { get; set; }
        public DateTime? cancelled_date { get; set; }
        public string created_user { get; set; }
        public string modify_user { get; set; }


        public List<int> item_ids { get; set; }
        public List<string> user_descriptions { get; set; }
        public List<int> sloc_ids { get; set; }
        public List<DateTime> Linedelivery_dates { get; set; }
        public List<decimal> quantitys { get; set; }
        public List<int> uom_ids { get; set; }
        public List<decimal> unit_prices { get; set; }
        public List<decimal> discounts { get; set; }
        public List<decimal> eff_prices { get; set; }
        public List<decimal> pur_values { get; set; }
        public List<decimal> ass_rates { get; set; }
        public List<decimal> ass_values { get; set; }
        public List<int> tax_ids { get; set; }
        public List<int> pur_requisition_detail_ids { get; set; }
        public List<string> po_detail_ids { get; set; }
        public List<int> sac_hsn_ids { get; set; }
        public List<int> detsrs { get; set; }
        public string item_name { get; set; }
        public string rcm { get; set; }
        [Display(Name = "Responsibility")]
        public int? responsibility_id { get; set; }
        public string responsibility { get; set; }
        public int? approved_by { get; set; }
        public DateTime? approved_ts { get; set; }
        public string closed_remarks { get; set; }
        public int? closed_by { get; set; }
        public DateTime? closed_ts { get; set; }
        public string approved_user { get; set; }
        public string closed_user { get; set; }
        public int? with_without_service_id { get; set; }
        public string po_verson { get; set; }
        public string amendment { get; set; }
    }
    public class pur_po_detail_vm
    {
        public int po_detail_id { get; set; }
        public int po_id { get; set; }
        public int sr_no { get; set; }
        public int sloc_id { get; set; }
        public string sloc_name { get; set; }
        public int item_id { get; set; }
        public string item_code { get; set; }
        public string remarks_on_document { get; set; }
        public string item_name { get; set; }
        public DateTime delivery_date { get; set; }
        public string deliverydate { get; set; }
        public double quantity { get; set; }
        public int uom_id { get; set; }
        public string uom_name { get; set; }
        public double unit_price { get; set; }
        public double discount { get; set; }
        public double eff_unit_price { get; set; }
        public double purchase_value { get; set; }
        public double assesable_rate { get; set; }
        public double assesable_value { get; set; }
        public int tax_code_id { get; set; }
        public string tax_name { get; set; }
        public string tax_code { get; set; }
        public double balance_quantity { get; set; }
        public string user_description { get; set; }
        public bool BATCH_MANAGED { get; set; }
        public bool QUALITY_MANAGED { get; set; }
        //public string batch { get; set; }
        public int auto_batch { get; set; }
        public int ITEM_CATEGORY_ID { get; set; }
        public int plant_id { get; set; }
        public int pur_requisition_detail_id { get; set; }
        public double tax_value { get; set; }
        public int? hsn_id { get; set; }
        public string hsn_code { get; set; }
        public int? item_type_id { get; set; }
        public int po_staggered_delivery_id { get; set; }

    }
    public class pur_po_detail_report_vm
    {
        public int po_detail_id { get; set; }
        public int po_id { get; set; }

        public int sr_no { get; set; }
        public int sloc_id { get; set; }
        public string sloc_name { get; set; }
        public int item_id { get; set; }
        public string item_code { get; set; }
        public string remarks_on_document { get; set; }
        public string item_name { get; set; }
        public string delivery_date { get; set; }
        [Range(0, 9999999999999999.99)]
        public double quantity { get; set; }
        public int uom_id { get; set; }
        public string uom_name { get; set; }
        [Range(0, 9999999999999999.99)]
        public decimal unit_price { get; set; }
        [Range(0, 9999999999999999.99)]
        public decimal po_value { get; set; }
        [Range(0, 9999999999999999.99)]
        public decimal discount { get; set; }
        [Range(0, 9999999999999999.99)]
        public decimal eff_unit_price { get; set; }
        [Range(0, 9999999999999999.99)]
        public decimal purchase_value { get; set; }
        [Range(0, 9999999999999999.99)]
        public decimal assesable_rate { get; set; }
        [Range(0, 9999999999999999.99)]
        public decimal assesable_value { get; set; }
        public string user_description { get; set; }
        public int tax_code_id { get; set; }
        public string tax_code { get; set; }
        public string tax_name { get; set; }
        [Range(0, 9999999999999999.99)]
        public decimal tax_value { get; set; }
        public decimal? disvalue { get; set; }
        public string date { get; set; }
        public decimal? po_de_qty { get; set; }
    }
    public class pur_po_report_vm
    {
        public string plant_gst { get; set; }
        public string vendor_gst { get; set; }
        public int? vendor_id { get; set; }
        public string vendor_contact_person { get; set; }
        public string currency { get; set; }
        public int delivery_type_id { get; set; }
        public int po_id_grn { get; set; }
        public int po_id { get; set; }
        public string po_no { get; set; }
        public string podate { get; set; }
        public string createdts { get; set; }
        public DateTime po_date { get; set; }
        public string payment_term { get; set; }
        public string service_tax_no { get; set; }
        public string freight_terms_name { get; set; }
        public string vendor_name { get; set; }
        public string vendor_address { get; set; }
        public string vendor_pin_code { get; set; }
        public string vendor_ecc_no { get; set; }
        public string vendor_vat_no { get; set; }
        public string vendor_cst_no { get; set; }
        public string vendor_city { get; set; }
        public string vendor_pan_no { get; set; }
        public string vendor_state { get; set; }
        public string vendor_country { get; set; }
        public string company_address { get; set; }
        public string company_state { get; set; }
        public string company_country { get; set; }
        public string company_telephone { get; set; }
        public string company_email { get; set; }
        public string company_website { get; set; }
        public int? company_pincode { get; set; }
        public string company_city { get; set; }
        public string plant_name { get; set; }
        public string plant_range { get; set; }
        public string plant_division { get; set; }
        public string plant_commissionarate { get; set; }
        public string freight_terms { get; set; }
        public string compnay_name { get; set; }
        public string company_display_name { get; set; }
        public string company_corporate_address { get; set; }
        public string company_corporate_state { get; set; }
        public string company_corporate_country { get; set; }
        public string company_corporate_telephone { get; set; }
        public string company_corporate_email { get; set; }
        public string company_corporate_website { get; set; }
        public int? company_corporate_pincode { get; set; }
        public string company_corporate_city { get; set; }
        public string company_cin_no { get; set; }
        public string company_pan_no { get; set; }
        public string company_tan_no { get; set; }
        public string consignee_category { get; set; }
        public string excisable_commidity { get; set; }
        public int tariff_no { get; set; }
        public string tariff_rate { get; set; }
        public string mode_of_transport { get; set; }
        public string vehicle_no { get; set; }
        public string vendor_code { get; set; }
        public int tds_code_id { get; set; }
        public byte[] company_logo { get; set; }
        public double purchase_value { get; set; }
        public double net_value { get; set; }
        public string plant_vat { get; set; }
        public string plant_cst { get; set; }
        public string plant_ECC { get; set; }
        public string vendor_quotationdate { get; set; }
        public string vendor_quotation_refNo { get; set; }
        public string remarks_ondocument { get; set; }
        public string plant_address { get; set; }
        public string Payment_Terms { get; set; }
        public string plant_service_tax_no { get; set; }
        public string vendor_email { get; set; }
        public string vendor_phone { get; set; }
        public int item_service_id { get; set; }
        public string plant_gst_no { get; set; }
        public string vendor_gst_no { get; set; }
        public string plant_gs { get; set; }
        public string gst_vendor_type { get; set; }
        public string plant_email { get; set; }
        public string COUNTRY_NAME { get; set; }
        public string billing_pin_code { get; set; }
        public int? with_without_service_id { get; set; }
        public string po_verson { get; set; }
        public int? approval_status { get; set; }
        public string PLANT_TELEPHONE { get; set; }
        public string plant_code { get; set; }
    }
    public class purchase_order_report_vm
    {
        public string item_category_name { get; set; }
        public int? Delay { get; set; }
        public string pi_number { get; set; }
        public string pi_category { get; set; }
        public double? pi_net_value { get; set; }
        public double? pi_quantity { get; set; }
        public DateTime? pi_date { get; set; }
        public double? iex_net_vlue { get; set; }
        public double? iex_quantity { get; set; }
        public double? grn_net_value { get; set; }
        public DateTime? grn_date { get; set; }
        public string grn_category { get; set; }
        public double? po_net_value { get; set; }

        public string isrcm { get; set; }
        public DateTime? incoming_excise_posting_date { get; set; }
        public string incoming_excise_number { get; set; }
        public string iex_category { get; set; }
        public string grn_number { get; set; }
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
        public double? Balance_Qty { get; set; }
        public double? Po_Qty { get; set; }
        public string vendor_code { get; set; }
        public string vendor_name { get; set; }
        public string item_service { get; set; }
        public string user_description { get; set; }
        public string storage_location_name { get; set; }
        public string uom_name { get; set; }
        public double? unit_price { get; set; }
        public DateTime? pur_requisition_date { get; set; }
        public string pur_requisition_number { get; set; }
        public double? discount { get; set; }
        public double? eff_unit_price { get; set; }
        public double? purchase_value { get; set; }
        public double? assesable_rate { get; set; }
        public double? assessable_value { get; set; }
        public double? assessable_rate { get; set; }
        public double? tds_code_value { get; set; }
        public string pr_no { get; set; }
        public string tax_name { get; set; }
        //for header
        public double? net_value { get; set; }
        public double? gross_value { get; set; }
        public string business_unit_name { get; set; }
        public string freight_terms_name { get; set; }
        public string vendor_quotation_no { get; set; }
        public DateTime? vendor_quotation_date { get; set; }
        public DateTime? valid_until { get; set; }
        public string form_name { get; set; }
        public string payment_terms_code { get; set; }
        public string payment_cycle_name { get; set; }
        public string payment_cycle_type_name { get; set; }
        public double? tax_value { get; set; }
        public string vendor_parent_code { get; set; }
        public string vendor_parent_name { get; set; }
        public string vendor_doc_no { get; set; }
        public DateTime? vendor_doc_date { get; set; }
        public string gate_entry_number { get; set; }
        public DateTime? gate_entry_date { get; set; }
        public string po_no { get; set; }
        public string batch { get; set; }
        public string bucket_name { get; set; }
        public double? round_off { get; set; }
        public DateTime? po_date { get; set; }
        public int? day_diff { get; set; }
        public string pr_doc_category { get; set; }
        public string status_name { get; set; }
        public string po_doc_category { get; set; }
        public double? po_quantity { get; set; }
        public double? pr_quantity { get; set; }
        public string source_name { get; set; }
        public double info_price { get; set; }
        public string vendor_category_name { get; set; }
        public string email_id_primary { get; set; }
        public string telephone_primary { get; set; }
        public string contact_name { get; set; }
        public string designation { get; set; }
        public string mobile_no { get; set; }
        public string telephone_secondary { get; set; }
        public string email_id_secondary { get; set; }

        public string payment_terms_description { get; set; }
        public string due_date_based { get; set; }
        public string bank_code { get; set; }
        public string bank_name { get; set; }
        public string bank_ifsc_code { get; set; }
        public string ifsc_code { get; set; }
        public string bank_account_number { get; set; }
        public double overall_discount { get; set; }
        public string item_group_name { get; set; }
        public double? rate { get; set; }
        public double original_price { get; set; }
        public double discount_value { get; set; }
        public int discount_type_id { get; set; }
        public double price_after_discount { get; set; }
        public DateTime? line_item_delivery_date { get; set; }
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



        public int? parent_id { get; set; }
        public int? vendor_id { get; set; }
        //public double? balance_qty { get; set; }
        public double? total_quantity { get; set; }
    }

    public class pur_po_delivery_detail_vm
    {
        public DateTime staggered_date { get; set; }
        public double staggered_qty { get; set; }
        public int staggered_item_id { get; set; }
        public int po_staggered_delivery_id { get; set; }
        public int po_id { get; set; }
        public int po_detail_id { get; set; }
        public string item_code { get; set; }
        public double quantity { get; set; }
        public int sr_nos { get; set; }
        public double? del_balance_qty { get; set; }
    }

    public class hsn_sac
    {
        public int id { get; set; }
        public string code { get; set; }
        public string description { get; set; }
    }

}
