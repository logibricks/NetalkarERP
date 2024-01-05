using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_Purchase_Invoice_Summary__vm: Report_Purchase_Invoice_Item_Level_vm
    {
        public string vendor_Code { get; set; }
        public string Vendor_Description { get; set; }
        public string Parent_code { get; set; }
        public string parent_description { get; set; }
        public double total_value { get; set; }
        public double gross_value { get; set; }
        public string TDS { get; set; }
        public double Nett_Value { get; set; }

    }
}
