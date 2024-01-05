using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_GeneralLedger_vm
    {
        public string Document_Type { get; set; }
        public string Document_category { get; set; }
        public string Document_Number { get; set; }
        public DateTime Posting_Date { get; set; }
        public DateTime Document_Date { get; set; }
        public DateTime Due_Date { get; set; }
        public string Ref1 { get; set; }
        public string Ref2 { get; set; }
        public string Ref3 { get; set; }
        public string Doc_Type { get; set; }
        public string Doc_Number { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public double DR_LC { get; set; }
        public double Cr_LC { get; set; }
        public string Currency_Indicator { get; set; }
        public double Dr_FC { get; set; }
        public double Dr_FC1 { get; set; }
        public double Closing_Bal { get; set; }
        public string Cost_Center { get; set; }
        public string Line_Remarks { get; set; }
    }
}
