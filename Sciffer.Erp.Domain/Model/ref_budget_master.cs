using Sciffer.Erp.Domain.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
   public  class ref_budget_master
    {
        [Key]
        public int budget_id { get; set; }
       
        public int financial_year_id { get; set; }
        [ForeignKey("financial_year_id")]
        public virtual REF_FINANCIAL_YEAR  ref_financial_year {get; set;}
        public int? general_ledger_id { get; set; }
        [ForeignKey("general_ledger_id")]
        public virtual ref_general_ledger ref_general_ledger { get; set; }
        public double amount { get; set; }
        public int instruction_type_id { get; set;}
        [ForeignKey("instruction_type_id")]
        public virtual ref_instruction_type  ref_instruction_type { get; set; }
        public bool? is_active { get; set; }
        public bool? is_blocked { get; set; }
    }



    public class ref_budget_mastervm
    {
        public int budget_id { get; set; }
        public int financial_year_id { get; set; }
        public int? general_ledger_id { get; set; }
        public double amount { get; set; }
        public int instruction_type_id { get; set; }
        public bool? is_active { get; set; }
        public bool? is_blocked { get; set; }
        public string financial_year_name { get; set; }
        public string general_ledger_name { get; set; }
        public string instruction_name { get; set; }
        public string financial_year { get; set; }
    }
}
