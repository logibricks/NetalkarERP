using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_plan_maintenance_component
    {
        [Key]
        public int plan_maintenance_component_id { get; set; }
        public int item_id { get; set; }
        [ForeignKey("item_id")]
        public virtual REF_ITEM REF_ITEM { get; set; }
       
        public double quantity { get; set; }
        public int sr_no { get; set; }
        public int parameter_id { get; set; }
        [ForeignKey("parameter_id")]
        public virtual ref_parameter_list ref_parameter_list { get; set; }

        public int plan_maintenance_id { get; set; }
        [ForeignKey("plan_maintenance_id")]
        public virtual ref_plan_maintenance ref_plan_maintenance { get; set; }        
        public bool is_active { get; set; }

        public int uom_id { get; set; }
        [ForeignKey("uom_id")]
        public virtual REF_UOM REF_UOM { get; set; }
    }
    public class ref_plan_maintenance_components
    {
        public int plan_maintenance_component_id { get; set; }
        public int item_id { get; set; }
        public double quantity { get; set; }
        public int sr_no { get; set; }
        public int parameter_id { get; set; }
        public int plan_maintenance_id { get; set; }
        public bool is_active { get; set; }
        public int uom_id { get; set; }
        public string item_code { get; set; }
        public string uom_code { get; set; }
        public string sloc_code { get; set; }
        public string parameter_code { get; set; }
    }
}
