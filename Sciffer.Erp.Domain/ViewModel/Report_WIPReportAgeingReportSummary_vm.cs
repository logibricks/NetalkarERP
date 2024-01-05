using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_WIPReportAgeingReportSummary_vm
    {
        public string Machine_Code { get; set; }
        public string Machine_Description { get; set; }
        public string Item_Code { get; set; }
        public string Item_Description { get; set; }
        public string Production_Order { get; set; }
        public string Tag_Number { get; set; }
        public string Interval_1 { get; set; }
        public string Interval_2 { get; set; }
        public string Interval_3 { get; set; }
        public string Interval_4 { get; set; }
    }
}
