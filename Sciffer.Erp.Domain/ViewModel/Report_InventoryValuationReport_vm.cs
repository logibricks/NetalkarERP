using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_InventoryValuationReport_vm:Report_InventoryCostingReport_vm
    {
        public string control_account_name { get; set; }
        public string control_account { get; set; }
        public double? value { get; set; }
        public double? qty { get; set; }
    }
}
