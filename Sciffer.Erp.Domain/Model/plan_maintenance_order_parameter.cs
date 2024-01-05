using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class plan_maintenance_order_parameter
    {
        [Key]
        public int planm_order_parameter_id { get; set; }

        public string parameter { get; set; }

        public string parameter_description { get; set; }
        public string range { get; set; }
        public string actual_result { get; set; }
        public string method_used { get; set; }
        public int self_check { get; set; }
        public string document_reference { get; set; }
        public bool is_active { get; set; }
        public int sr_no { get; set; }
        public int plan_maintenance_order_id { get; set; }
        [ForeignKey("plan_maintenance_order_id")]
        public virtual plan_maintenance_order plant_maintenance_order { get; set; }

        public int maintenance_detail_id { get; set; }
        [ForeignKey("maintenance_detail_id")]
        public virtual ref_plan_maintenance_detail ref_plan_maintenance_detail { get; set; }



    }
}
