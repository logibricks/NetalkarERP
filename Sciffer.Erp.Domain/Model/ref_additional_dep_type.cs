using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_additional_dep_type
    {
        [Key]
        public int? add_dep_type_id { get; set; }
        public int? dep_type_id { get; set; }
        public int? year_id { get; set; }
        public decimal? add_dep_percentage { get; set; }
    }
}
