using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_asset_class
    {
        [Key]
        public int asset_class_id { get; set; }
        public string asset_class_code { get; set; }
        public string asset_class_des { get; set; }
        public int asset_type_id { get; set; }
        public bool is_active { get; set; }
        public virtual ICollection<ref_asset_class_gl> ref_asset_class_gl { get; set; }
        public virtual ICollection<ref_asset_class_depreciation> ref_asset_class_depreciation { get; set; }
    }
}
