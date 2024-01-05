using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class mfg_machine_item_upgradation_vm
    {
        public int mfg_upgradation_id { get; set; }
        public int process_id { get; set; }
        public string process_name { get; set; }
        public int item_id { get; set; }
        public string item_name { get; set; }
        public int machine_id { get; set; }
        public string machine_name { get; set; }
        public string file_name { get; set; }
        public string file_path { get; set; }
    }
}
