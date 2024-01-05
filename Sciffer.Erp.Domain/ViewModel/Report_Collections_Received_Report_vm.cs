using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_Collections_Received_Report_vm: Report_Collections_Expected_Report__vm
    {
        public double Total_Receivable { get; set; }
        public string Received_in_Past { get; set; }
        public string Received_in_selected_period { get; set; }
        public string Adjustement_Selected_Period { get; set; }
        public string Future_receivable { get; set; }

    }
}
