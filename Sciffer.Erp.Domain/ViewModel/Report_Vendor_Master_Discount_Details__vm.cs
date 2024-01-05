using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_Vendor_Master_Discount_Details__vm
    {
        public string Vendor_Code { get; set; }
        public string Vendor_Decription { get; set; }
        public double Overall_Discount { get; set; }
        public string Item_Group { get; set; }
        public double DIscount_Rate { get; set; }

    }
}
