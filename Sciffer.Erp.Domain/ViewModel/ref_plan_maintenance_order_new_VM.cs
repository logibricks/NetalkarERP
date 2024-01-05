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
    public class ref_plan_maintenance_order_new_VM
    {
        [Key]
        public int maintenance_order_id { get; set; }
        public string order_no { get; set; }
        public int category_id { get; set; }
        public DateTime creation_date { get; set; }
        public int machine_id { get; set; }
        public string machine_name { get; set; }
        public int machine_category_id { get; set; }
        public string machine_category_name { get; set; }
        public int maintenance_type { get; set; }
        public string maintenance_type_name { get; set; }
        public string order_description { get; set; }
        public int plant_id { get; set; }
        public string manufacturer { get; set; }
        public string model_no { get; set; }
        public string manufacturer_part_no { get; set; }
        public string manufacturer_serial_no { get; set; }
        public string asset_code_id { get; set; }
        public string plan_maintenance_order_no { get; set; }
        public string asset_tag_no { get; set; }
        public DateTime schedule_date { get; set; }
        public DateTime finish_date { get; set; }
        public DateTime actual_start_date { get; set; }
        public DateTime actual_finish_date { get; set; }
        public int order_executed_by { get; set; }
        public int order_approved_by { get; set; }
        public string permit_no { get; set; }
        public string notification_no { get; set; }
        [DataType(DataType.MultilineText)]
        public string remarks { get; set; }
        public virtual List<ref_plan_maintenance_order_parameter_vm> plan_maintenance_order_parameter { get; set; }
    }

    public class ref_plan_maintenance_order_parameter_vm
    {
        public int plan_order_parameter_id { get; set; }
        public int parameter_id { get; set; }
        public string paramaterName { get; set; }
        public string employeeName { get; set; }
        public string range { get; set; }
        public string actual_result { get; set; }
        public string method_used { get; set; }
        public int? self_check { get; set; }
        public string document_reference { get; set; }
        public bool is_active { get; set; }
        public int sr_no { get; set; }
        public int plan_maintenance_order_id { get; set; }
        public int maintenance_detail_id { get; set; }
        public int? attended_by { get; set; }
    }
}
