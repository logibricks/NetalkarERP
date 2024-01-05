using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_tax_type
    {
        [Key]
        public int tax_type_id { get; set; }
        [Display(Name ="Tax Type")]
        [Required(ErrorMessage ="taxt type is required")]
        public string tax_type_name { get; set; }
        public bool is_active { get; set; }
        public bool is_blocked { get; set; }
        public bool income_excise { get; set; }
    }
}
