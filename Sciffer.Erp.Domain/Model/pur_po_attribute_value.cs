using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class pur_po_attribute_value
    {
        [Key]
        public int attribute_id { get; set; }
        public int po_id { get; set; }
        [ForeignKey("po_id")]
        public virtual pur_po pur_po { get; set; }
        [Required(ErrorMessage = "attribute value is required")]
        [Display(Name ="Attribute Value")]
        public string attribute_value { get; set; }
       
    }
}
