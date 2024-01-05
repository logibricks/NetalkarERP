using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_item_category_gl
    {
        [Key]
        [Column(Order = 0)]
        public int item_category_id { get; set; }
        [ForeignKey("item_category_id")]
        public REF_ITEM_CATEGORY REF_ITEM_CATEGORY { get; set; }

        [Key]
        [Column(Order = 1)]
        public int ledger_account_type_id { get; set; }
        [ForeignKey("ledger_account_type_id")]
        public ref_ledger_account_type ref_ledger_account_type { get; set; }

        public int gl_ledger_id { get; set; }
        [ForeignKey("gl_ledger_id")]
        public ref_general_ledger ref_general_ledger { get; set; }
    }
}
