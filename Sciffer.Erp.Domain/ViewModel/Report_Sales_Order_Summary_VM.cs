using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_Sales_Order_Summary_VM
    {
        public string Customer_Code { get; set; }
        public string Customer_Description { get; set; }
        public string Parent_code { get; set; }
        public string parent_description { get; set; }
        public double total_value { get; set; }
        public double total_taxes { get; set; }
        public double gross_value { get; set; }

    }
}
