using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class fin_credit_debit_note_vm
    {
        [Key]
        public int fin_credit_debit_node_id { get; set; }
        [Display(Name ="Category *")]
        public int category_id { get; set; }
        public string category_name { get; set; }
        public string document_no { get; set; }
        public string credit_debit_name { get; set; }
        public int credit_debit_id { get; set; }
        [Display(Name ="Entity Type *")]
        public int entity_type_id { get; set; }
        public string entity_type_name { get; set; }
        [Display(Name = "Entity Name *")]
        public int entity_id { get; set; }
        public string entity_code { get; set; }
        public string entity_name { get; set; }
        [Display(Name = "Posting Date *")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime posting_date { get; set; }
        [Display(Name = "Currency *")]
        public int currency_id { get; set; }
        public string currency_name { get; set; }
        [Display(Name = "Header Remarks")]
        [DataType(DataType.MultilineText)]
        public string remarks { get; set; }
        public double total_amount { get; set; }
        public decimal?  amount { get; set; }
        public virtual List<fin_credit_debit_note_detail> fin_credit_debit_node_detail { get; set; }
        public virtual List<fin_credit_debit_note_detail_vm> fin_credit_debit_note_detail_vm { get; set; }
        public List<string> fin_credit_debit_node_detail_id { get; set; }
        public List<string> gl_ledger_id1 { get; set; }
        public List<string> user_description { get; set; }
        public List<string> credit_debit_amount { get; set; }
        public List<string> tax_id1 { get; set; }   
        public List<string> cost_center_id1 { get; set; }
        public List<string> item_type_id1 { get; set; }
        public List<string> hsn_sac_id1 { get; set; }
        public List<string> tax_rate1 { get; set; }
        public List<string> exclusive_inclusive1 { get; set; }
        public string posting_dates { get; set; }
        public List<string> fin_credit_debit_note_transaction_id { get; set; }
        public List<string> module_form_code1 { get; set; }
        public List<string> document_id1 { get; set; }
        public List<string> taxable_value1 { get; set; }
        public List<string> dr_cr_amount1 { get; set; }

        public double? rate { get; set; }

        [Display(Name = "Business Unit")]
        public int business_unit_id { get; set; }
        [Display(Name = "Payment Terms")]
        public int payment_terms_id { get; set; }
        [Display(Name = "Payment Cycle")]
        public int payment_cycle_id { get; set; }
        [Display(Name = "Payment Cycle Type")]
        public int payment_cycle_type_id { get; set; }
        [Display(Name = "Billing Address")]
        public string billing_address { get; set; }
        [Display(Name = "Billing City")]
        public string billing_city { get; set; }
        public string billing_city_name { get; set; }
        [Display(Name = "Billing Pincode")]
        public string billing_pin_code { get; set; }
        [Display(Name = "Billing State")]
        public int billing_state_id { get; set; }
        public string billing_state_name { get; set; }
        [Display(Name = "Billing Country")]
        public int billing_country_id { get; set; }
        public string billing_country_name { get; set; }
        [Display(Name = "Email ID")]
        public string email_id { get; set; }
        [Display(Name = "GSTIN")]
        public string gstin_no { get; set; }
        [Display(Name = "GST Vendor Cateogry")]
        public int gst_category_id { get; set; }
        public string gst_category_name { get; set; }
        [Display(Name = "Internal Remarks")]
        [DataType(DataType.MultilineText)]
        public string internal_remarks { get; set; }
        [Display(Name = "Remarks on Document")]
        [DataType(DataType.MultilineText)]
        public string remarks_on_document { get; set; }
        [Display(Name = "Attachment")]
        public string attachement { get; set; }
        public string row_id { get; set; }
        [NotMapped]
        public HttpPostedFileBase FileUpload { get; set; }

        [Display(Name = "Vendor Document Number")]
        public string vendor_document_no { get; set; }
        [Display(Name = "Vendor Document Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? vendor_document_date { get; set; }
        [Display(Name = "RCM")]
        public bool is_rcm { get; set; }
        public double net_value { get; set; }
        public double gross_value { get; set; }
        public decimal? value { get; set; }
        public int plant_id { get; set; }
        public int company_id { get; set; }
        public int? status_id { get; set; }
        public string status_name { get; set; }
        public decimal round_off { get; set; }
        [Display(Name = "TDS Code ")]
        public int? tds_code_id { get; set; }
        [Display(Name = "PAN")]
        public string pan_no { get; set; }
        public string company_name { get; set; }     
        public string state_name { get; set; }
        public string plant_gstin_no { get; set; }
        public string billing_state_code { get; set; }
        public string company_state_code { get; set; }
        public string plant_code { get; set; }
        public string plant_address { get; set; }
        public string document_number { get; set; }
        public string document_date { get; set; }
        public decimal? cgst_amount { get; set; }
        public decimal? sgst_amount { get; set; }
        public decimal? igst_amount { get; set; }
        public decimal? total_tax { get; set; }
        public int? cancellation_reason_id { get; set; }
        public string cancellation_reason { get; set; }
        public string cancellation_remarks { get; set; }
        public int? cancelled_by { get; set; }
        public string cancelled_by_user { get; set; }
        public DateTime? cancellation_ts { get; set; }
        public string cancellation_date { get; set; }
        public virtual List<GetFinCreditDebitTransactionById> GetFinCreditDebitTransactionById { get; set; }
    }

    public class GetFinCreditDebitTransactionById
    {
        public string document_no { get; set; }
        public string category { get; set; }
        public string posting_date_st { get; set; }
        public string customer_name { get; set; }
        public string vendor_document_no { get; set; }
        public string vendor_document_date { get; set; }
        public decimal taxable_value { get; set; }
        public string vendor_name { get; set; }
        public string module_form_code { get; set; }
        public int fin_credit_debit_note_transaction_id { get; set; }
        public int fin_credit_debit_note_id { get; set; }
        public string doc_type_code { get; set; }
        public int document_id { get; set; }
        public decimal original_amount { get; set; }
        public decimal dr_cr_amount { get; set; }
        public decimal balance_net_value { get; set; }
        public decimal balance_gross_value { get; set; }
        public double net_value { get; set; }
        public double gross_value { get; set; }
        public string exclusive_inclusive { get; set; }
    }

}
