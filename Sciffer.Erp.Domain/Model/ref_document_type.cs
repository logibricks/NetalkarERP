using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_document_type
    {
        [Key]
        public int document_type_id { get; set; }
        [Display(Name ="Document Type")]
        [Required(ErrorMessage ="Document Type is required")]
        public string document_type_name { get; set; }
        public bool is_active { get; set; }
        public string document_type_code { get; set; }
    }
}
