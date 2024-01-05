using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_mfg_process
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int process_id { get; set; }
        public string process_description { get; set; }
        public string process_code { get; set; }
        public bool is_blocked { get; set; }
        public bool is_active { get; set; }
        public string process_name { get; set; }
    }
}
