using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Sciffer.Erp.Domain.Model
{
   public class Ref_checkpoints
    {
        [Key]
        public int check_point_id { get; set; }
      
        public string checkpoints { get; set; }
        public string ideal_scenario { get; set; }
        public int permit_template_id { get; set; }

        [ForeignKey("permit_template_id")]
        public virtual Ref_permit_template Ref_permit_template { get; set; }
        public bool is_blocked { get; set; }

    }
}
