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
    public class Ref_permit_template_VM
    {
        [Key]
        public int permit_template_id { get; set; }
        public string permit_template_no { get; set; }
        public string permit_category { get; set; }
        public bool is_blocked { get; set; }
        // [NotMapped]
        // public HttpPostedFileBase FileUpload { get; set; }
        public virtual IList<Ref_checkpoints> Ref_checkpoints { get; set; }
        public List<string> checkpoints { get; set; }
        public List<string> ideal_scenario { get; set; }
        public List<string> checkpoint_id { get; set; }

        public string deleteids { get; set; }

    }
}

