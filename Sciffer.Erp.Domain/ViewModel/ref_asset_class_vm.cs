using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class ref_asset_class_vm
    {
        
        public int asset_class_id { get; set; }
        [Display(Name = "ASSET CLASS CODE *")]
        public string asset_class_code { get; set; }
        [Display(Name = "ASSET CLASS DESCRIPTION *")]
        public string asset_class_des { get; set; }
        [Display(Name = "ASSET TYPE *")]
        public int? asset_type_id { get; set; }
        public string asset_type { get; set; }
        //public List<string> asset_class_gl_id { get; set; }
        //public List<string> ledger_account_type_id { get; set; }
        //public List<string> gl_id { get; set; }
        //public virtual List<ref_asset_class_gl_vm> ref_asset_class_gl_vm { get; set; }
        //public List<string> asset_class_dep_id { get; set; }
        //public List<string> dep_area_id { get; set; }
        //public List<string> dep_type_id { get; set; }
        //public List<string> dep_type_frquency_id { get; set; }
        //public List<string> useful_life_months { get; set; }
        //public List<string> useful_life_rate { get; set; }
        //public List<string> is_blocked { get; set; }
        //public virtual List<ref_asset_class_depreciation> ref_asset_class_depreciation { get; set; }
        public virtual List<ref_asset_class_gl_vm> ref_asset_class_gl_vm { get; set; }
        public List<ref_asset_class_gl_vm> ref_asset_class_gl_vms { get; set; }
        public virtual List<ref_asset_class_depreciation_vm> ref_asset_class_depreciation_vm { get; set; }
        public List<ref_asset_class_depreciation_vm> ref_asset_class_depreciation_vms { get; set; }
        public virtual List<ref_asset_class_gl> ref_asset_class_gl { get; set; }
        public virtual List<ref_asset_class_depreciation> ref_asset_class_depreciation { get; set; }
    }
}
