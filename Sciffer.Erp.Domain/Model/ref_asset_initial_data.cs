using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
   public class ref_asset_initial_data
    {
        [Key]
        public int asset_initial_data_id { get; set; }
        public int asset_initial_data_header_id { get; set; }
        public int asset_class_id { get; set; }
        public int dep_area_id { get; set; }
        public decimal historical_cost { get; set; }
        public decimal acc_depriciation { get; set; }
        public decimal net_value { get; set; }
        public int created_by { get; set; }
        public DateTime created_ts { get; set; }
        public bool is_active { get; set; }
        public int? modify_by { get; set; }
        public DateTime? modify_ts { get; set; }

        public bool? is_wdv_slm_or_block { get; set; }
    }
}
