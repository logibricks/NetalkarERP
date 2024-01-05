using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Sciffer.Erp.Domain.Model
{
   public class ref_permit_details
    {
        [Key]

        public int permit_detail_id { get; set; }
        public string checkpoints { get; set; }
        public string ideal_scenario { get; set; }

        public int permit_id { get; set; }
        [ForeignKey("permit_id")]
        public virtual ref_permit_issue ref_permit_issue { get; set; }

        public int check_point_id { get; set; }
        [ForeignKey("check_point_id")]
        public virtual Ref_checkpoints Ref_checkpoints { get; set; }
        
        public int permit_template_id { get; set; }
        [ForeignKey("permit_template_id")]
        public virtual Ref_permit_template Ref_permit_template { get; set; }

        public bool is_blocked { get; set; }

    }
}
