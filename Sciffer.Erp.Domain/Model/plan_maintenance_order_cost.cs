using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class plan_maintenance_order_cost
    {
        [Key]
        public int planm_order_cost_id { get; set; }

        public int item_id { get; set; }
        [ForeignKey("item_id")]
        public virtual REF_ITEM REF_ITEM { get; set; }
        public string related_parameter { get; set; }
        public float quantity { get; set; }

        public float actual_quantity { get; set; }
        public string batch { get; set; }
        public int storage_location_id { get; set; }
        public string bucket { get; set; }

        public int document_numbering_id { get; set; }
        [ForeignKey("document_numbering_id")]
        public virtual ref_document_numbring ref_document_numbring { get; set; }

        [Display(Name = "Posting Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime posting_date { get; set; }

        public int plan_maintenance_order_id { get; set; }
        [ForeignKey("plan_maintenance_order_id")]
        public virtual plan_maintenance_order plant_maintenance_order { get; set; }

        public int sr_no { get; set; }
        public bool is_active { get; set; }

        public int plan_maintenance_component_id { get; set; }
        [ForeignKey("plan_maintenance_component_id")]
        public virtual ref_plan_maintenance_component ref_plan_maintenance_component { get; set; }

        

        }
}
