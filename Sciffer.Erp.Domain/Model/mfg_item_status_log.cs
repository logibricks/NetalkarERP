using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class mfg_item_status_log
    {
        [Key]
        public int item_status_log_id { get; set; }

        public int prod_order_detail_id { get; set; }
        [ForeignKey("prod_order_detail_id")]
        public virtual mfg_prod_order_detail mfg_prod_order_detail { get; set; }

        public int tag_id { get; set; }
        [ForeignKey("tag_id")]
        public virtual inv_item_batch_detail_tag inv_item_batch_detail_tag { get; set; }

        public string machine_id { get; set; }
       
        public int? operator_id { get; set; }

        public int machine_task_status_id { get; set; }
        [ForeignKey("machine_task_status_id")]
        public virtual ref_mfg_machine_task_status ref_mfg_machine_task_status { get; set; }

        public int machine_task_id { get; set; }
        [ForeignKey("machine_task_id")]
        public virtual mfg_machine_task mfg_machine_task { get; set; }

        public int create_user { get; set; }
        public DateTime create_ts { get; set; }
    }
}
