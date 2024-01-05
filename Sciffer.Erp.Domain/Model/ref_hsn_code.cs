using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_hsn_code
    {
        [Key]
        public int hsn_code_id { get; set; }

        [Required]
        public string hsn_code { get; set; }

        [Required]
        public string hsn_code_description { get; set; }

        public bool is_blocked { get; set; }
        public int? within_state_tax_id { get; set; }
        public int? inter_state_tax_id { get; set; }

    }
    public class ref_hsn_code_vm
    {
        public int hsn_code_id { get; set; }
        public string hsn_code { get; set; }    
        public string hsn_code_description { get; set; }
        public int inter_state_tax_id { get; set; }
        public string inter_state_tax_name { get; set; }
        public bool is_blocked { get; set; }
        public int within_state_tax_id { get; set; }
        public string within_state_tax_name { get; set;  }
    }
}
