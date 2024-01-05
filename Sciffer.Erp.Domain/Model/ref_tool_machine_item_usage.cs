using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_tool_machine_item_usage
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int tool_machine_item_usage_id { get; set; }

        public int tool_machine_usage_id { get; set; }
        [ForeignKey("tool_machine_usage_id")]
        public virtual ref_tool_machine_usage ref_tool_machine_usage { get; set; }

        public int item_id { get; set; }
        [ForeignKey("item_id")]
        public virtual REF_ITEM REF_ITEM { get; set; }

        public double life_no_of_items { get; set; }
        public double per_unit_life_consumption { get; set; }

        public double no_of_items_processed { get; set; }
        public double item_life_consumption { get; set; }
        
        public double item_life_consumption_percentage { get; set; }
    }
}
