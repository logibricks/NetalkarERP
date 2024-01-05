using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public  class ref_tool_category
    {
        [Key]
        public int tool_category_id { get; set; }
        public string tool_category_code { get; set; }
        public string tool_category_name { get; set; }
    }
}
