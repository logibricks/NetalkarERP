using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class mfg_shiftwise_production_master_details
    {
        [Key]
        public int shiftwise_production_details_id { get; set; }

        public int shiftwise_production_id { get; set; }
        [ForeignKey("shiftwise_production_id")]
        public virtual mfg_shiftwise_production_master mfg_shiftwise_production_master { get; set; }

        [Display(Name = "Operation_Code")]
        public string operation_code { get; set; }
        [Display(Name = "Operation_Name")]
        public string operation_name { get; set; }
        public int machine_id { get; set; }
        [ForeignKey("machine_id")]
        public virtual ref_machine ref_machine { get; set; }
        public int item_id { get; set; }
        [ForeignKey("item_id")]
        public virtual REF_ITEM REF_ITEM { get; set; }
        [Display(Name = "Cycle Time")]
        public TimeSpan cycle_time { get; set; }
        [Display(Name = "Std Production Qty")]
        public decimal std_prod_qty { get; set; }
        [Display(Name = "WIP Qty")]
        public decimal wip_qty { get; set; }
        [Display(Name = "Target Qty")]
        public decimal target_qty { get; set; }

    }
}
