using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_plan_maintenance_order_cost
    {
        [Key]
        public int maintenance_order_cost_id { get; set; }
        public int item_id { get; set; }
        [ForeignKey("item_id")]
        public virtual REF_ITEM REF_ITEM { get; set; }
        public int plan_maintenance_order_id { get; set; }
        [ForeignKey("plan_maintenance_order_id")]
        public virtual ref_plan_maintenance_order plant_maintenance_order { get; set; }
        public double quantity { get; set; }
        public double? actual_quantity { get; set; }
        public int? sloc_id { get; set; }
        [ForeignKey("sloc_id")]
        public virtual REF_STORAGE_LOCATION REF_STORAGE_LOCATION { get; set; }
        public int? bucket_id { get; set; }
        [ForeignKey("bucket_id")]
        public virtual ref_bucket ref_bucket { get; set; }
        public double? value { get; set; }
        public string doc_number { get; set; } 
        public int parameter_id { get; set; }
        [ForeignKey("parameter_id")]
        public virtual ref_parameter_list ref_parameter_list { get; set; }
        [Display(Name = "Posting Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? posting_date { get; set; }      
        public bool is_active { get; set; }
        public int plan_maintenance_component_id { get; set; }
        [ForeignKey("plan_maintenance_component_id")]
        public virtual ref_plan_maintenance_component ref_plan_maintenance_component { get; set; }
        public string plan_type { get; set; }
        public int sr_no { get; set; }
        public int uom_id { get; set; }
        [ForeignKey("uom_id")]
        public virtual REF_UOM REF_UOM { get; set; }
    }
}
