using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class sal_si_detail_batch
    {
        [Key]
        public int si_detail_batch_id { get; set; }
        public int si_detail_id { get; set; }
        public int item_batch_detail_id { get; set; }
        [ForeignKey("item_batch_detail_id")]
        public virtual inv_item_batch_detail inv_item_batch_detail { get; set; }
        public double quantity { get; set; }
    }
}
