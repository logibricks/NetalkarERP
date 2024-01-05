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
    public class ref_plan_maintenance_VM
    {
        [Key]
        public int plan_maintenance_id { get; set; }

        //public string machine_name { get; set; }
        public string doc_number { get; set; }
        public int category_id { get; set; }

        [Display(Name = "Posting Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime posting_date { get; set; }
        public string machine_name { get; set; }
        public int machine_id { get; set; }
        //[ForeignKey("machine_id")]
        //public virtual ref_machine ref_machine { get; set; }

        public string machine_category_name { get; set; }
        public int machine_category_id { get; set; }
        //[ForeignKey("machine_category_id")]
        //public virtual ref_machine_category ref_machine_category { get; set; }

        public string maintenance_type_name { get; set; }
        public int maintenance_type_id { get; set; }
        public bool is_blocked { get; set; }
        //[ForeignKey("maintenance_type_id")]
        //public virtual ref_maintenance_type ref_maintenance_type { get; set; }

        [Display(Name = "Plan Start Date")]
        [Required(ErrorMessage = "Plan Start Date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime plan_start_date { get; set; }
        public string plan_start_dates { get; set; }

        [Display(Name = "Plan End Date")]
        [Required(ErrorMessage = "Plan End Date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime plan_end_date { get; set; }
        public string plan_end_dates { get; set; }

        [Display(Name = "Start Date")]
        [Required(ErrorMessage = "Cycle Start Date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime cycle_start_date { get; set; }
        public string cycle_start_dates { get; set; }
        public string category_name { get; set; }
        public int frequency { get; set; }

        public int frequency_type { get; set; }

        public bool create_order_days { get; set; }
        public string create_order_dtype { get; set; }
        public int days_before { get; set; }

        public int? create_order_yes { get; set; }
        public int maintenance_period { get; set; }
        public string maintenance_period_type { get; set; }
        public int allowed_delay { get; set; }
        public string allowed_delay_type { get; set; }
        public int allowed_early_completion { get; set; }
        public string allowed_early_comlpletion_type { get; set; }

        public string counter_frequency { get; set; }
        public string remarks { get; set; }
        public string attachement { get; set; }

        public string plant_name { get; set; }
        public int plant_id { get; set; }
        [ForeignKey("plant_id")]
        public virtual REF_PLANT REF_PLANT { get; set; }
        public string manufacturer { get; set; }
        public string model_no { get; set; }
        public string manufacturer_part_no { get; set; }
        public string manufacturing_serial_number { get; set; }
        public string asset_code_id { get; set; }
        public string asset_tag_no { get; set; }
        public int? employee_id { get; set; }
        public string employee_name { get; set; }
        public bool is_active { get; set; }
        public string deleteids { get; set; }
        public string deleteids1 { get; set; }

        public virtual List<ref_plan_maintenance_detail> ref_plan_maintenance_detail { get; set; }
        public virtual List<ref_plan_maintenance_details> ref_plan_maintenance_details { get; set; }
        public virtual List<ref_plan_maintenance_component> ref_plan_maintenance_component { get; set; }
        public virtual List<ref_plan_maintenance_components> ref_plan_maintenance_components { get; set; }
        public virtual List<ref_plan_maintenance_schedule> ref_plan_maintenance_schedule { get; set; }
        public virtual List<ref_plan_maintenance_schedules> ref_plan_maintenance_schedules { get; set; }

        public List<string> maintenance_parameter_id { get; set; }
        public List<string> parameter_sr_no { get; set; }
        public List<string> parameter_code_id { get; set; }
        public List<string> range { get; set; }

        public List<string> component_detail_id { get; set; }
        public List<string> component_sr_no { get; set; }
        public List<string> item_id { get; set; }
        public List<string> uom_id1 { get; set; }
        public List<string> component_parameter { get; set; }
        public List<Double> quantity { get; set; }
    }
    public class report_plant_maintenanace
    {
        public string doc_number { get; set; }
        public DateTime? posting_date { get; set; }
        public DateTime? notification_date { get; set; }
        public string maintenance_name { get; set; }
        public string machine_code { get; set; }
        public string machine_name { get; set; }
        public string Machine { get; set; }
        public DateTime? plan_start_date { get; set; }
        public DateTime? plan_end_date { get; set; }
        public DateTime? cycle_start_date { get; set; }
        public int? frequency { get; set; }
        public int? maintenance_period { get; set; }
        //public int? counter_frequency { get; set; }
        public string employee_name { get; set; }
        public int? allowed_early_completion { get; set; }
        public int? allowed_delay { get; set; }
        //public int? create_order_days { get; set; }
        //public int? create_order_yes { get; set; }
        public string Parameter { get; set; }
        public string parameter_range { get; set; }
        public string order_no { get; set; }
        //public string creation_date { get; set; }
        public string Plant { get; set; }
        public string Maintenance_Plan_Maintenance_Number { get; set; }
        public string Notification { get; set; }
        public string notification_description { get; set; }
        public string Notified_By { get; set; }
        public DateTime? start_date { get; set; }
        public string start_time { get; set; }
        public string end_date { get; set; }
        public string end_time { get; set; }
        public string detail_problem { get; set; }
        public string detail_solution { get; set; }
        public string reviewed_by { get; set; }
        public string attended_by { get; set; }
        public string is_breakdown { get; set; }
        public string breakdown_start_date { get; set; }
        public string breakdown_start_time { get; set; }
        public string breakdown_end_date { get; set; }
        public string breakdown_end_time { get; set; }
        public int Days { get; set; }
        public decimal? Hours { get; set; }
        public int? Hour { get; set; }
        public int breakdown_count { get; set; }
        public int Total_Malfunction_Days { get; set; }
        public int Total_Malfunction_Hours { get; set; }
        public int Breakdown_Total_Days { get; set; }
        public int Breakdown_Total_Hours { get; set; }
        //public int? Ageing_Days { get; set; }
        public string Break_Down { get; set; }
        public int? status_id { get; set; }
        public string counter_frequency { get; set; }
        public string status { get; set; }

        public DateTime? creation_date { get; set; }
        public string PLANT_NAME { get; set; }
        public string Item { get; set; }
        public double? quantity { get; set; }
        public DateTime? order_date { get; set; }
        public DateTime? completion_date { get; set; }
        public DateTime? schedule_date { get; set; }
        public DateTime? finish_date { get; set; }
        public DateTime? actual_start_date { get; set; }
        public DateTime? actual_finish_date { get; set; }
        public string maintenance_type { get; set; }
        public string actual_result { get; set; }
        public string method_used { get; set; }
        public int? self_check { get; set; }
        public int? sr_no { get; set; }
        public string document_reference { get; set; }
        public string plan_type { get; set; }

        public int? create_order_days { get; set; }
        public int? create_order_yes { get; set; }
        public string Days_Month { get; set; }
        public string Create_order_on_a_fixed_date { get; set; }
        public int? Fixed_Order_Date { get; set; }
        public int? Ageing_Days { get; set; }
        public int? auto_manual { get; set; }
        public DateTime? attending_date { get; set; }
        public string attending_time { get; set; }
        public int Total_Malfunction_minute { get; set; }
    }
}
