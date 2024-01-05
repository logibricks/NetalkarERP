using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class quality_parameter_vm
    {
        public int mfg_qc_id { get; set; }
        public int item_code { get; set; }
        public int machine_code { get; set; }
        public int frequency { get; set; }

        public List<String> para_id { get; set; }
        public List<String> para_name { get; set; }
        public List<String> para_uom { get; set; }
        public List<String> para_from { get; set; }
        public List<String> para_to { get; set; }

        public List<mfg_qc_vm> mfg_qc_vm { get; set; }
    }

    public class mfg_qc_vm
    {
        public int mfg_qc_id { get; set; }
        public int item_id { get; set; }
        public string item_code { get; set; }
        public int machine_id { get; set; }
        public string machine_code { get; set; }
        public int frequency { get; set; }

        public int mfg_qc_parameter_id { get; set; }
        public string parameter_name { get; set; }
        public string parameter_uom { get; set; }
        public string std_range_start { get; set; }
        public string std_range_end { get; set; }
        public bool is_numeric { get; set; }
    }
}
