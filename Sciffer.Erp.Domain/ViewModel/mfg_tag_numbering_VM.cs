using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class mfg_tag_numbering_VM
    {
        public int tag_numbering_id { get; set; }
        public string from_number { get; set; }
        public string to_number { get; set; }
        public int? current_number { get; set; }
        public int year { get; set; }
        public int month { get; set; }
        public string prefix { get; set; }
        public bool is_active { get; set; }
        public bool is_blocked { get; set; }
        public int machine_id { get; set; }
        public string machine_code { get; set; }
        public int ITEM_ID { get; set; }
        public string ITEM_NAME { get; set; }
    }
}
