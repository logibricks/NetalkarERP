using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_plan_breakdown_parameter
    {
        [Key]
        public int plan_breakdown_parameter_id { get; set; }
        public int parameter_id { get; set; }
        [ForeignKey("parameter_id")]
        public virtual ref_parameter_list ref_parameter_list { get; set; }
        public string parameter_range { get; set; }
        public string actual_result { get; set; }
        public string method_used { get; set; }
        public int self_vendor_id { get; set; }
        public string doc_reference { get; set; }
        public int plan_breakdown_order_id { get; set; }
        [ForeignKey("plan_breakdown_order_id")]
        public virtual ref_plan_breakdown_order ref_plan_breakdown_order { get; set; }
        public bool is_active { get; set; }
    }
}
