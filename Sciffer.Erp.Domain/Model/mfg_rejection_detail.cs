using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class mfg_rejection_detail
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int rejection_detail_id { get; set; }

        public int tag_id { get; set; }
        [ForeignKey("tag_id")]
        public virtual inv_item_batch_detail_tag inv_item_batch_detail_tag { get; set; }

        public int machine_id { get; set; }
        [ForeignKey("machine_id")]
        public virtual ref_machine ref_machine { get; set; }

        public int operator_id { get; set; }
        [ForeignKey("operator_id")]
        public virtual REF_USER REF_USER { get; set; }

        public string root_cause_details { get; set; }

        public int nc_status_id { get; set; }
        [ForeignKey("nc_status_id")]
        public virtual ref_mfg_nc_status ref_mfg_nc_status { get; set; }

        public string nc_details { get; set; }
        public string action_plan { get; set; }
        public DateTime create_ts { get; set; }

        public int machine_task_qc_qc_id { get; set; }
        [ForeignKey("machine_task_qc_qc_id")]
        public virtual mfg_machine_task_qc_qc mfg_machine_task_qc_qc { get; set; }

        public string remarks { get; set; }

        public string why1 { get; set; }
        public string why2 { get; set; }
        public string why3 { get; set; }
        public string why4 { get; set; }
        public string why5 { get; set; }
        public string nc_tag_number { get; set; }


    }
}
