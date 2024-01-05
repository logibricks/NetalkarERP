using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class mfg_prod_order_detail
    {
        [Key]
        public int prod_order_detail_id { get; set; }
        public int prod_order_id { get; set; }
        [ForeignKey("prod_order_id")]
        public virtual mfg_prod_order mfg_prod_order { get; set; }
        public int? item_batch_detail_id { get; set; }
        [ForeignKey("item_batch_detail_id")]
        public virtual inv_item_batch_detail inv_item_batch_detail { get; set; }
        public int? batch_id { get; set; }
        [ForeignKey("batch_id")]
        public virtual inv_item_batch inv_item_batch { get; set; }
        public int in_item_id { get; set; }
        [ForeignKey("in_item_id")]
        public virtual REF_ITEM REF_ITEM { get; set; }
        public double quantity { get; set; }
        public int in_uom_id { get; set; }
        [ForeignKey("in_uom_id")]
        public virtual REF_UOM REF_UOM { get; set; }
        public bool is_active { get; set; }
        public bool order_status { get; set; }
        public double bal_quantity { get; set; }
    }
}
