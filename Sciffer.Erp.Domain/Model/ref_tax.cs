using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_tax
    {
        [Key]
        public int tax_id { get; set; }
        [Display(Name ="Tax Name *")]
        public string tax_name { get; set; }
        [Display(Name = "Tax Code *")]
        public string tax_code { get; set; }
        [Display(Name = "Block")]
        public bool is_blocked { get; set; }
        public virtual ICollection<ref_tax_detail> ref_tax_detail { get; set; }
    }
}
