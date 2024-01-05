using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Sciffer.Erp.Domain.Model
{
    public class ref_credit_card
    {
        [Key]
        public int credit_card_id { get; set; }
        public string credit_card_code { get; set; }
        public int bank_id { get; set; }    
        [ForeignKey("bank_id")]
        public virtual ref_bank ref_bank { get; set; }
        public int country_id { get; set; }
        [ForeignKey("country_id")]
        public virtual REF_COUNTRY REF_COUNTRY { get; set; }
        public Int64 credit_card_number { get; set; }
        public int currency_id { get; set; }
        [ForeignKey("currency_id")]
        public virtual REF_CURRENCY REF_CURRENCY { get; set; }
        public int? gl_ledger_id { get; set; }
        [ForeignKey("gl_ledger_id")]
        public virtual ref_general_ledger REF_GENERAL_LEDGER { get; set; }
        public bool? is_active { get; set; }
        public bool is_blocked { get; set; }
    }
}
