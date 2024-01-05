using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_Userwiseproduction_vm
    {
        public string User_Code { get; set; }
        public string User_Name { get; set; }
        public string Machine_Code { get; set; }
        public string Machine_Description { get; set; }
        public string Item_Code { get; set; }
        public string Item_Description { get; set; }
        public string Production_Order { get; set; }
        public double OK_Qty { get; set; }
        public double Rejected_Qty { get; set; }
        public double Total_Qty { get; set; }
    }
}
