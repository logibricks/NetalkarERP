using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class ref_pm_notification_vm
    {
        [Key]
        public int notification_id { get; set; }
        public int emp_id { get; set; }     
        public int category_id { get; set; }
        public string category_name { get; set; }      
        public string doc_number { get; set; }
        public string notification_type { get; set; }     
        public DateTime notification_date { get; set; }      
        public string notification_description { get; set; }      
        public int plant_id { get; set; }
        public string plant_name { get; set; }       
        public string employee_name { get; set; }
        public string machine_code { get; set; }
        public int machine_id { get; set; }
        public int employee_id { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public TimeSpan start_time { get; set; }
        public TimeSpan end_time { get; set; }
        public string detail_problem { get; set; }
        public string problem_attachment { get; set; }
        public string detail_solution { get; set; }
        public string attended_by { get; set; }
        public string reviewed_by { get; set; }
        public bool is_breakdown { get; set; }
        public DateTime breakdown_start_date { get; set; }
        public DateTime breakdown_end_date { get; set; }
        public TimeSpan breakdown_start_time { get; set; }
        public TimeSpan breakdown_end_time { get; set; }
        public int created_by { get; set; }
        public DateTime created_ts { get; set; }
        public string module_form_name { get; set; }
        public string plant_code { get; set; }
        public string category { get; set; }
        public bool open_close { get; set; }
        public int closed_by { get; set; }
        public DateTime closed_ts { get; set; }
        //public int notification_id { get; set; }
        public virtual IList<ref_plan_breakdown_order> ref_plan_breakdown_order_VM { get; set; }
        public string order_executedby { get; set; }
    }
}
