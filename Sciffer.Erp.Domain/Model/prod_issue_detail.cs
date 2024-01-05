using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class prod_issue_detail
    {
        [Key]
        public int prod_issue_detail_id { get; set; }
        public int prod_issue_id { get; set; }
        [ForeignKey("prod_issue_id")]
        public virtual prod_issue prod_issue { get; set; }
        public int prod_order_detail_id { get; set; }
        [ForeignKey("prod_order_detail_id")]
        public virtual mfg_prod_order_detail mfg_prod_order_detail { get; set; }
        public int item_batch_detail_id { get; set; }
        [ForeignKey("item_batch_detail_id")]
        public virtual inv_item_batch_detail inv_item_batch_detail { get; set; }
        public int in_item_id { get; set; }
        [ForeignKey("in_item_id")]
        public virtual REF_ITEM REF_ITEM { get; set; }
        public int batch_id { get; set; }
        [ForeignKey("batch_id")]
        public virtual inv_item_batch inv_item_batch { get; set; }
        public bool is_active { get; set; }
        public double quantity { get; set; }
        public double rate {get;set;}
        public double value { get; set; }


    }
}
