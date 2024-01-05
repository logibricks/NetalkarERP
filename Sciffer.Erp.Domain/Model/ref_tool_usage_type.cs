using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public  class ref_tool_usage_type
    {
        [Key]
        public int tool_usage_type_id { get; set; }
        public string tool_usage_type_code { get; set; }
        public string tool_usage_type_name { get; set; }
    }
}
