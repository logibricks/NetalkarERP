using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_Purchase_Requisition_Detailed__vm: Report_Purchase_Invoice_Item_Level_vm
    {
        public string DOcument_Number { get; set; }
        public DateTime Posting_Date { get; set; }
        public string Business_Unit { get; set; }
        public string Header_Delivery_Date { get; set; }
        public string Source { get; set; }
        public string Item_COde { get; set; }
        public string Info_Price { get; set; }
        public string Preferred_Vendor { get; set; }

    }
}
