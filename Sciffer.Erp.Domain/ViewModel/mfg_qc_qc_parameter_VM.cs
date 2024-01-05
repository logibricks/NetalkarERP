using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.Model;

namespace Sciffer.Erp.Domain.ViewModel
{
    public  class mfg_qc_qc_parameter_VM
    {
        [Key]
        public int? mfg_qc_qc_parameter_id { get; set; }

        [Display(Name = "Select Machine")]
        public int? machine_id { get; set; }
        public string machine_name { get; set; }
        [Display(Name = "Select Item")]
        public int? item_id { get; set; }
        [Display(Name = "Select Machine")]
        public string machine_list_id { get; set; }
        public string item_name { get; set; }
        [Display(Name = "Numeric")]
        public bool is_numeric { get; set; }
        public int srno { get; set; }

        public List<string> mfg_qc_qc_parameter_list_id { get; set; }

        public List<string> parameter_name { get; set; }
        public List<string> paramter_uom { get; set; }
        public List<string> std_range_start { get; set; }
        public List<string> std_range_end { get; set; }
        public List<bool> is_numeric1 { get; set; }
        public string deleteids { get; set; }
        public virtual IList<mfg_qc_qc_parameter_list> mfg_qc_qc_parameter_list { get; set; }

        [Display(Name = "Select Operation *")]
        public int? process_id { get; set; }
        public string process_code { get; set; }

        public string username { get; set; }

        public DateTime? modify_ts { get; set; }



    }
}
