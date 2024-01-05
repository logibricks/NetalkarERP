using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.Model;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class ref_hsn_codeVM
    {
        [Key]
        public int hsn_code_id { get; set; }
        [Display(Name ="HSN Code")]
        [Required]
        public string hsn_code { get; set; }
        [Required]
        [Display(Name = "HSN Code Description")]
        public string hsn_code_description { get; set; }
        [Display(Name = "Blocked")]
        public bool is_blocked { get; set; }

    }

    public class ref_hsn_code_VM
    {
        public int hsn_code_id { get; set; }
        public string hsn_code { get; set; }
        public string hsn_code_description { get; set; }
        public bool is_blocked { get; set; }
    }
    public class ref_hsn_code_grn
    {
        public int? sac_id { get; set; }
        public List<ref_hsn_code_vm> ref_hsn_code_vm { get; set; }
    }
}
