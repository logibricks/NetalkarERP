using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_tax_charged_on
    {
        [Key]
        public int tax_chargerd_on_id { get; set; }
        [Required]
        public string tax_chargerd_on_name { get; set; }
    }
}
