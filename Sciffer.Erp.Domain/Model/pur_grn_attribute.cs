using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
     public class pur_grn_attribute
    {
        [Key]
        public int attribute_id { get; set; }
        public int grn_id { get; set; }
        [ForeignKey("grn_id")]
        public virtual pur_grn pur_grn { get; set; }
        public string attribute_value { get; set; }
    }
}
