using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class VM_ref_item_wip_valuation
    {
        
        public int item_wip_valuation_id { get; set; }

        public int machine_id { get; set; }
        public string machine_name { get; set; }

        public int ITEM_ID { get; set; }
        public string item_name { get; set; }

        public double value { get; set; }

        public bool is_active { get; set; }
        public int created_by { get; set; }
        public int modified_by { get; set; }
        public DateTime created_on { get; set; }
        public DateTime modified_on { get; set; }
    }
}
