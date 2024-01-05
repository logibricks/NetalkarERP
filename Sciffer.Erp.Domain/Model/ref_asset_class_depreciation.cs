using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_asset_class_depreciation
    {
        [Key]
        public int asset_class_dep_id { get; set; }
        public int? dep_area_id { get; set; }
        public int? dep_type_id { get; set; }
        public int? dep_type_frquency_id { get; set; }
        public int? useful_life_months { get; set; }
        public int? useful_life_rate { get; set; }
        public bool is_blocked { get; set; }
        public int? asset_class_id { get; set; }
    }
}
