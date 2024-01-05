using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class process_machine_mapping_VM
    {
        public List<String> process_id { get; set; }
        public List<String> process_code { get; set; }
        public List<String> machine_id { get; set; }
        public List<String> machine_code { get; set; }

        public int process_id_get { get; set; }
        public string process_desc { get; set; }
        public string machine_id_get { get; set; }
        public string machine_desc { get; set; }
    }
    public class ref_mfg_process_vm
    {
        public int process_id { get; set; }
        public string process_description { get; set; }
        public string process_code { get; set; }
        public int? operation_id { get; set; }
        public bool is_blocked { get; set; }

    }
}
