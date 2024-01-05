using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class mfg_machine_task_VM
    {
        public int? machine_task_id { get; set; }
        public int? prod_order_id { get; set; }
        public int? prod_order_detail_id { get; set; }
        public string prod_order_no { get; set; }
        public int? tag_id { get; set; }
        public string tag_no { get; set; }
        public string machine_id { get; set; }
        public string machine_name { get; set; }
        public int? operator_id { get; set; }
        public string operator_name { get; set; }
        public DateTime? task_time { get; set; }
        public int status_id { get; set; }
        public string status_name { get; set; }
        public int? in_item_id { get; set; }
        public string in_item_desc { get; set; }
        public int? item_batch_id { get; set; }
        public string item_batch_no { get; set; }
        public string heat_code { get; set; }
        public string run_code { get; set; }
    }
}
