using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_Client_Ledger_VM
    {
        public DateTime Posting_Date { get; set; }
        public DateTime Document_Date { get; set; }
        public DateTime Due_Date { get; set; }
        public string DOcument_Category { get; set; }
        public string Document_Number { get; set; }
        public string JE_Number { get; set; }
        public string Header_Remarks { get; set; }
        public string Ref1 { get; set; }
        public string Ref2 { get; set; }
        public string Ref3 { get; set; }
        public string Currency { get; set; }
        public string Dr_Doc_Currency { get; set; }
        public string Cr_Doc_Currency { get; set; }
        public string Dr_Loc_Currency { get; set; }
        public string Cr_Loc_Currency { get; set; }
        public double Cumulative_Balance { get; set; }

    }
}
