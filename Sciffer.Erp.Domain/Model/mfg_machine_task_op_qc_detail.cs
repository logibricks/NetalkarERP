using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class mfg_machine_task_op_qc_detail
    {
        [Key]
        public int machine_task_op_qc_detail_id { get; set; }

        public int machine_task_id { get; set; }
        [ForeignKey("machine_task_id")]
        public virtual mfg_machine_task mfg_machine_task { get; set; }

        public int mfg_op_qc_parameter_id { get; set; }
        [ForeignKey("mfg_op_qc_parameter_id")]
        public virtual mfg_op_qc_parameter mfg_op_qc_parameter { get; set; }

        public string status { get; set; }
    }
}
