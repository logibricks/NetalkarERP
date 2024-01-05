using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class ref_tool_life_VM
    {
        public int tool_life_id { get; set; }
        public int tool_id { get; set; }
        public string tool_name { get; set; }
        public int tool_renew_type_id { get; set; }
        public string tool_renew_type_name { get; set; }
        public int machine_id { get; set; }
        public string machine_name { get; set; }
        public int ITEM_ID { get; set; }
        public string ITEM_NAME { get; set; }
        public double life_no_of_items { get; set; }
        public double per_unit_life_consumption { get; set; }
        
    }
}
