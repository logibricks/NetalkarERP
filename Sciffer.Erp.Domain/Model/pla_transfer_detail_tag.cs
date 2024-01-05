using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
   public class pla_transfer_detail_tag
    {
        [Key]
        public int pla_transfer_detail_tag_id { get; set; }

        public int? pla_transfer_id { get; set; }
        [ForeignKey("pla_transfer_id")]
        public virtual pla_transfer pla_transfer { get; set; }

        public int? tag_id { get; set; }
        [ForeignKey("tag_id")]
        public virtual inv_item_batch_detail_tag inv_item_batch_detail_tag { get; set; }

        public double? quantity { get; set; }

        public int? batch_id { get; set; }
        [ForeignKey("batch_id")]
        public virtual inv_item_batch inv_item_batch { get; set; }

        public int? item_id { get; set; }
        [ForeignKey("item_id")]
        public virtual REF_ITEM REF_ITEM { get; set; }

        public int? item_batch_detail_id { get; set; }
        [ForeignKey("item_batch_detail_id")]
        public virtual inv_item_batch_detail inv_item_batch_detail { get; set; }


        public int? tsr_no { get; set; }

        public int? uom_id { get; set; }
        [ForeignKey("uom_id")]
        public virtual REF_UOM REF_UOM { get; set; }

        public string tag_no { get; set; }

        public bool? is_active { get; set; }
    }
}
