using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_ITEM_MASTERQCPARAMETERS_vm: Report_Inventory_ledger_Summary_vm
    {
        public string Item_Type { get; set; }
        public string Item_Category { get; set; }
        public string Item_group { get; set; }
        public string Paramater_name { get; set; }
        public string Paramater_range { get; set; }

    }
}
