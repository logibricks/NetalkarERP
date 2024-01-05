using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
   public class ref_asset_master_data_dep_parameter
    {
        [Key]
        public int asset_master_dep_parameter_id { get; set; }
        public int? asset_master_data_id { get; set; }
        public int? asset_class_dep_id { get; set; }      
        public DateTime? dep_start_date { get; set; }
        public DateTime? dep_end_date { get; set; }       
        public bool is_active { get; set; }
    }
}
