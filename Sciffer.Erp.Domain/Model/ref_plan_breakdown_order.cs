using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_plan_breakdown_order
    {
        [Key]
        public int plan_breakdown_order_id { get; set; }
        public int category_id { get; set; }
        [ForeignKey("category_id")]
        public virtual ref_document_numbring ref_document_numbring { get; set; }
        public string doc_number { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime creation_date { get; set; }
        public int maintenance_type_id { get; set; }
        [ForeignKey("maintenance_type_id")]
        public virtual ref_maintenance_type ref_maintenance_type { get; set; }
        public int plant_id { get; set; }
        [ForeignKey("plant_id")]
        public virtual REF_PLANT REF_PLANT { get; set; }
        //public string machine_id { get; set; }
        //[ForeignKey("machine_id")]
        //public virtual ref_machine ref_machine { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? actual_start_date { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? actual_finish_date { get; set; }
        public int order_executed_by { get; set; }
        [ForeignKey("order_executed_by")]
        public virtual REF_EMPLOYEE REF_EMPLOYEE_order_executed_by { get; set; }
        public int order_approved_by { get; set; }
        [ForeignKey("order_approved_by")]
        public virtual REF_EMPLOYEE REF_EMPLOYEE_order_approved_by { get; set; }
        public int permit_id { get; set; }
        public int notification_id { get; set; }
        public string remarks { get; set; }
        public string attachment { get; set; }
        public bool is_active { get; set; }
        public int created_by { get; set; }
        public DateTime created_ts { get; set; }
        public int employee_id { get; set; }
        [ForeignKey("employee_id")]
        public virtual REF_EMPLOYEE REF_EMPLOYEE { get; set; }
        public virtual ICollection<ref_plan_breakdown_parameter> ref_plan_breakdown_parameter { get; set; }
        public virtual ICollection<ref_plan_breakdown_cost> ref_plan_breakdown_cost { get; set; }
        public virtual ICollection<ref_plan_breakdown_service_cost> ref_plan_breakdown_service_cost { get; set; }
        [Display(Name = "Actual Start Time *")]
        [DataType(DataType.Time)]
        public TimeSpan Actual_start_time { get; set; }
        [Display(Name = "actual End Time *")]
        [DataType(DataType.Time)]
        public TimeSpan actual_end_time { get; set; }
        //public virtual ICollection<map_machine_breakdown_order> map_machine_breakdown_order { get; set; }
        //added 06-12
        public string order_executedby { get; set; }
        public int? rm_item_id { get; set; }
        public string notification_description { get; set; }

        public virtual ref_pm_notification Ref_Pm_Notification { get; set; }

    }
}
