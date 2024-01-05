using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_Item_Wise_Sales_Detail_vm: Report_Item_Accounting_Report_vm
    {
        public string Plant { get; set; }
        public string SLOC { get; set; }
        public double Qty { get; set; }
        public string Batch { get; set; }
        public string Customer_Code { get; set; }
        public string Customer_Description { get; set; }
        public string Sales_DOcument_Category { get; set; }
        public string Sales_DOcument_Number { get; set; }
        public DateTime Posting_Date { get; set; }
        public string Effective_Rate { get; set; }
        public double Sales_Value { get; set; }

    }
}
