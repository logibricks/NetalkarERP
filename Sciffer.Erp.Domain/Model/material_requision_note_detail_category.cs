using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class material_requision_note_detail_category
    {
        [Key]
        public int material_requision_note_detail_category_id { get; set; }

        public int material_requision_note_detail_id { get; set; }
        [ForeignKey("material_requision_note_detail_id")]
        public virtual material_requision_note_detail material_requision_note_detail { get; set; }

        public int item_id { get; set; }
        [ForeignKey("item_id")]
        public virtual REF_ITEM REF_ITEM { get; set; }

        public int process_id { get; set; }
        [ForeignKey("process_id")]
        public virtual ref_mfg_process ref_mfg_process { get; set; }

        public int tool_usage_type_id { get; set; }
        [ForeignKey("tool_usage_type_id")]
        public virtual ref_tool_usage_type ref_tool_usage_type { get; set; }

        public int tool_category_id { get; set; }
        [ForeignKey("tool_category_id")]
        public virtual ref_tool_category ref_tool_category { get; set; }
    }
}
