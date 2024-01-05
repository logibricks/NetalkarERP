using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
   public class ref_asset_master_data
    {
        [Key]
        public int asset_master_data_id { get; set; }
        public string asset_master_data_code { get; set; }
        public int? machine_id { get; set; }
        public string asset_master_data_desc { get; set; }
        public int asset_class_id { get; set; }
        public int asset_group_id { get; set; }
        public int? plant_id { get; set; }
        public string purchase_order { get; set; }
        public string manufacturer { get; set; }
        public string manufacturer_part_no { get; set; }
        public int? manufacturing_country_id { get; set; }
        public int? priority_id { get; set; }
        public int? business_unit_id { get; set; }
        public string asset_tag_no { get; set; }
        public int? purchasing_vendor_id { get; set; }
        public string model_no { get; set; }
        public string manufacturing_serial_number { get; set; }
        public DateTime? manufacturing_date { get; set; }
        public int? cost_center_id { get; set; }
        public DateTime? capitalization_date { get; set; }
        public int? created_by { get; set; }
        public DateTime? created_ts { get; set; }
        public int? modified_by { get; set; }
        public DateTime? modified_ts { get; set; }
        public bool is_active { get; set; }
        public int? status_id { get; set; }
       
        public virtual ICollection<ref_asset_master_data_dep_parameter> ref_asset_master_data_dep_parameter { get; set; }

    }
}
