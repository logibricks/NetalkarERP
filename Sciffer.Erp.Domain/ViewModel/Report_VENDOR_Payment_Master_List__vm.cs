using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_VENDOR_Payment_Master_List__vm
    {
        public string VENDOR_code { get; set; }
        public string VENDOR_name { get; set; }
        public string Payment_Terms_Code { get; set; }
        public string Payment_Terms_Description { get; set; }
        public string Due_Date_Based_On { get; set; }
        public string Payment_Cycle_Type { get; set; }
        public string Payment_CYcle { get; set; }
        public string Bank_COde { get; set; }
        public string Bank_Name { get; set; }
        public string Branch { get; set; }
        public string Account_Type { get; set; }
        public string IFSC_COde { get; set; }
        public string Account_Number { get; set; }

    }
}
