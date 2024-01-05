
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_machine_details
    {
        [Key]
        public int machine_detail_id { get; set; }
        public int item_id { get; set; }
        [ForeignKey("item_id")]
        public virtual REF_ITEM REF_ITEM { get; set; }
        public int initial_received_qty { get; set; }
        public int suggested_qty { get; set; }
        public int sr_no { get; set; }
        public int current_qty { get; set; }
        public bool is_active { get; set; }
        public int machine_id { get; set; } 

    }
}
