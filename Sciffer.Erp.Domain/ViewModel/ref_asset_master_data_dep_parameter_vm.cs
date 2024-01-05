using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
   public class ref_asset_master_data_dep_parameter_vm
    {
        public int? asset_master_dep_parameter_id { get; set; }
        public int? dep_area_id { get; set; }
        public string dep_start_date { get; set; }
        public string dep_end_date { get; set; }
        public int? dep_type_frquency_id { get; set; }
        public int? useful_life_months { get; set; }
        public int? remaining_life_months { get; set; }
        public bool is_active { get; set; }
        public string dep_area_code { get; set; }
        public int? asset_class_dep_id { get; set; }
        public int? asset_master_data_id { get; set; }
        public string dep_type_code { get; set; }
        public string dep_type_frquency { get; set; }
        public int? asset_class_id { get; set; }
    }
}
