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
    public class ref_dep_type_vm
    {
        public int dep_type_id { get; set; }
        [Display(Name = "CODE *")]
        public string dep_type_code { get; set; }
        [Display(Name = "DESCRIPTION *")]
        public string dep_type_description { get; set; }
        [Display(Name = "METHOD *")]
        public int? method_type_id { get; set; }
        [Display(Name = "DEPRECIATION FREQUENCY *")]
        public int? dep_type_frquency_id { get; set; }
        [Display(Name = "DEPRECIATION AREA *")]
        public int? dep_area_id { get; set; }
        [Display(Name = "CALCUALTION BASED ON ")]
        public int? cal_based_on_id { get; set; }
        [Display(Name = "SCRAP VALUE ( % TO COST)")]
        public decimal? scrap_value_percentage { get; set; }
        public decimal? cal_based_value { get; set; }
        [Display(Name = "ACQUISTION")]
        public int? dep_cal_acquistion_id { get; set; }
        [Display(Name = "RETIREMENT")]
        public int? dep_cal_retirement_id { get; set; }
        public List<string> add_dep_type_id { get; set; }
        public List<string> year_id { get; set; }
        public List<string> add_dep_percentage { get; set; }
        public string method { get; set; }
        public string dep_fre { get; set; }
        public string dep_area_code { get; set; }
        public string cal_based { get; set; }
        public virtual List<ref_additional_dep_type> ref_additional_dep_type { get; set; }
    }
}
