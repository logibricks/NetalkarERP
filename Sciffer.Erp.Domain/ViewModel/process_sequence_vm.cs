using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class process_sequence_vm
    {
        public int process_sequence_id { get; set; }
        public string process_sequence_name { get; set; }
        public int ITEM_ID { get; set; }
        public string ITEM_CODE { get; set; }
        public bool is_blocked { get; set; }

        public List<String> process_id { get; set; }
        public List<String> process_name { get; set; }
        public List<String> machine_id { get; set; }
        public List<String> machine_name { get; set; }
        public List<String> item_cost { get; set; }

        public List<process_sequence_detail_vm> process_sequence_detail_vm { get; set; }
    }

    public class process_sequence_detail_vm
    {
        public int map_process_sequence_id { get; set; }
        public int process_sequence_id { get; set; }
        public int machine_id { get; set; }
        public string machine_code { get; set; }
        public int process_id { get; set; }
        public string process_code { get; set; }
        public Double item_cost { get; set; }
    }
}
