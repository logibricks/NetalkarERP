using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
   public class ref_task_log_vm
    {
        public int task_log_id { get; set; }
        public int? task_id { get; set; }
        public int? old_status_id { get; set; }
        public int? new_status_id { get; set; }
        public int? created_by { get; set; }
        public DateTime? created_ts { get; set; }
    }
}
