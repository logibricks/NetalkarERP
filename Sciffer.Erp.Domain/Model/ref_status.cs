using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_status
    {
        [Key]
        public int status_id { get; set; }
        [Display(Name ="Status")]
        [Required(ErrorMessage ="status is required")]
        public string status_name { get; set; }
        public string form { get; set; }
    }
}
