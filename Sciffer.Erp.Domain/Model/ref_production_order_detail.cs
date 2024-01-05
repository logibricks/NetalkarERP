using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_production_order_detail
    {
        [Key]
        public int prod_order_detail_id { get; set; }
        public int prod_order_id { get; set; }
        [ForeignKey("prod_order_id")]
        public virtual ref_production_order ref_production_order { get; set; }
        public int uom_id_bom { get; set; }
        [ForeignKey("uom_id_bom")]
        public virtual REF_UOM REF_UOM { get; set; }
        public int item_id_bom { get; set; }
        [ForeignKey("item_id_bom")]
        public virtual REF_ITEM REF_ITEM { get; set; }
        public int sloc_id_bom { get; set; }
        [ForeignKey("sloc_id_bom")]
        public virtual REF_STORAGE_LOCATION REF_STORAGE_LOCATION { get; set; }
        public double req_qty_bom { get; set; }
    }
}
