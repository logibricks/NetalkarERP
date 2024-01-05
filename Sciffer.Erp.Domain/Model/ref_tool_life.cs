using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_tool_life
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int tool_life_id { get; set; }

        public int tool_id { get; set; }
        [ForeignKey("tool_id")]
        public virtual REF_ITEM REF_ITEM1 { get; set; }

        public int tool_renew_type_id { get; set; }
        [ForeignKey("tool_renew_type_id")]
        public virtual ref_tool_renew_type ref_tool_renew_type { get; set; }

        public int machine_id { get; set; }
        [ForeignKey("machine_id")]
        public virtual ref_machine ref_machine { get; set; }

        public int item_id { get; set; }
        [ForeignKey("item_id")]
        public virtual REF_ITEM REF_ITEM { get; set; }

        public double life_no_of_items { get; set; }
        public double per_unit_life_consumption { get; set; }
        
        public bool is_active { get; set; }
        public int created_by { get; set; }
        public DateTime created_on { get; set; }
    }
}
