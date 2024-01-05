using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class mfg_qc_VM
    {
        public int mfg_qc_id { get; set; }

        public int machine_id { get; set; }
        public string machine_desc { get; set; }
        public string machine_code { get; set; }
        public int lifetime_count { get; set; }
        public int shift_count { get; set; }
        public int item_count { get; set; }
        public bool is_machine_blocked { get; set; }
    }
}
