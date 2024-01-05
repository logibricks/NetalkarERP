using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class mfg_machine_item_upgradation
    {
        [Key]
        public int mfg_upgradation_id { get; set; }
        public int process_id { get; set; }
        public int item_id { get; set; }
        public int machine_id { get; set; }
        public string file_name { get; set; }
        public string file_path { get; set; }
        public bool? is_active { get; set; }
    }
}
