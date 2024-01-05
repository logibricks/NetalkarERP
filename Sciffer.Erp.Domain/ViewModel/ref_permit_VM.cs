using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.ViewModel
{
    class ref_permit_VM
    {
        [Key]
        public int permit_id { get; set; }
        [Required(ErrorMessage = "Permit number is required")]
        [Display(Name = "Permit no")]
        public string permit_no { get; set; }
        [Required(ErrorMessage = "Permit categoryis required")]
        [Display(Name = "Permit category")]
        public string permit_category { get; set; }
        public bool is_active { get; set; }
        public bool is_blocked { get; set; }
    }
}
