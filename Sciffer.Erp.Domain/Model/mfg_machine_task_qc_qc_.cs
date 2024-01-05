using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class mfg_machine_task_qc_qc
    {
        [Key]
        public int machine_task_qc_qc_id { get; set; }
        public DateTime machine_task_qc_qc_date { get; set; }
        public TimeSpan machine_task_qc_qc_time { get; set; }

        public int machine_task_id { get; set; }
        [ForeignKey("machine_task_id")]
        public virtual mfg_machine_task mfg_machine_task { get; set; }

        public int mfg_qc_reason_id { get; set; }
        [ForeignKey("mfg_qc_reason_id")]
        public virtual ref_mfg_qc_reason ref_mfg_qc_reason { get; set; }

        public int tag_id { get; set; }
        [ForeignKey("tag_id")]
        public virtual inv_item_batch_detail_tag inv_item_batch_detail_tag { get; set; }

        public int tag_lifetime_count { get; set; }
        public int tag_shift_count { get; set; }
        public int tag_item_count { get; set; }
        public int machine_id { get; set; }
        public int operator_id { get; set; }

        public int current_status_id { get; set; }
        [ForeignKey("current_status_id")]
        public virtual ref_mfg_machine_task_status ref_mfg_machine_task_status { get; set; }

        public bool is_corr_qc_triggered { get; set; }
        public int create_user { get; set; }
        public DateTime create_ts { get; set; }
        public int modify_user { get; set; }
        public DateTime modify_ts { get; set; }
        

    }
}
