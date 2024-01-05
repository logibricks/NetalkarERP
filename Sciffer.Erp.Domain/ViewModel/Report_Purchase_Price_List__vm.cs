using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_Purchase_Price_List__vm: Report_Purchase_Invoice_Item_Level_vm
    {
        public string Vendor_code { get; set; }
        public string Vendor_description { get; set; }
        public string UOm { get; set; }
        public string Original_Price { get; set; }
        public double Discount_value { get; set; }
        public string AMt {get;set;}
        public double Price_After_doscount { get; set; }

}
}
