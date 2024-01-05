using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_party_type
    {
        [Key]
        public int party_type_id { get; set; }
        [Required(ErrorMessage ="party name is required")]
        [Display(Name ="Party Name")]
        [StringLength(50,ErrorMessage ="text is too long")]
        public string party_type_name { get; set; }
        public bool? is_active { get; set; }
    }
}
