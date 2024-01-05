using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sciffer.Erp.Domain.Model
{
    public class plan_maintenance_order
    {
        [Key]
        public int plan_maintenance_order_id { get; set; }

        public string order_no { get; set; }
        public int machine_id { get; set; }
        [ForeignKey("machine_id")]
        public virtual ref_machine ref_machine { get; set; }

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

        public string manufacturer { get; set; }
        public string model_no { get; set; }
        public string manufacturer_part_no { get; set; }
        public string manufacturer_serial_no { get; set; }
        public string asset_code_id { get; set; }
        public string asset_tag_no { get; set; }


        [Display(Name = "Scheduled Date")]
        [Required(ErrorMessage = "Scheduled Date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime scheduled_date { get; set; }

        [Display(Name = "Finish Date")]
        [Required(ErrorMessage = "Finish Date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime finish_date { get; set; }

        [Display(Name = "Actual Start Date")]
        [Required(ErrorMessage = "Actual Start Date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime actual_start_date { get; set; }

        [Display(Name = "Actual Finish Date")]
        [Required(ErrorMessage = "Actual Finish Date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime actual_finish_date { get; set; }


        
        public string order_executed_by { get; set; }
        public string order_approved_by { get; set; }
        public string permit_no { get; set; }
        public string notification_no { get; set; }
        public string remarks { get; set; }
        public string attachement { get; set; }

        public int machine_category_id { get; set; }
        [ForeignKey("machine_category_id")]
        public virtual ref_machine_category ref_machine_category { get; set; }

        public bool is_active { get; set; }

        public virtual ICollection<plan_maintenance_order_cost> plan_maintenance_order_cost { get; set; }
        public virtual ICollection<plan_maintenance_order_parameter> plan_maintenance_order_parameter { get; set; }
    }
}
