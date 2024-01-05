using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public  class ref_module_form
    {
        [Key]
        public int module_form_id { get; set; }
        [Required]
        [Display(Name ="Form Name")]
        public string module_form_name { get; set; }
        public int module_id { get; set; }
        [ForeignKey("module_id")]
        public virtual ref_module ref_module { get; set; }
        public string module_form_code { get; set; }
        public bool doc_numbering_flag { get; set; }
        public bool attachment_flag { get; set; }

        public bool? is_active { get; set; }
    }
    public class module_form_vm
    {
        public int module_form_id { get; set; }
        public string module_form_name { get; set; }
        public string module_name { get; set; }
        public string module_form_code { get; set; }
    }
}
