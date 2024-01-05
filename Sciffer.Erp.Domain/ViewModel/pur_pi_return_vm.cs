using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;


namespace Sciffer.Erp.Domain.ViewModel
{
    public class pur_pi_return_vm
    {
        public int? pi_return_id { get; set; }
        [Display(Name = "Purchase Invoice *")]
        public int po_id { get; set; }
        [Display(Name = "Document Number")]
        public string document_no { get; set; }
        [Display(Name = "Category *")]
        public int category_id { get; set; }
        [Display(Name = "Posting Date *")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime posting_date { get; set; }
        [Display(Name = "Vendor *")]
        public int vendor_id { get; set; }
        [Display(Name = "Net Value *")]
        public double net_value { get; set; }
        [Display(Name = "Gross Value *")]
        public double gross_value { get; set; }
        [Display(Name = "Business Unit *")]
        public int business_unit_id { get; set; }
        [Display(Name = "Plant *")]
        public int? plant_id { get; set; }
        [Display(Name = "Freight Terms *")]
        public int freight_terms_id { get; set; }
        [Display(Name = "Delivery Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? delivery_date { get; set; }
        [Display(Name = "Vendor Document Number ")]
        public string vendor_document_no { get; set; }
        [Display(Name = "Vendor Document Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? vendor_document_date { get; set; }
        [Display(Name = "Gate Entry Number")]
        public string gate_entry_no { get; set; }
        [Display(Name = "Gate Entry Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? gate_entry_date { get; set; }
        [Display(Name = "Created By")]
        public int created_by { get; set; }
        [Display(Name = "Billing Address *")]
        [DataType(DataType.MultilineText)]
        public string billing_address { get; set; }
        [Display(Name = "Billing City *")]
        public string billing_city { get; set; }
        [Display(Name = "Billing State *")]
        public int billing_state_id { get; set; }
        [Display(Name = "Billing Pincode")]
        public string billing_pincode { get; set; }
        [Display(Name = "Billing Country *")]
        public int country_id { get; set; }
        [Display(Name = "Email")]
        public string email_id { get; set; }
        [Display(Name = "Payment Terms *")]
        public int payment_terms_id { get; set; }
        [Display(Name = "Payment Cycle *")]
        public int payment_cycle_id { get; set; }
        [Display(Name = "Payment Cycle Type *")]
        public int payment_cycle_type_id { get; set; }
        [Display(Name = "PAN No")]
        public string pan_no { get; set; }
        [Display(Name = "ECC No")]
        public string ecc_no { get; set; }
        [Display(Name = "VAT TIN No")]
        public string vat_tin_no { get; set; }
        [Display(Name = "Service Tax No")]
        public string service_tax_no { get; set; }
        [Display(Name = "GST No")]
        public string gst_no { get; set; }
        [Display(Name = "CST TIN No")]
        public string cst_tin_no { get; set; }
        [Display(Name = "Created Time")]
        public DateTime created_ts { get; set; }
        public bool is_active { get; set; }
        public int net_currency_id { get; set; }
        public int gross_currency_id { get; set; }
        [Display(Name = "Form")]
        public int? form_id { get; set; }
        public string gst_in { get; set; }
        public int gst_vendor_type_id { get; set; }
        public string deleteids { get; set; }
        public string payment_cycle_type_name { get; set; }
        public string country_name { get; set; }
        public string state_name { get; set; }
        public string category_name { get; set; }
        public string freight_term { get; set; }
        public string net_currency_name { get; set; }
        public string gross_currency_name { get; set; }
        public string payment_cycle_name { get; set; }
        public string payment_term_name { get; set; }
        public string plant_name { get; set; }
        public string vendor_name { get; set; }
        public string item_service_name { get; set; }
        public string po_no { get; set; }
        public virtual List<pur_pi_return_detail> pur_pi_return_detail { get; set; }
        public virtual List<pur_pi_return_form> pur_pi_return_form { get; set; }
        [Display(Name = "Internal Remarks")]
        [DataType(DataType.MultilineText)]
        public string internal_remarks { get; set; }
        [Display(Name = "Remarks On Document")]
        [DataType(DataType.MultilineText)]
        public string remarks_on_doc { get; set; }
        [NotMapped]
        public HttpPostedFileBase FileUpload { get; set; }
        public string attachment { get; set; }
        [Display(Name = "TDS Code")]
        public int? tds_code_id { get; set; }
        public double? round_off { get; set; }
        public List<int> pi_return_detail_id1 { get; set; }
        public List<int> pi_id1 { get; set; }
        public List<int> pi_detail_id1 { get; set; }
        public List<int> item_id1 { get; set; }
        public List<int> sloc_id1 { get; set; }
        public List<int> uom_id1 { get; set; }
        public List<double> quantity1 { get; set; }
        public List<double> unit_price1 { get; set; }
        public List<double> discount1 { get; set; }
        public List<double> eff_unit_price1 { get; set; }
        public List<double> purchase_value1 { get; set; }
        public List<double> assessable_rate1 { get; set; }
        public List<double> assessable_value1 { get; set; }
        public List<int> tax_id1 { get; set; }
        public List<double> grir_value1 { get; set; }
        public List<double> basic_value1 { get; set; }
        public List<int> bucket_id1 { get; set; }
        public List<int> batch_id1 { get; set; }

        public string vendor_code { get; set; }
        public string business_name { get; set; }
        public string business_desc { get; set; }
        public string plant_desc { get; set; }
        public string plant_code { get; set; }
        public string form_name { get; set; }
        public string tds_code_name { get; set; }
        public string pi_name { get; set; }
        public int status_id { get; set; }
        [ForeignKey("status_id")]
        public virtual ref_status ref_status { get; set; }

        [Display(Name = "Storage Location*")]
        public int storage_location_id { get; set; }
        public string customer_vendor_name { get; set; }
        public string billing_state_code { get; set; }
        public string our_state_name { get; set; }
        public DateTime posting_dt { get; set; }

        public string email { get; set; }



        public string item_name { get; set; }

        public decimal quantity { get; set; }

        public decimal discount { get; set; }

        public decimal purchase_value { get; set; }

        public string tax_type { get; set; }

        public decimal tax_element_value { get; set; }
        public decimal tax_element_rate { get; set; }

        public string tax_element_code1 { get; set; }

        public decimal cgst { get; set; }

        public decimal sgst { get; set; }

        public decimal igst { get; set; }

        public string company_address { get; set; }

        public string tax_value { get; set; }

        public string gst_number { get; set; }

        public string remarks { get; set; }

        public string state_code { get; set; }

        public string currency { get; set; }
        public string pay_term { get; set; }
        public string COMPANY_NAME { get; set; }
        public string REGISTERED_ADDRESS { get; set; }
        public string WEBSITE { get; set; }
        public string CIN_NO { get; set; }
        public string CORPORATE_CITY { get; set; }
        public string bill_addr { get; set; }

        public string ven_g_name { get; set; }

        public string WEBSITE1 { get; set; }

        public double amount { get; set; }
        public double total_tax { get; set; }

        public double cgst_amount { get; set; }
        public double sgst_amount { get; set; }
        public double igst_amount { get; set; }

        public decimal? pi_gross_value { get; set; }
        public decimal? pi_net_value { get; set; }

        public float eff_unit_price { get; set; }

        public string plant_email { get; set; }
        public string gst_no_plant { get; set; }

    }
}
