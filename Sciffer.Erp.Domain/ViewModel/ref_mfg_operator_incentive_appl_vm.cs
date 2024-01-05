using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class ref_mfg_operator_incentive_appl_vm
    {
        
        public int? mfg_operator_incentive_appl_id { get; set; }
        public int user_id { get; set; }
        public bool? is_incentive_applicable { get; set; }
        public string employee_name { get; set; }
        public string user_code { get; set; }
        public List<string> user_id_name { get; set; }
        public List<string> create_rights_name { get; set; }


    }
}
