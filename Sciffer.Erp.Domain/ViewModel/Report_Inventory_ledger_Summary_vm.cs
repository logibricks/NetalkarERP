using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_Inventory_ledger_Summary_vm
    {
        public string Item_Code { get; set; }
        public string Item_Description { get; set; }
        public string Plant { get; set; }
        public string Sloc { get; set; }
        public string Bucket { get; set; }
        public string batch { get; set; }
        public string Opening { get; set; }
        public string Receipts { get; set; }
        public string Issue { get; set; }
        public string Closing { get; set; }

    }
}
