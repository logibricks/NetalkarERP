using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_Sales_Quotation_Summary_VM: Report_Purchase_Return_Summary__vm
    {
        public string Customer_Code { get; set; }
        public string Customer_Description { get; set; }
    }
}
