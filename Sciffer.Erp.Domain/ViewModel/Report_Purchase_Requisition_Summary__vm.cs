using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_Purchase_Requisition_Summary__vm: Report_Purchase_Requisition_Detailed__vm
    {
        public string PR_Doc_Category { get; set; }
        public string PR_Doc_Number { get; set; }
        public DateTime PR_Doc_Date { get; set; }
        public string Status { get; set; }
        public string PO_Doc_Category { get; set; }
        public string PODoc_Number { get; set; }
        public DateTime PO_Doc_Date { get; set; }
        public double Ordered_Qty { get; set; }

    }
}
