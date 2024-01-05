using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_tax_element_detail
    {
        [Key]
        public int tax_element_detail_id { get; set; }
        [Display(Name = "Effective From")]
        [Required(ErrorMessage ="effective from is required")]
        public DateTime effective_from { get; set; }       
        [Display(Name = "Rate % ")]
        [Required(ErrorMessage ="rate is requirede")]
        public double rate { get; set; }
        [Display(Name = "No Setoff %")]
        [Required(ErrorMessage = "rate is requirede")]
        public double no_setoff { get; set; }
        [Display(Name = "On Hold %")]
        public double on_hold { get; set; }
        public int tax_element_id { get; set; }
        [ForeignKey("tax_element_id")]
        public virtual ref_tax_element ref_tax_element { get; set; }
        public bool is_active { get; set; }
    }
}
