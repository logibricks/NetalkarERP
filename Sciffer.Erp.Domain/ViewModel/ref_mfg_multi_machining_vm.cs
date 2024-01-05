using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
   public class ref_mfg_multi_machining_vm
    {
        public int multi_machining_id { get; set; }
        public int machine_group_id { get; set; }
        public string machine_id { get; set; }
        public string machine_id1 { get; set; }
        public string machine_id2 { get; set; }
        public string machine_name1 { get; set; }
        public string machine_name2 { get; set; }
        public List<string> mac_id1 { get; set; }
        public List<string> mac_id2 { get; set; }
        public List<string> srnos { get; set; }

    }
}
