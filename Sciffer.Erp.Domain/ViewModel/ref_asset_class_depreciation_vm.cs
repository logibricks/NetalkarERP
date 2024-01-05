using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class ref_asset_class_depreciation_vm
    {
        public int asset_class_dep_id { get; set; }
        public int? dep_area_id { get; set; }
        public int? dep_type_id { get; set; }
        public int? dep_type_frquency_id { get; set; }
        public int? useful_life_months { get; set; }
        public int? useful_life_rate { get; set; }
        public bool is_blocked { get; set; }
        public int? asset_class_id { get; set; }
        public string dep_area_code { get; set; }
        public string dep_type_code { get; set; }
        public string dep_type_frquency { get; set; }
    }
}
