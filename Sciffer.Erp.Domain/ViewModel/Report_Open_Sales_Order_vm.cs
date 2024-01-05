using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_Open_Sales_Order_vm
    {
        public string Customer_Code { get; set; }
        public string Customer_Description { get; set; }
        public string DOc_Number { get; set; }
        public string Item_Code { get; set; }
        public double Total_Basic_Value { get; set; }
        public double Invoiced_Basic_Value { get; set; }
        public double Open_Value { get; set; }
        public double Total_Qty { get; set; }
        public double Invoice_Qty { get; set; }
        public double Open_QTy { get; set; }

    }
}
