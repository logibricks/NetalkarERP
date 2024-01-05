using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class REF_GRADE
    {
        [Key]
        public int grade_id { get; set; }
        [Display(Name ="Grade Name")]
        public string grade_name { get; set; }
    }
}
