using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
     public class REF_FORM
    {
        [Key]
        public int FORM_ID { get; set; }
        [Display(Name ="Form Name")]
        public String FORM_NAME { get; set; }
        public bool IS_BLOCKED { get; set; }
    }
}
