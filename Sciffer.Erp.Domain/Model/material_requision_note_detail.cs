using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class material_requision_note_detail
    {
        [Key]
        public int material_requision_note_detail_id { get; set; }
        public int material_requision_note_id { get; set; }
        [ForeignKey("material_requision_note_id")]
        public virtual material_requision_note material_requision_note { get; set; }
        public int item_id { get; set; }
        [ForeignKey("item_id")]
        public virtual REF_ITEM REF_ITEM { get; set; }
        public int uom_id { get; set; }
        [ForeignKey("uom_id")]
        public virtual REF_UOM REF_UOM { get; set; }
        public double required_qty { get; set; }
        public int cost_center_id { get; set; }
        [ForeignKey("cost_center_id")]
        public virtual ref_cost_center ref_cost_center { get; set; }
        public int machine { get; set; }
        [ForeignKey("machine")]
        public virtual ref_machine ref_machine { get; set; }
        public int reason { get; set; }
        [ForeignKey("reason")]
        public virtual REF_REASON_DETERMINATION REF_REASON_DETERMINATION { get; set; }
        public int order_type { get; set; }
        public string order_number { get; set; }
        public string line_remarks { get; set; }
        public bool is_active { get; set; }
        public double rate { get; set; }
        public double? balance_qty { get; set; }
    }
}
