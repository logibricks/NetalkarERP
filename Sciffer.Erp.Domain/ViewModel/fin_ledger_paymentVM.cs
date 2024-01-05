using System;
using System.Collections.Generic;
using Sciffer.Erp.Domain.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class fin_ledger_paymentVM
    {
        [Key]
        public int fin_ledger_payment_id { get; set; }

        [Display(Name = "IN/OUT * ")]
        public int in_out { get; set; }
        [Display(Name = "Category *")]
        public int category_id { get; set; }
        [Display(Name = "Cash/Bank *")]
        public int cash_bank { get; set; }

        [Display(Name = "Payment Method *")]
        [Required(ErrorMessage = "payment type id is required")]
        public int payment_type_id { get; set; }
        [ForeignKey("payment_type_id")]

        [Display(Name = "Bank Account *")]
        [Required(ErrorMessage = "bank account id is required")]
        public int bank_account_id { get; set; }


        [Display(Name = "Amount *")]
        [Required(ErrorMessage = "payment amount is required")]
        public double payment_amount { get; set; }

        [Display(Name = "Currency *")]
        [Required(ErrorMessage = "currency id is required")]
        public int currency_id { get; set; }
        [ForeignKey("currency_id")]

        [Display(Name = "Entity Type *")]

        public int entity_type_id { get; set; }
        [ForeignKey("entity_type_id")]

        [Display(Name = "Remarks")]
        [DataType(DataType.MultilineText)]
        public string remarks { get; set; }
        [Display(Name ="Entity *")]
        public int? party_id { get; set; }

        [Display(Name = "Document Number *")]
        public string document_no { get; set; }

        [Display(Name = "Date *")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "payment date is required")]
        public DateTime payment_date { get; set; }

        public string payment_type_name { get; set; }
        public string CURRENCY_NAME { get; set; }
        public string bank_name { get; set; }
        public string ENTITY_TYPE_NAME { get; set; }
        public string bank_account_number { get; set; }
        public string payment_doc_detail { get; set; }
        public string payment_entity_detail { get; set; }
        public virtual List<entity_transaction_detail> entity_transaction_detail { get; set; }
        public virtual List<payment_receipt_entity_detail> payment_receipt_entity_detail { get; set; }
        public List<string> entity_id { get; set; }
        public List<string> amount { get; set; }
        public List<string> tran_ref_no { get; set; }
        public List<string> document_type_code { get; set; }
        public List<string> document_id { get; set; }
        public List<string> adjust_amount { get; set; }
        public List<string> entity_id1 { get; set; }
        public List<string> fin_ledger_detail_id { get; set; }
        public List<string> fin_ledger_payment_detail_id { get; set; }
        public List<string> on_account_amount { get; set; }
        public List<string> round_off_amount { get; set; }
        public List<string> bank_charges_amount { get; set; }
        [Display(Name = "Cancellation Remarks")]
        [DataType(DataType.MultilineText)]
        public string cancellation_remarks { get; set; }
        [Display(Name = "Cancellation Reason")]
        public int? cancellation_reason_id { get; set; }
        public string status_name { get; set; }
        [Display(Name = "Status")]
        public int? status_id { get; set; }
        public virtual ref_status ref_status { get; set; }
        [Display(Name = "Cancelled Date")]
        public DateTime? cancelled_date { get; set; }
        [Display(Name = "Cancelled By")]
        public int? cancelled_by { get; set; }
        [Display(Name = "Created Date")]
        public DateTime? created_ts { get; set; }
        [Display(Name = "Created By")]
        public int? created_by { get; set; }
    }

    public class fin_payment_receipt_vm
    {
        public int? fin_ledger_payment_id { get; set; }
        public string in_out { get; set; }
        public string cash_bank { get; set; }
        public string payment_type_name { get; set; }
        public string bank_account { get; set; }
        public double? amount { get; set; }
        public decimal? payment_amount { get; set; }
        public string currency_name { get; set; }
        public string document_no { get; set; }
        public DateTime? payment_date { get; set; }
        public string posting_date { get; set; }
        public string remarks { get; set; }
        public string entity_type_name { get; set; }
        public string entity_code { get; set; }
        public string entity_name { get; set; }
        public string company_name { get; set; }
        public string vendor_address { get; set; }
        public string tran_ref_no { get; set; }
        public string cancellation_remarks { get; set; }
    }
    public class entity_transaction_detail
    {
        public int? fin_ledger_detail_id { get; set; }
        public int? entity_id { get; set; }
        public int? entity_type_id { get; set; }
        public string entity_code { get; set; }
        public string entity_name { get; set; }
        public string document_type_name { get; set; }
        public int? source_document_id { get; set; }
        public string source_document_no { get; set; }
        public string document_date { get; set; }
        public DateTime? ledger_date { get; set; }
        public DateTime? due_date { get; set; }
        public double? amount { get; set; }
        public double? balance { get; set; }
        public string ref1 { get; set; }
        public string ref2 { get; set; }
        public string ref3 { get; set; }
        public decimal? setoff_amount_local { get; set; }
        public decimal? amount_local { get; set; }
        public string narration { get; set; }

    }
    public class payment_receipt_entity_detail
    {
        public int fin_ledger_payment_detail_id { get; set; }
        public int entity_type_id { get; set; }
        public int entity_id { get; set; }
        public string entity_code { get; set; }
        public string entity_name { get; set; }
        public double amount { get; set; }
        public string tran_ref_no { get; set; }
        public double? on_account_amount { get; set; }
        public double? round_off_amount { get; set; }
        public double? bank_charges_amount { get; set; }
    }
    public class fin_payment_receipt_report
    { 
        public int? fin_ledger_payment_id { get; set; }
        public string category { get; set; }
        public string document_no { get; set; }
        public DateTime? payment_date { get; set; }
        public double? payment_amount { get; set; }
        public string payment_type_name { get; set; }
        public string gl_code { get; set; }
        public string gl_name { get; set; }
        public string tran_ref_no { get; set; }
        public int? parent_id { get; set; }
        public int? rownumber { get; set; }

        public string entity_type_name { get; set; }
        public string entity_code { get; set; }
        public string source_document_no { get; set; }
        public string document_type_name { get; set; }
        public double? setoff_amount_local { get; set; }
        public DateTime? ledger_date { get; set; }
        public DateTime? due_date { get; set; }
        public string Sending_Code { get; set; }
        public string Receiving_Code { get; set; }
        public string sending { get; set; }
        public string receiving { get; set; }
        public string entity_name { get; set; }
        public string reference { get; set; }
        public double? transfer_amount { get; set; }
        public DateTime? posting_date { get; set; }
        
    }
}
