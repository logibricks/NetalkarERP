using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
   public class operator_change_request_detail_vm
    {
        public int operator_change_request_id { get; set; }
        public int operator_change_request_detail_id { get; set; }
        public int current_level_id { get; set; }
        public int machine_id { get; set; }
        public int operator_id { get; set; }
        public int new_level_id { get; set; }
        public string current_level_code { get; set; }
        public string machine_name { get; set; }
        public string operator_name { get; set; }
        public string new_level_code { get; set; }
    }
}
