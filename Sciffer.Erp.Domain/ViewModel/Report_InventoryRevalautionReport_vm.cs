using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_InventoryRevalautionReport_vm
    {
        public string Doc_Category { get; set; }
        public string Doc_Number { get; set; }
        public DateTime Posting_Date { get; set; }
        public DateTime Document_Date { get; set; }
        public string Plant { get; set; }
        public string Item_code { get; set; }
        public string Description { get; set; }
        public string UOM { get; set; }
        public double Qty { get; set; }
        public double Old_Rate { get; set; }
        public double New_Rate { get; set; }
        public string Different_Rate { get; set; }
        public string Different_Value { get; set; }
        public string GL_Account { get; set; }

    }
}
