using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class ref_tool_machine_usage_VM
    {
        public int tool_machine_usage_id { get; set; }
        [Display(Name = "Select Tool")]
        public int tool_id { get; set; }
        public string tool_name { get; set; }
        [Display(Name = "Select Machine")]
        public int machine_id { get; set; }
        public string machine_name { get; set; }
        [Display(Name = "Tool Renew Type")]
        public int tool_renew_type_id { get; set; }
        public string tool_renew_type_name { get; set; }
        [Display(Name = "Current Life")]
        public double current_life_percentage { get; set; }
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime start_date_time { get; set; }
        [Display(Name = "In Use")]
        public bool in_use { get; set; }

        public List<string> item_id { get; set; }
        public List<string> life_no_of_items { get; set; }
        public List<string> per_unit_life_consumption { get; set; }
        public List<string> no_of_items_processed { get; set; }
        public List<string> item_life_consumption { get; set; }
        public List<string> item_life_consumption_percentage { get; set; }


        public virtual IList<ref_tool_machine_item_usage> ref_tool_machine_item_usage { get; set; }
    }

    public class ref_tool_machine_item_usage_VM
    {
        public int tool_machine_item_usage_id { get; set; }
        public int tool_machine_usage_id { get; set; }

        public int item_id { get; set; }
        public string item_name { get; set; }

        public double life_no_of_items { get; set; }
        public double per_unit_life_consumption { get; set; }

        public double no_of_items_processed { get; set; }
        public double item_life_consumption { get; set; }
        public double item_life_consumption_percentage { get; set; }
    }
}
