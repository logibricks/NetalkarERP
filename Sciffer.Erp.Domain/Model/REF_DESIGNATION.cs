using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class REF_DESIGNATION
    {
        [Key]
        public int designation_id { get; set; }
        [Display(Name ="Designation Name")]
        public string designation_name { get; set; }
        [Display(Name = "Designation Code")]
        public string designation_code { get; set; }

    }
}
