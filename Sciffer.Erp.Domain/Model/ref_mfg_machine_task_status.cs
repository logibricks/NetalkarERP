using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_mfg_machine_task_status
    {
        [Key]
        public int machine_task_status_id { get; set; }
        public string machine_task_status_name { get; set; }
    }
}
