using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
   public class ref_plan_maintenance_detail
    {
        [Key]
        public int maintenance_detail_id { get; set; }
        public int plan_maintenance_id { get; set; }
        [ForeignKey("plan_maintenance_id")]
        public virtual ref_plan_maintenance ref_plan_maintenance { get; set; }

        public int? parameter_id { get; set; }
        [ForeignKey("parameter_id")]
        public virtual ref_parameter_list ref_parameter_list { get; set; }

        public string parameter_range { get; set; }
        public int sr_no { get; set; }
        public bool is_active { get; set; }
    }
    public class ref_plan_maintenance_details
    {
        public int maintenance_detail_id { get; set; }
        public int plan_maintenance_id { get; set; }
        public int? parameter_id { get; set; }
        public string parameter_code { get; set; }
        public string parameter_range { get; set; }
        public int sr_no { get; set; }
        public bool is_active { get; set; }
    }
}
