using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class ref_plan_breakdown_order_VM
    {
        public int plan_breakdown_order_id { get; set; }
        public int category_id { get; set; }
        public string doc_number { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime creation_date { get; set; }
        public int maintenance_type_id { get; set; }
        public int plant_id { get; set; }
        public int? employee_id { get; set; }
        public string machine_id { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? actual_start_date { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? actual_finish_date { get; set; }
        [Display(Name = "Notification Description")]
        [DataType(DataType.MultilineText)]
        public string notification_description { get; set; }

        public int? order_executed_by { get; set; }
        public int? order_approved_by { get; set; }
        public int? permit_id { get; set; }
        public int? notification_id { get; set; }
        public string remarks { get; set; }
        public string attachment { get; set; }
        public bool is_active { get; set; }
        public int? created_by { get; set; }
        public DateTime created_ts { get; set; }

        public string deleteids { get; set; }
        public string deleteids1 { get; set; }

        public int? rm_item_id { get; set; }
        public int? rm_item_id1 { get; set; }


        public string item_name { get; set; }
        public string category_name { get; set; }
        public string maintenance_type { get; set; }
        public string plant_name { get; set; }
        public string machine_name { get; set; }
        public string permit_no { get; set; }
        public string notification_no { get; set; }
        public string order_executed_by_name { get; set; }
        public string order_approved_by_name { get; set; }

        public string machine_id_selected { get; set; }
        public TimeSpan actual_start_time { get; set; }
        public TimeSpan? actual_end_time { get; set; }

        public virtual IList<ref_plan_breakdown_parameter> ref_plan_breakdown_parameter { get; set; }
        public virtual IList<ref_plan_breakdown_cost> ref_plan_breakdown_cost { get; set; }
        public virtual IList<ref_plan_breakdown_service_cost> ref_plan_breakdown_service_cost { get; set; }
        public virtual IList<map_machine_breakdown_order> map_machine_breakdown_order { get; set; }

        public List<string> plan_breakdown_parameter_id { get; set; }
        public List<string> parameter_sr_no { get; set; }
        public List<string> parameter_id { get; set; }
        public List<string> parameter_range { get; set; }
        public List<string> actual_result { get; set; }
        public List<string> method_used { get; set; }
        public List<string> shelf_check { get; set; }
        public List<string> document_reference { get; set; }

        public List<string> plan_breakdown_cost_id { get; set; }
        public List<string> cost_sr_no { get; set; }
        public List<string> item_id { get; set; }
        public List<string> uom_id { get; set; }
        public List<string> cost_parameter_id { get; set; }
        public List<double> required_qty { get; set; }
        public List<string> actual_qty { get; set; }
        public List<string> sloc_id { get; set; }
        public List<string> bucket_id { get; set; }
        public List<string> goods_issue_id { get; set; }
        public List<string> issue_doc_number { get; set; }
        public List<string> posting_date { get; set; }

        //added 06-12
        public string order_executedby { get; set; }

        public int? under_taken_by_id { get; set; }
        public string attended_by { get; set; }
        public int user_id { get; set; }
        public string user_name { get; set; }
    }
}
