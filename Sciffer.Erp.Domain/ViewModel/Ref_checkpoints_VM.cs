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
 public  class Ref_checkpoints_VM
    {
        [Key]
        public int check_point_id { get; set; }

        public string checkpoints { get; set; }
        public string ideal_scenario { get; set; }
        public int permit_template_id { get; set; }

        [ForeignKey("permit_template_id")]
        public virtual Ref_permit_template Ref_permit_template { get; set; }
        [Display(Name = "Permit Category ")]
        public int? permit_category { get; set; }
        [Display(Name = "Permit Template No")]
        //public int permit_template_id { get; set; }
        //[ForeignKey("permit_template_id")]
        //public virtual Ref_permit_template Ref_permit_template { get; set; }
        public bool is_blocked { get; set; }
    }
}
