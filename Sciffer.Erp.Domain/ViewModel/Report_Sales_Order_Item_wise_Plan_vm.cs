using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_Sales_Order_Item_wise_Plan_vm: Report_Sales_Order_Qty_Tracker_vm
    {
        public DateTime Delivery_Date { get; set; }
        public double Invoiced_Qty { get; set; }
        public double Balance_Qty { get; set; }
        public string Current_Stock { get; set; }
        public string Customer_Code { get; set; }
        public string Customer_Description { get; set; }
        public string So_Number { get; set; }
        public string So_Line_Item_Number { get; set; }
        public DateTime So_Date { get; set; }

    }
}
