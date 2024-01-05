using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_tax_detail
    {
        [Key]
        public int tax_detail_id { get; set; }
        public int tax_id { get; set; }
        [ForeignKey("tax_id")]
        public virtual ref_tax ref_tax { get; set; }
        public int tax_element_id { get; set; }
        [ForeignKey("tax_element_id")]
        public virtual ref_tax_element ref_tax_element { get; set; }
        public string tax_charged_on { get; set; }      
        public bool is_active { get; set; }
    }
}
