using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class goods_issue_batch
    {
        [Key]
        public int goods_issue_batch_id { get; set; }

        public int item_id { get; set; }
        [ForeignKey("item_id")]
        public virtual REF_ITEM REF_ITEM { get; set; }

        public int batch_id { get; set; }
        [ForeignKey("batch_id")]
        public virtual inv_item_batch inv_item_batch { get; set; }

        public double quantity { get; set; }
        
        public int goods_issue_detail_id { get; set; }
        [ForeignKey("goods_issue_detail_id")]
        public virtual goods_issue_detail goods_issue_detail { get; set; }
    }

    public class goods_issue_batch_VM
    {
        public int goods_issue_batch_id { get; set; }
        public int item_id { get; set; }
        public int batch_id { get; set; }
        public double quantity { get; set; }
        public int goods_issue_detail_id { get; set; }
        public string batch_number { get; set; }
    }
}
