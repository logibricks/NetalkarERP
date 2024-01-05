using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_GRN_Header__vm: Report_GRN_Item_Level_vm
    {
        public string PO_Number { get; set; }
        public double Nett_Value { get; set; }
        public string Business_Unit { get; set; }
        public string Vendor_Document_No { get; set; }
        public DateTime Vendior_Quotation_Date { get; set; }
        public string Gate_Entry_No { get; set; }
        public string Gate_Entry_Date { get; set; }

    }
}
