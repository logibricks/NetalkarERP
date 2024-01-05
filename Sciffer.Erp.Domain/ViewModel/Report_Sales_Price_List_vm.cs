using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_Sales_Price_List_vm
    {
        public string Customer_code { get; set; }
        public string Customer_description { get; set; }
        public string Item_Code { get; set; }
        public string Item_Description { get; set; }
        public string UOm { get; set; }
        public double Original_Price { get; set; }
        public double Discount_value { get; set; }
        public double AMT { get; set; }
        public double Price_After_doscount { get; set; }

    }
}
