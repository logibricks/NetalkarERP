using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class mfg_prod_order_detail_tag
    {
        [Key]
        public int prod_order_detail_tag_id { get; set; }
        public int prod_order_detail_id { get; set; }
        [ForeignKey("prod_order_detail_id")]
        public virtual mfg_prod_order_detail mfg_prod_order_detail { get; set; }
        public int tag_id { get; set; }
        [ForeignKey("tag_id")]
        public virtual inv_item_batch_detail_tag inv_item_batch_detail_tag { get; set; }
    }
}
