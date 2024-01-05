using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_Customer_Payment_Master_List_vm
    {
        public string customer_code { get; set; }
        public string customer_name { get; set; }
        public string Payment_Terms_Code { get; set; }
        public string Payment_Terms_Description { get; set; }
        public string Due_Date_Based_On { get; set; }
        public string Payment_Cycle_Type { get; set; }
        public string Payment_CYcle { get; set; }

    }
}
