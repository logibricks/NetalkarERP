﻿using Sciffer.Erp.Domain.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Sciffer.Erp.Domain.ViewModel
{
    public class ref_bank_account_vm
    {
        [Display(Name ="Blocked")]
        public bool is_blocked { get; set; }

        public int bank_account_id { get; set; }
        public string bank_name { get; set; }
        public string bank_code { get; set; }
        [Display(Name = "Bank Account Code *")]
        public string bank_account_code { get; set; }
        [Display(Name = "Bank *")]
        public int bank_id { get; set; }
        public bool is_active { get; set; }
        [Display(Name = "Branch *")]
        public string bank_branch { get; set; }

        [Display(Name = "City *")]
        public string bank_city { get; set; }

        [Display(Name = "State *")]
        public int bank_state_id { get; set; }
        [ForeignKey("bank_state_id")]
        public virtual REF_STATE REF_STATE { get; set; }

        [Display(Name = "Country *")]
        public int BANK_COUNTRY_ID { get; set; }
        [ForeignKey("BANK_COUNTRY_ID")]
        public virtual REF_COUNTRY REF_COUNTRY { get; set; }

        [Display(Name = "Account Type *")]
        public int bank_account_type_id { get; set; }
        [ForeignKey("bank_account_type_id")]
        public virtual REF_ACCOUNT_TYPE REF_ACCOUNT_TYPE { get; set; }

        [Display(Name = "Account Number *")]
        public string bank_account_number { get; set; }

        [Display(Name = "IFSC Code *")]
        public string bank_ifsc_code { get; set; }

        [Display(Name = "SWIFT Code")]
        public string bank_swift_code { get; set; }


        [Display(Name = "Currency *")]
        public int bank_currency_id { get; set; }
        [ForeignKey("bank_currency_id")]
        public virtual REF_CURRENCY REF_CURRENCY { get; set; }

        [Display(Name = "GL Account *")]
        public int gl_account_id { get; set; }
        [ForeignKey("gl_account_id")]
        public virtual ref_general_ledger REF_GENERAL_LEDGER { get; set; }

    }

    public class ref_bank_accountvm
    {
        public string bank_account_code { get; set; }
        public int bank_account_id { get; set; }
        public int bank_id { get; set; }
        public bool? is_active { get; set; }
        public string bank_code { get; set; }
        public string bank_name { get; set; }
        public string bank_branch { get; set; }
        public string bank_city { get; set; }
        public string bank_state { get; set; }
        public int bank_state_id { get; set; }
        public string bank_account_type { get; set; }
        public int bank_account_type_id { get; set; }
        public string bank_account_number { get; set; }
        public string bank_ifsc_code { get; set; }
        public string bank_swift_code { get; set; }
        public string bank_currency { get; set; }
        public int bank_currency_id { get; set; }
        public string gl_account { get; set; }
        public int gl_account_id { get; set; }
        public string country_name { get; set; }
        public int BANK_COUNTRY_ID { get; set; }
        public bool is_blocked { get; set; }
    }
}