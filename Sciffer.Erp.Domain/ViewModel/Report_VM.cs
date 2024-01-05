using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_VM
    {

    }
    public class report_sales_summary
    {
        public string customer_code { get; set; }
        public string customer_name { get; set; }
        public string parent_code { get; set; }
        public string parent_description { get; set; }
        public string customer_category { get; set; }
        public string sales_rm { get; set; }
        public string priority_name { get; set; }
        public string gl_ledger_name { get; set; }
        public double? total_value { get; set; }
        public double? total_taxes { get; set; }
        public double? gross_value { get; set; }
        public double? tds_value { get; set; }
        public double? net_value { get; set; }
    }
}
