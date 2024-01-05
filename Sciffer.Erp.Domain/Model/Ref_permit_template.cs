using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
   public class Ref_permit_template
    {
        [Key]
        public int permit_template_id { get; set; }
        public string permit_template_no { get; set; }
        public string permit_category { get; set; }
        public bool is_blocked { get; set; }
        public virtual ICollection<Ref_checkpoints> Ref_checkpoints { get; set; }
        public object hidden { get; set; }
    }
}
