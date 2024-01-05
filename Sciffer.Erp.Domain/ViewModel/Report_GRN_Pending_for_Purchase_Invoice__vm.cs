using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_GRN_Pending_for_Purchase_Invoice__vm
    {
        public string GRN_Category { get; set; }
        public string GRN_DOc_Number { get; set; }
        public DateTime GRN_Date { get; set; }
        public string Plant { get; set; }
        public string Ageing_Days { get; set; }
        public string Vendor_Description { get; set; }
        public string Vendor_Description1 { get; set; }
        public double GRN_Value { get; set; }
        public string IEX_Category { get; set; }
        public string IEX_Number { get; set; }
        public DateTime IEX_Date { get; set; }

    }
}
