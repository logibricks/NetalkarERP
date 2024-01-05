using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_Inventory_Report_CascadeReport_vm
    {
        public string item_code { get; set; }
        public string ITEM_NAME { get; set; }
        public string PLANT_CODE { get; set; }
        public string PLANT_NAME { get; set; }
        public string storage_location_name { get; set; }
        public string bucket_name { get; set; }
        public string batch_number { get; set; }
        public double? Qty { get; set; }
        public string UOM_NAME { get; set; }
    }
}
