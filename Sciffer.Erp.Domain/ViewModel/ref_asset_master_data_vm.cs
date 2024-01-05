using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class ref_asset_master_data_vm
    {
        public int asset_master_data_id { get; set; }
        [Display(Name = "Asset Code*")]
        public string asset_master_data_code { get; set; }
        [Display(Name = "Select Machine Code")]
        public int machine_id { get; set; }
        [Display(Name = "Description*")]
        public string asset_master_data_desc { get; set; }
        [Display(Name = "Asset Class*")]
        public int asset_class_id { get; set; }
        [Display(Name = "Asset Group *")]
        public int asset_group_id { get; set; }
        [Display(Name = "Plant")]
        public int plant_id { get; set; }
        [Display(Name = "Purchase Order")]
        public string purchase_order { get; set; }
        [Display(Name = "Manufacturer")]
        public string manufacturer { get; set; }
        [Display(Name = "Manufacturer Part Number")]
        public string manufacturer_part_no { get; set; }
        [Display(Name = "Manufacturing Country")]
        public int manufacturing_country_id { get; set; }
        [Display(Name = "Priority")]
        public int priority_id { get; set; }
        [Display(Name = "Business Unit")]
        public int business_unit_id { get; set; }
        [Display(Name = "Asset Tag No")]
        public string asset_tag_no { get; set; }
        [Display(Name = "Purchasing Vendor")]
        public int purchasing_vendor_id { get; set; }
        [Display(Name = "Model Number")]
        public string model_no { get; set; }
        [Display(Name = "Manufacturer Serial Number")]
        public string manufacturing_serial_number { get; set; }
        [Display(Name = "Manufacturing Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime manufacturing_date { get; set; }
        [Display(Name = "Cost Center")]
        public int cost_center_id { get; set; }
        [Display(Name = "Capitalization Date ")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime capitalization_date { get; set; }
        [Display(Name = "Created By")]
        public int created_by { get; set; }
        [Display(Name = "Created Date*")]
        public DateTime created_ts { get; set; }
        [Display(Name = "Modified By*")]
        public int modified_by { get; set; }
        [Display(Name = "Modified Date*")]
        public DateTime modified_ts { get; set; }
        [Display(Name = "Blocked")]
        public bool is_active { get; set; }
        [Display(Name = "Status")]
        public int status_id { get; set; }
        [Display(Name = "HISTORICAL COST")]
        public decimal historical_cost { get; set; }
        [Display(Name = "ACCUMULATED DEPRECIATION")]
        public decimal accumulated_dep { get; set; }
        [Display(Name = "NET VALUE")]
        public decimal net_value { get; set; }
        [Display(Name = "HISTORICAL COST")]
        public decimal op_historical_cost { get; set; }
        [Display(Name = "ACCUMULATED DEPRECIATION")]
        public decimal op_accumulated_dep { get; set; }
        [Display(Name = "NET VALUE")]
        public decimal op_net_value { get; set; }
        [Display(Name = "Depreciation Area")]
        public int dep_area_id { get; set; }
        [Display(Name = "Depreciation Area")]
        public int dep_area_id1 { get; set; }
        [Display(Name = "Depreciation Area")]
        public int dep_area_id2 { get; set; }

        public string machine_name { get; set; }
        public string asset_class_name { get; set; }
        public string asset_group_name { get; set; }
        public string status_name { get; set; }

        public List<string> dep_area_code1 { get; set; }
        public List<string> dep_type_code1 { get; set; }
        public List<string> start_date1 { get; set; }
        public List<string> end_date1 { get; set; }
        public List<string> dep_type_frquency1 { get; set; }
        public List<string> useful_life_months1 { get; set; }
        public List<string> remaining1 { get; set; }

        public List<ref_asset_master_data_dep_parameter_vm> ref_asset_master_data_dep_parameter_vm { get; set; }
        public List<ref_asset_transaction_vm> ref_asset_transaction_vm { get; set; }
        public string machine_name_text { get; set; }
    }
}
