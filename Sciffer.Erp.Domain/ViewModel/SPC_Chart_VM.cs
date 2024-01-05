using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class SPC_Chart_VM
    {
        public int item_id { get; set; }
        public int process_id { get; set; }
        public string machine_id { get; set; }
        public string machine_name { get; set; }
        public TimeSpan start_time { get; set; }
        public TimeSpan end_time { get; set; }
        public int mfg_op_oc_id { get; set; }
        public string mfg_op_name { get; set; }
        public int mfg_op_qc_id { get; set; }
        public string mfg_qc_name { get; set; }
        public DateTime from_date { get; set; }
        public bool is_op_qc_parameter { get; set; }
        public string tag_no { get; set; }
        public int mfg_op_qc_parameter_id { get; set; }
        public string parameter_name { get; set; }
        public string parameter_value { get; set; }
        public double parameter_value_converted { get; set; }
        public string employee_name { get; set; }
        public string task_time1 { get; set; }
        public string row_id { get; set; }
        public string std_range_start { get; set; }
        public string std_range_end { get; set; }
        public int machine_task_op_qc_detail_id { get; set; }
    }

    public class spc_chart_calculation
    {
        public bool? is_parametervalue_string { get; set; }
        //public double max { get; set; }
        //public double min { get; set; }
        //public double avg { get; set; }
        //public double range { get; set; }
        //public string std_start_range { get; set; }
        //public string std_end_range { get; set; }
        //public double tolerance { get; set; }
        //public double sigma { get; set; }
        //public double cp { get; set; }
        //public double cpk { get; set; }
        //public string parameter_value { get; set; }

        public string text { get; set; }
        public string value { get; set; }
    }
}
