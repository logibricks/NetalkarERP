using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
  public  class ref_sub_ledger
    {
        [Key]
        public int sub_ledger_id { get; set; }
        [Required]
        [Display(Name = "Sub Ledger")]
        public string sub_ledger_name { get; set; }
        public int entity_type_id { get; set; }
        public int entity_id { get; set; }
        public int gl_ledger_id { get; set; }
        public int ledger_account_type_id { get; set; }
    }
}
