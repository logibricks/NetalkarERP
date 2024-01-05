using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_parameter_list
    {
        [Key]
        public int parameter_id { get; set; }
        public string parameter_code { get; set; }
        public string parameter_desc { get; set; }
        public string parameter_range { get; set; }
        public bool is_active { get; set; }
        public bool is_blocked { get; set; }
    }
    public class ref_parameter_list_VM
    {
        public int parameter_id { get; set; }
        public string parameter_code { get; set; }
        public string parameter_desc { get; set; }
        public string parameter_range { get; set; }
        public bool is_active { get; set; }
        public bool is_blocked { get; set; }
    }
}
