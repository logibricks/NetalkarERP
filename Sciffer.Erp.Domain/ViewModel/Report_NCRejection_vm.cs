using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_NCRejection_vm
    {
        public DateTime date { get; set; }
        public string tag_no { get; set; }
        public string nc_details { get; set; }
        public string machine_name { get; set; }
        public string employee_name { get; set; }
        public string root_cause_details { get; set; }
        public string action_plan { get; set; }
        public string nc_status_desc { get; set; }
        public string remarks { get; set; }
        public string ITEM_NAME { get; set; }

        public string heat_code { get; set; }
        public string run_code { get; set; }

        public string why1 { get; set; }
        public string why2 { get; set; }
        public string why3 { get; set; }
        public string why4 { get; set; }
        public string why5 { get; set; }
        public string machine_operator_name { get; set; }
        public string nc_tag_number { get; set; }

    }
}
