using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_ARAGeingReport_vm
    {
        public string Customer_Code { get; set; }
        public string Customer_Description { get; set; }
        public DateTime Posting_Date { get; set; }
        public DateTime Due_Date { get; set; }
        public DateTime DOcument_Date { get; set; }
        public string Document_Category { get; set; }
        public string Doc_Number { get; set; }
        public double Total_value { get; set; }
        public string Not_Due { get; set; }
        public string Interval_1 { get; set; }
        public string Interval_2 { get; set; }
        public string Interval_3 { get; set; }
        public string Interval_4 { get; set; }
        public string Interval_5 { get; set; }
        public string Interval_6 { get; set; }
    }
}
