using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_InventoryRateHistory_vm: Report_Inventory_ledger_Summary_vm
    {
        public string Description { get; set; }
        public double Qty { get; set; }
        public string Doc_Category { get; set; }
        public string Doc_Number { get; set; }
        public DateTime Posting_Date { get; set; }
        public double Rate { get; set; }
        public string Different_Value { get; set; }
        public string GL_Account { get; set; }

    }
}
