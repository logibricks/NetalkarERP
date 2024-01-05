using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_Customer_Master_Discount_Details_vm
    {
        public string Customer_Code { get; set; }
        public string Customer_Decription { get; set; }
        public double Overall_Discount { get; set; }
        public string Item_Group { get; set; }
        public double DIscount_Rate { get; set; }

    }
}
