using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class mfg_rejection_detail_vm
    {
        public int rejection_detail_id { get; set; }
        public int tag_id { get; set; }
        public int machine_id { get; set; }
        public string machine_name { get; set; }
        public int operator_id { get; set; }
        public string operator_name { get; set; }
        public string root_cause_details { get; set; }
        public int nc_status_id { get; set; }
        public string nc_status_desc { get; set; }
        public string nc_details { get; set; }
        public string action_plan { get; set; }
        public DateTime create_ts { get; set; }
        public string create_ts_str { get; set; }
        public int machine_task_qc_qc_id { get; set; }
        public string remarks { get; set; }
        public string rejected_onmachine_by { get; set; }
        public string created_ts_on { get; set; }
    }
}
