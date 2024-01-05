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
    public class plan_maintenance_order_VM
    {
        public int maintenance_order_id { get; set; }
        public string order_no { get; set; }
        public string machine_name { get; set; }
        public int machine_id { get; set; }
        [ForeignKey("machine_id")]
        public virtual ref_machine ref_machine { get; set; }
        public int plan_maintenance_id { get; set; }
        public int category_id { get; set; }

        [Display(Name = "Creation Date")]
        [Required(ErrorMessage = "Creation Date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime creation_date { get; set; }
        public int maintenance_type_id { get; set; }
        [ForeignKey("maintenance_type_id")]
        public virtual ref_maintenance_type ref_maintenance_type { get; set; }
        public string order_desc { get; set; }
        public int plant_id { get; set; }
        [ForeignKey("plant_id")]
        public virtual REF_PLANT REF_PLANT { get; set; }
        public int plan_maintenance_order_id { get; set; }
        public string manufacturer { get; set; }
        public string model_no { get; set; }
        public string manufacturer_part_no { get; set; }
        public string manufacturer_serial_no { get; set; }
        public string asset_code_id { get; set; }
        public string asset_tag_no { get; set; }
        [DataType(DataType.Time)]
        public TimeSpan? actual_start_time { get; set; }
        [DataType(DataType.Time)]
        public TimeSpan? actual_finish_time { get; set; }
        [Display(Name = "Scheduled Date")]
        [Required(ErrorMessage = "Scheduled Date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? schedule_date { get; set; }

        [Display(Name = "Finish Date")]
        [Required(ErrorMessage = "Finish Date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? finish_date { get; set; }

        [Display(Name = "Actual Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? actual_start_date { get; set; }

        [Display(Name = "Actual Finish Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? actual_finish_date { get; set; }
        public int? order_executed_by { get; set; }
        [ForeignKey("order_executed_by")]
        public virtual REF_EMPLOYEE REF_EMPLOYEE { get; set; }
        public int? order_approved_by { get; set; }
        [ForeignKey("order_approved_by")]
        public virtual REF_EMPLOYEE REF_EMPLOYEE1 { get; set; }
        public string machine_category_name { get; set; }
        public string maintenance_type_name { get; set; }
        public string permit_no { get; set; }
        public string notification_no { get; set; }
        [DataType(DataType.MultilineText)]
        public string remarks { get; set; }
        public string attachement { get; set; }

        public int machine_category_id { get; set; }
        [ForeignKey("machine_category_id")]
        public virtual ref_machine_category ref_machine_category { get; set; }
        public bool is_active { get; set; }
        public string deleteids { get; set; }
        public string deleteids1 { get; set; }
        public virtual List<ref_plan_maintenance_order_cost> ref_plan_maintenance_order_cost { get; set; }
        public virtual List<ref_plan_maintenance_order_parameter> ref_plan_maintenance_order_parameter { get; set; }
        public List<string> maintenance_parameter_id { get; set; }
        public List<string> parameter_sr_no { get; set; }
        public List<string> parameter_code_id { get; set; }
        public List<string> range { get; set; }
        public List<string> actual_result { get; set; }
        public List<string> method_used { get; set; }
        public List<string> shelf_check { get; set; }
        public List<string> document_reference { get; set; }
        public List<string> maintenannce_detail_id { get; set; }
        public List<string> component_detail_id { get; set; }
        public List<string> component_sr_no { get; set; }
        public List<string> item_id { get; set; }
        public List<string> uom_id { get; set; }
        public List<string> component_parameter_id { get; set; }
        public List<double> req_quantity { get; set; }
        public List<string> actual_quantity { get; set; }
        public List<string> batch_id { get; set; }
        public List<string> sloc_id { get; set; }
        public List<string> bucket_id { get; set; }
        public List<string> doc_number { get; set; }
        public List<string> posting_date { get; set; }
        public List<string> plan_maintenance_component_id { get; set; }
        public List<string> plan_type { get; set; }
        //added_06_12
        public List<int> attended_bys { get; set; }
        public string plant_name { get; set; }
        public string Maintenance_Plan_Maintenance_Number { get; set; }
        public string maintenance_type { get; set; }
        public string creation_date11 { get; set; }
        public string schedule_date11 { get; set; }
        public string finish_date11 { get; set; }
        public string actual_start_date11 { get; set; }
        public string actual_finish_date11 { get; set; }
        public string company_name { get; set; }
        public string company_address { get; set; }
        public string state_name { get; set; }
        //public string range11 { get; set; }
        //public string actual_result11 { get; set; }
        //public string method_used11 { get; set; }
        //public string self_check11 { get; set; }
        //public string document_reference11 { get; set; }






    }
    public class plan_maintenance_order_detail_VM
    {

        public int plan_maintenance_order_id { get; set; }
        public string sr_no { get; set; }
        public string parameter_desc { get; set; }
        public string range { get; set; }
        public string actual_result { get; set; }
        public string method_used { get; set; }
        public int? self_check { get; set; }
        public string document_reference { get; set; }


        public string item_name { get; set; }
        public double actual_quantity { get; set; }
        public double require_quantity { get; set; }
        public string uom_name { get; set; }
        public string sloc { get; set; }
        public string bucket { get; set; }

    }
}