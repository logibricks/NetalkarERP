using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class mfg_incentive_rule
    {
        [Key]
        public int incentive_rule_id { get; set; }

        public int machine_id { get; set; }
        [ForeignKey("machine_id")]
        public virtual ref_machine ref_machine { get; set; }

        public int item_id { get; set; }
        [ForeignKey("item_id")]
        public virtual REF_ITEM REF_ITEM { get; set; }

        public double min_prod_qty { get; set; }
        public double incentive_per_qty { get; set; }
        public DateTime effective_date { get; set; }
        public bool is_blocked { get; set; }

        public bool is_active { get; set; }
        public int created_by { get; set; }
        public int modified_by { get; set; }
        public DateTime created_on { get; set; }
        public DateTime modified_on { get; set; }
    }
}
