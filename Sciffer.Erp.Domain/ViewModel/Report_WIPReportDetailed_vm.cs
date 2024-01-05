using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_WIPReportDetailed_vm: Report_WIPReportAgeingReportSummary_vm
    {
        public double Qty { get; set; }
        public double Cost { get; set; }
        public double Value { get; set; }

    }
}
