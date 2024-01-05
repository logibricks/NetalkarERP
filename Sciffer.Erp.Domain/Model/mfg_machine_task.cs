using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class mfg_machine_task
    {
        [Key]
        public int machine_task_id { get; set; }

        public int prod_order_detail_id { get; set; }
        [ForeignKey("prod_order_detail_id")]
        public virtual mfg_prod_order_detail mfg_prod_order_detail { get; set; }

        public int tag_id { get; set; }
        [ForeignKey("tag_id")]
        public virtual inv_item_batch_detail_tag inv_item_batch_detail_tag { get; set; }

        public string machine_id { get; set; }
       
        public int? operator_id { get; set; }
        public DateTime? task_time { get; set; }

        public int machine_task_status_id { get; set; }
        [ForeignKey("machine_task_status_id")]
        public virtual ref_mfg_machine_task_status ref_mfg_machine_task_status { get; set; }
        public int? process_id { get; set; }
        [ForeignKey("process_id")]
        public virtual ref_mfg_process ref_mfg_process { get; set; }

    }
}
