using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_InventoryCostingReport_vm
    {
        public string item_code { get; set; }
        public string ITEM_NAME { get; set; }
        public string PLANT_CODE { get; set; }
        public string PLANT_NAME { get; set; }
        public string UOM_NAME { get; set; }
        public string costing_method { get; set; }
        public double? rate { get; set; }

    }
}
