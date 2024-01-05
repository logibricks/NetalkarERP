using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_TrialBalance_vm
    {
        public string gl_ledger_code { get; set; }
        public string gl_ledger_name { get; set; }
        public decimal? Opening_Bal { get; set; }
        public decimal? DR { get; set; }
        public decimal? CR { get; set; }
        public decimal? Closing_Bal { get; set; }
    }
}
