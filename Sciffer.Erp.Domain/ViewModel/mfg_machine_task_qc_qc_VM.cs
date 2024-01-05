using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class mfg_machine_task_qc_qc_VM
    {
        public int machine_task_qc_qc_id { get; set; }
        public string machine_task_qc_qc_no { get; set; }
        public DateTime machine_task_qc_qc_date { get; set; }
        public TimeSpan machine_task_qc_qc_time { get; set; }

        public int? machine_task_id { get; set; }
        public int? prod_order_id { get; set; }
        public int? prod_order_detail_id { get; set; }
        public string prod_order_no { get; set; }

        public int? tag_id { get; set; }
        public string tag_no { get; set; }
        public int machine_id { get; set; }
        public string machine_desc { get; set; }
        public int? operator_id { get; set; }
        public DateTime task_time { get; set; }
        
        public int? in_item_id { get; set; }
        public string in_item_desc { get; set; }
        public int? item_batch_id { get; set; }
        public string item_batch_no { get; set; }
        
        public int? mfg_qc_reason_id { get; set; }
        public string mfg_qc_reason_desc { get; set; }
       
        public int? current_status_id { get; set; }
        public string current_status { get; set; }
        
        public bool is_corr_qc_triggered { get; set; }

        
        
    }
}
