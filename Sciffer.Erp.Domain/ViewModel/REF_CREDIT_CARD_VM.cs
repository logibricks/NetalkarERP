using Sciffer.Erp.Domain.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Sciffer.Erp.Domain.ViewModel
{
    public class REF_CREDIT_CARD_VM
    {
        [Key]
        public int credit_card_id { get; set; }
        [Display(Name = "Credit Card Code *")]
        public string credit_card_code { get; set; }
        [Display(Name = "Credit Card Issuing Bank")]
        public int bank_id { get; set; }
        [ForeignKey("bank_id")]
        public virtual ref_bank ref_bank { get; set; }
        public string bank_name { get; set; }
        public string bank_code { get; set; }
        public string country_name { get; set; }
        [Display(Name = "Country *")]
        public int country_id { get; set; }
        [ForeignKey("country_id")]
        public virtual REF_COUNTRY REF_COUNTRY { get; set; }
        [Display(Name = "Credit Card Number *")]
        public Int64 credit_card_number { get; set; }
        public string Currency { get; set; }
        [Display(Name = "Currency *")]
        public int currency_id { get; set; }
        [ForeignKey("currency_id")]
        public virtual REF_CURRENCY REF_CURRENCY { get; set; }
        [Display(Name = "GL Account *")]
        public int? gl_ledger_id { get; set; }
        [ForeignKey("gl_ledger_id")]
        public virtual ref_general_ledger REF_GENERAL_LEDGER { get; set; }
        public string gl_ledger_name { get; set; }
        public string gl_ledger_code { get; set; }
        public bool is_blocked { get; set; }

    }

    public class credit_card_vm
    {
        public int credit_card_id { get; set; }
        public string credit_card_code { get; set; }
        public string bankname { get; set; }
        public int bank_id { get; set; }
        public string bank_country { get; set; }
        public Int64 credit_card_number { get; set; }
        public int country_id { get; set; }
        public string currency { get; set; }
        public string gl_account { get; set; }
        public bool? is_active { get; set; }
        public bool is_blocked { get; set; }
        public int currency_id { get; set; }
        public int state_id { get; set; }
        public int gl_ledger_id { get; set; }
    }
}
