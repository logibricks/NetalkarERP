using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_mfg_bom_detail
    {
        [Key]
        public int mfg_bom_detail_id { get; set; }
        public int mfg_bom_id { get; set; }
        [ForeignKey("mfg_bom_id")]
        public virtual ref_mfg_bom ref_mfg_bom { get; set; }

        public int in_item_group_id { get; set; }
        [ForeignKey("in_item_group_id")]
        public virtual REF_ITEM_CATEGORY REF_ITEM_CATEGORY { get; set; }

        public int in_item_id { get; set; }
        [ForeignKey("in_item_id")]
        public virtual REF_ITEM REF_ITEM { get; set; }
        public double in_item_qty { get; set; }

        public int in_uom_id { get; set; }
        [ForeignKey("in_uom_id")]
        public virtual REF_UOM REF_UOM { get; set; }

    }
}
