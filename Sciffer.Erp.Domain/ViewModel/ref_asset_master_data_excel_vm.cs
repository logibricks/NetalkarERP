using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
   public class ref_asset_master_data_excel_vm
    {
        public int row_no { get; set; }
        public bool is_based_on_machine_code { get; set; }   //Flag to Check with or Without
        public string asset_code { get; set; }
        public string machine_code { get; set; }
        public string asset_class { get; set; }
        public string asset_group { get; set; }
        public string status { get; set; }
        public string cost_center { get; set; }
        public string capitalisation_date  { get; set; }
        
        //Extra

        public string asset_master_data_id { get; set; }
        public int machine_id { get; set; }
        public int asset_class_id { get; set; }
        public int asset_group_id { get; set; }
        public int status_id { get; set; }
        public int cost_center_id { get; set; }


        //Code
        
        public string description { get; set; }
        public string plant { get; set; }
        public string purchase_order { get; set; }
        public string manufacturer { get; set; }
        public string manufacturer_part_number { get; set; }
        public string manufacturing_country { get; set; }
        public string priority { get; set; }
        public string business_unit { get; set; }
        public string asset_tag_no { get; set; }
        public string purchasing_vendor_code { get; set; }
        public string model_number { get; set; }
        public string manufacturer_serial_number { get; set; }
        public string manufacturing_date { get; set; }
    }
}
