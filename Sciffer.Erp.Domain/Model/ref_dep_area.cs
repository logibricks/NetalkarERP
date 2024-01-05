using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_dep_area
    {
        [Key]
        public int dep_area_id { get; set; }
        public string dep_area_code { get; set; }
        public string dep_area_description { get; set; }
        public int? dep_type_id { get; set; }
        public int? dep_posting_id { get; set; }
        public bool? is_blocked { get; set; }
        public int? created_by { get; set; }
        public DateTime? created_ts { get; set; }
        public int? modify_by { get; set; }
        public DateTime? modify_ts { get; set; }
        public int? dep_type_frquency_id { get; set; }
        public int? no_of_periods { get; set; }
        public string financial_year_id { get; set; }
        public virtual ICollection<ref_dep_posting_period> ref_dep_posting_period { get; set; }

    }
}
