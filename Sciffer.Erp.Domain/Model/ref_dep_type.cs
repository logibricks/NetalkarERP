using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_dep_type
    {
        [Key]
        public int dep_type_id { get; set; }
        public string dep_type_code { get; set; }
        public string dep_type_description { get; set; }
        public int? method_type_id { get; set; }
        public int? dep_area_id { get; set; }
        public int? dep_type_frquency_id { get; set; }
        public int? cal_based_on_id { get; set; }
        public decimal? scrap_value_percentage { get; set; }
        public decimal? cal_based_value { get; set; }
        public int? dep_cal_acquistion_id { get; set; }
        public int? dep_cal_retirement_id { get; set; }
        public virtual ICollection<ref_additional_dep_type> ref_additional_dep_type { get; set; }
    }
}
