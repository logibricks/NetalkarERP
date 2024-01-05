using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.ViewModel
{
    public  class fin_bank_reco_vm
    {
        [Key]
        public int fin_bank_reco_id { get; set; }
        [Display(Name = "Category *")]
        public int category_id { get; set; }
        [ForeignKey("category_id")]
        public virtual ref_document_numbring ref_document_numbring { get; set; }
        public string category_name { get; set; }
        public string document_no { get; set; }
        [Display(Name = "Posting Date *")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime posting_date { get; set; }
        [Display(Name = "From Date *")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime reco_start_date { get; set; }
        [Display(Name = "To Date *")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime reco_end_date { get; set; }
        [Display(Name = "Bank Account *")]
        public int bank_account_id { get; set; }
        public string bank_account_code { get; set; }
        public string bank_account_number { get; set; }
        [Display(Name = "Currency *")]
        public int currency_id { get; set; }
        [ForeignKey("currency_id")]
        public virtual REF_CURRENCY REF_CURRENCY { get; set; }
        public string currency_name { get; set; }
        [Display(Name = "Enter Bank Statement Closing Balance")]
        public double user_closing_bal { get; set; }
        [Display(Name = "Closing Balance as per Ledger")]
        public double ledger_closing_bal { get; set; }
        [Display(Name = "Payments Total")]
        public double payment_total { get; set; }
        [Display(Name = "Receipt Total")]
        public double receipt_total { get; set; }
        [Display(Name = "Payments not reflected in bank statement")]
        public double payment_not_sel_total { get; set; }
        [Display(Name = "Receipts not reflected in the bank statement")]
        public double receipt_not_sel_total { get; set; }
        [Display(Name = "Entered Bank Statement Balance ")]
        public double derived_closing_bal { get; set; }
        public bool? is_start { get; set; }
        public bool? is_final { get; set; }
        public virtual IList<fin_bank_payment_receipt_reco_vm> fin_bank_payment_receipt_reco_vm { get; set; }       
        public List<string> fin_ledger_payment_detail_id { get; set; }
        public List<string> doc_category_id { get; set; }
        public List<string> doc_no { get; set; }
        public List<string> doc_posting_date { get; set; }
        public List<string> entity_type_id { get; set; }
        public List<string> entity_id { get; set; }
        public List<string> bank_tran_date { get; set; }
        public List<string> amount { get; set; }
        public List<string> fin_ledger_payment_detail_id1 { get; set; }
        public List<string> doc_category_id1 { get; set; }
        public List<string> doc_no1 { get; set; }
        public List<string> doc_posting_date1 { get; set; }
        public List<string> entity_type_id1 { get; set; }
        public List<string> entity_id1 { get; set; }
        public List<string> bank_tran_date1 { get; set; }
        public List<string> amount1 { get; set; }

    }

    public class fin_bank_payment_receipt_reco_vm
    {
        public int category_id { get; set; }
        public string category { get; set; }
        public string document_no { get; set; }
        public DateTime payment_date { get; set; }
        public int entity_type_id { get; set; }
        public string entity_type_name { get; set; }
        public int entity_id { get; set; }
        public string entity_code { get; set; }
        public string entity_description { get; set; }
        public double amount { get; set; }
        public int in_out { get; set; }
        public int fin_ledger_payment_detail_id { get; set; }
        public DateTime? bank_tran_date { get; set; }

    }
}
