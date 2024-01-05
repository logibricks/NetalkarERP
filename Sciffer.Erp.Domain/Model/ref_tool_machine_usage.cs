using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_tool_machine_usage
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int tool_machine_usage_id { get; set; }

        public int tool_id { get; set; }
        [ForeignKey("tool_id")]
        public virtual REF_ITEM REF_ITEM { get; set; }

        public int machine_id { get; set; }
        [ForeignKey("machine_id")]
        public virtual ref_machine ref_machine { get; set; }

        public int tool_renew_type_id { get; set; }
        [ForeignKey("tool_renew_type_id")]
        public virtual ref_tool_renew_type ref_tool_renew_type { get; set; }
        
        public double current_life_percentage { get; set; }
        public DateTime start_date_time { get; set; }
        public bool in_use { get; set; }

        public bool is_active { get; set; }
        public int created_by { get; set; }
        public DateTime created_on { get; set; }
        public int modified_by { get; set; }
        public DateTime modified_on { get; set; }

        public virtual ICollection<ref_tool_machine_item_usage> ref_tool_machine_item_usage { get; set; }
    }
}
