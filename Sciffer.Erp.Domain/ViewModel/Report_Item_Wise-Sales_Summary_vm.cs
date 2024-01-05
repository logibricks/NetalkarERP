using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_Item_Wise_Sales_Summary_vm: Report_Item_Accounting_Report_vm
    {
        public string Plant { get; set; }
        public double Qty { get; set; }
        public string Sales_Value { get; set; }
        public double Avg_Rate { get; set; }

    }
}
