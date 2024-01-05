using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
    public  class ref_module
    {
        [Key]
        public int module_id { get; set; }
        [Required]
        [Display(Name ="Module")]
        public string module_name { get; set; }
        public bool? is_active { get; set; }
    }
}
