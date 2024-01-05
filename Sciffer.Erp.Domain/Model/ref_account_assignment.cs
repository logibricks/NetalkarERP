using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_account_assignment
    {
        [Key]
        public int account_assignment_id { get; set; }
        [Required(ErrorMessage = "account assignment is required")]
        [Display(Name = "Account Assignment")]
        public string account_assignment_name { get; set; }
    }
}
