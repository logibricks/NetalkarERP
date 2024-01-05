using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class ref_tool_operation_map_vm
    {
        public int? tool_operation_map_id { get; set; }

        [Display(Name = "Select Operation")]
        public int? process_id { get; set; }
        public string process_code { get; set; }
        public string process_description { get; set; }

        [Display(Name = "Select Crankshaft")]
        public int? crankshaft_id { get; set; }
        public string ITEM_CODE_CRANKSHAFT { get; set; }
        public string ITEM_NAME_CRANKSHAFT { get; set; }

        [Display(Name = "Select Tool")]
        public int? tool_id { get; set; }
        public string ITEM_CODE { get; set; }
       // public string ITEM_NAME { get; set; }

        [Display(Name = "Select Usage type")]
        public int? tool_usage_type_id { get; set; }
        public string tool_usage_type_name { get; set; }

        [Display(Name = "Select Category")]
        public int? tool_category_id { get; set; }
        public string tool_category_name { get; set; }
        //[Display(Name = "Is Block")]
        public bool? is_blocked { get; set; }
        
        public string process_name { get; set; }
        public int item_id { get; set; }

        public string item_name { get; set; }
        public string tool_name { get; set; }
    }
}
