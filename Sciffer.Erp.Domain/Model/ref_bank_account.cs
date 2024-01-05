using Sciffer.Erp.Domain.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Sciffer.Erp.Domain.Model
{
    public class ref_bank_account
    {
        [Key]
        public int bank_account_id { get; set; }
        [StringLength(5, ErrorMessage = "The maximum length possible is 5")]
        public string bank_account_code { get; set; }

        public int bank_id { get; set; }
        [ForeignKey("bank_id")]
        public virtual ref_bank ref_bank { get; set; }
        public string bank_branch { get; set; }

        public string bank_city { get; set; }

        public int bank_state_id { get; set; }
        [ForeignKey("bank_state_id")]
        public virtual REF_STATE REF_STATE { get; set; }

        public int bank_account_type_id { get; set; }
        [ForeignKey("bank_account_type_id")]
        public virtual REF_ACCOUNT_TYPE REF_ACCOUNT_TYPE { get; set; }

        public string bank_account_number { get; set; }

        public string bank_ifsc_code { get; set; }

        public string bank_swift_code { get; set; }

        public int bank_currency_id { get; set; }
        [ForeignKey("bank_currency_id")]
        public virtual REF_CURRENCY REF_CURRENCY { get; set; }

        public int gl_account_id { get; set; }
        [ForeignKey("gl_account_id")]
       
        public virtual ref_general_ledger REF_GENERAL_LEDGER { get; set; }
        public bool? is_active { get; set; }
        public bool is_blocked { get; set; }
    }

}