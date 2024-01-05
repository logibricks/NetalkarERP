using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    class ref_task_periodicity_vm
    {
        public int task_periodicity_id { get; set; }
        public string task_periodicity_name { get; set; }
        public bool is_active { get; set; }
        public int? created_by { get; set; }
        public DateTime? created_ts { get; set; }
        public int? modified_by { get; set; }
        public DateTime? modified_ts { get; set; }
    }
}
