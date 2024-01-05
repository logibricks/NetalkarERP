using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_plan_maintenance
    {
        [Key]
        public int plan_maintenance_id { get; set; }
        public string doc_number { get; set; }
        public int category_id { get; set; }
        [ForeignKey("category_id")]
        public virtual ref_document_numbring ref_document_numbring { get; set; }

        [Display(Name = "Posting Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime posting_date { get; set; }
        public int machine_id { get; set; }
        [ForeignKey("machine_id")]
        public virtual ref_machine ref_machine { get; set; }

        public int machine_category_id { get; set; }
        [ForeignKey("machine_category_id")]
        public virtual ref_machine_category ref_machine_category { get; set; }

        public int maintenance_type_id { get; set; }
        [ForeignKey("maintenance_type_id")]
        public virtual ref_maintenance_type ref_maintenance_type { get; set; }

        [Display(Name = "Plan Start Date")]
        [Required(ErrorMessage = "Plan Start Date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime plan_start_date { get; set; }

        [Display(Name = "Plan End Date")]
        [Required(ErrorMessage = "Plan End Date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime plan_end_date { get; set; }

        [Display(Name = "Start Date")]
        [Required(ErrorMessage = "Cycle Start Date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime cycle_start_date { get; set; }

        public int frequency { get; set; }

        public int frequency_type { get; set; }        
        public bool create_order_days { get; set; }
        public string create_order_dtype { get; set; }
        
        public int maintenance_period { get; set; }
        public string maintenance_period_type { get; set; }
        public int allowed_delay { get; set; }
        public string allowed_delay_type { get; set; }
        public int allowed_early_completion { get; set; }
        public string allowed_early_comlpletion_type { get; set; }
        
        public string counter_frequency { get; set; }
        public string remarks { get; set; }
        public string attachement { get; set; }
        public bool is_active { get; set; }
        public int days_before { get; set; }
        public int? create_order_yes { get; set; }

        public int plant_id { get; set; }
        [ForeignKey("plant_id")]
        public virtual REF_PLANT REF_PLANT { get; set; }

        public string model_no { get; set; }
        public string manufacturer_part_no { get; set; }
        public string manufacturing_serial_number { get; set; }
        public string asset_code_id { get; set; }
        public string asset_tag_no { get; set; }
        public string manufacturer { get; set; } 
        public bool is_blocked { get; set; }
        public int? employee_id { get; set; }
        [ForeignKey("employee_id")]
        public virtual REF_EMPLOYEE REF_EMPLOYEE { get; set; }

        public virtual ICollection<ref_plan_maintenance_detail> ref_plan_maintenance_detail { get; set; }
        public virtual ICollection<ref_plan_maintenance_component> ref_plan_maintenance_component { get; set; }
        public virtual ICollection<ref_plan_maintenance_schedule> ref_plan_maintenance_schedule { get; set; }

    }
}
