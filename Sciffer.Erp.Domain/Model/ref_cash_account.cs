using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_cash_account
    {
        [Key]
        public int cash_account_id { get; set; }
        [Display(Name = "Account Code *")]
        public string cash_account_code { get; set; }
        [Display(Name = "Description *")]
        public string cash_account_desc { get; set; }
        [Required]
        [Display(Name = "Currency *")]
        public int currency_id { get; set; }
        [ForeignKey("currency_id")]     
        public virtual REF_CURRENCY REF_CURRENCY { get; set; }
        public bool is_active { get; set; }
        [Display(Name = "Blocked ")]
        public bool is_blocked { get; set; }
        [Display(Name = "GL Account *")]
        public int gl_ledger_id { get; set; }
        [ForeignKey("gl_ledger_id")] 
        public virtual ref_general_ledger ref_general_ledger { get; set; }
    }

}
