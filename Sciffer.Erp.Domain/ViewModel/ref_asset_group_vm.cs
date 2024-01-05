using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
   public class ref_asset_group_vm
    {
        public int asset_group_id { get; set; }
        public string asset_group_code { get; set; }
        public string asset_group_des { get; set; }
        [Display(Name = "Asset Class *")]
        public int asset_class_id { get; set; }
        public bool? is_blocked { get; set; }
        public int created_by { get; set; }
        public DateTime created_ts { get; set; }
        public int? modify_by { get; set; }
        public DateTime? modify_ts { get; set; }
    }
}
