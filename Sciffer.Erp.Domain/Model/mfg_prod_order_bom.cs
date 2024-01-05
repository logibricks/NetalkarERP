using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class mfg_prod_order_bom
    {
        [Key]
        public int prod_order_bom_id { get; set; }
        public int prod_order_id { get; set; }
        [ForeignKey("prod_order_id")]
        public virtual mfg_prod_order mfg_prod_order { get; set; }
        public int mfg_bom_id { get; set; }
        [ForeignKey("mfg_bom_id")]
        public virtual ref_mfg_bom ref_mfg_bom { get; set; }
    }
}
