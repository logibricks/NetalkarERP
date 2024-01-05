using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
    public  class fin_bank_reco
    {
        [Key]
        public int fin_bank_reco_id { get; set; }
        public int category_id { get; set; }
        public string document_no { get; set; }
        public DateTime posting_date { get; set; }
        public DateTime reco_start_date { get; set; }
        public DateTime reco_end_date { get; set; }
        public int bank_account_id { get; set; }
        public int currency_id { get; set; }
        public double user_closing_bal { get; set; }
        public double ledger_closing_bal { get; set; }
        public double payment_total { get; set; }
        public double receipt_total { get; set; }
        public double payment_not_sel_total { get; set; }
        public double receipt_not_sel_total { get; set; }
        public double derived_closing_bal { get; set; }
        public bool? is_start { get; set; }
        public bool? is_final { get; set; }
       // public virtual ICollection<fin_bank_reco_payment> fin_bank_reco_payment { get; set; }
       // public virtual ICollection<fin_bank_reco_receipt> fin_bank_reco_receipt { get; set; }
    }
}
