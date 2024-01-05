using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_frequency
    {
        [Key]
        public int frequency_id { get; set; }
        [Display(Name = "Frequency")]
        [Required(ErrorMessage = "frequncy is required")]
        public string frequency_name { get; set; }
    }
}
