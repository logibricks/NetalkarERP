using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_InventoryledgerDetailed_vm: Report_Inventory_ledger_Summary_vm
    {
        public string Source_Doc { get; set; }
        public string Doc_Category { get; set; }
        public string Doc_Number { get; set; }
        public DateTime Posting_date { get; set; }
        public DateTime Document_Date { get; set; }
        public DateTime System_Date { get; set; }
        public string Batch { get; set; }
        public string Reason_Code { get; set; }
        public double Qty { get; set; }
        public double Balance { get; set; }

    }
}
