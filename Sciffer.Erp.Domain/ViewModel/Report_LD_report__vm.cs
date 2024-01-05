using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_LD_report__vm
    {
        public string GRN_Doc_Category { get; set; }
        public string Grn_Doc_Number { get; set; }
        public string Vendor_Code { get; set; }
        public string Vendor_Description { get; set; }
        public string Plant { get; set; }
        public string Item_Code { get; set; }
        public string Item_Description { get; set; }
        public DateTime PO_Delivery_Date { get; set; }
        public DateTime Gate_Entry_Date { get; set; }
        public string Delay { get; set; }
        public DateTime GRN_Doc_Date { get; set; }
        public string Delay1 { get; set; }

    }
}
