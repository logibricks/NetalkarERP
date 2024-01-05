using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class pur_grn_form
    {
        [Key]
        [Column(Order = 0)]
        public int form_id { get; set; }
        [ForeignKey("form_id")]
        public virtual REF_FORM REF_FORM { get; set; }
        [Key]
        [Column(Order = 1)]
        public int grn_id { get; set; }
        [ForeignKey("grn_id")]
        public virtual pur_grn pur_grn { get; set; }
    }
}
