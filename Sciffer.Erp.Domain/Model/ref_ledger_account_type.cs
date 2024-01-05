using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
   public class ref_ledger_account_type
    {
        [Key]
        public int ledger_account_type_id { get; set; }
        [Required]
        [Display(Name ="Ledger Account Type")]
        public string ledger_account_type_name { get; set; }
        public int entity_type_id { get; set; }
        [ForeignKey("entity_type_id")]
        public REF_ENTITY_TYPE REF_ENTITY_TYPE { get; set; }
        public int? item_type_id { get; set; }
    }
}
