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
    public class ref_plan_maintenance_order_VM
    {
        [Key]
        public int maintenance_order_id { get; set; }
        public string order_no { get; set; }
        public int category_id { get; set; }
        [ForeignKey("category_id")]
        public virtual ref_document_numbring ref_document_numbring { get; set; }

        public DateTime creation_date { get; set; }
        public int machine_id { get; set; }
        [ForeignKey("machine_id")]
        public virtual ref_machine ref_machine { get; set; }

        public int machine_category_id { get; set; }
        [ForeignKey("machine_category_id")]
        public virtual ref_machine_category ref_machine_category { get; set; }

        public int maintenance_type { get; set; }
        public string order_description { get; set; }
        public int plant_id { get; set; }
        [ForeignKey("plant_id")]
        public virtual REF_PLANT REF_PLANT { get; set; }

        public string manufacturer { get; set; }
        public string model_no { get; set; }
        public string manufacturer_part_no { get; set; }
        public string manufacturer_serial_no { get; set; }
        public string asset_code_id { get; set; }
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

        public virtual List<ref_plan_maintenance_order_parameter> ref_plan_maintenance_order_parameter { get; set; }
        public virtual List<ref_production_order_detail> ref_production_order_detail { get; set; }


    }
}
