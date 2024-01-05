using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class rejection_receipt_detail
    {
        [Key]
        public int reject_receipt_detail_id { get; set; }

        public int reject_receipt_id { get; set; }
        //[ForeignKey("prod_receipt_id")]
        //public virtual prod_receipt prod_receipt { get; set; }

        public int prod_order_detail_id { get; set; }
        //[ForeignKey("prod_order_detail_id")]
        //public virtual mfg_prod_order_detail mfg_prod_order_detail { get; set; }

        public int out_item_id { get; set; }
        [ForeignKey("out_item_id")]
        public virtual REF_ITEM REF_ITEM { get; set; }

        public int storage_location_id { get; set; }
        [ForeignKey("storage_location_id")]
        public virtual REF_STORAGE_LOCATION REF_STORAGE_LOCATION { get; set; }

        public bool is_active { get; set; }

        public double quantity { get; set; }

        public int uom_id { get; set; }
        [ForeignKey("uom_id")]
        public virtual REF_UOM REF_UOM { get; set; }

        public int? batch_id { get; set; }
        [ForeignKey("batch_id")]
        public virtual inv_item_batch inv_item_batch { get; set; }

        public int? tag_id { get; set; }
        [ForeignKey("tag_id")]
        public virtual inv_item_batch_detail_tag inv_item_batch_detail_tag { get; set; }

        public int reason_id { get; set; }
        [ForeignKey("reason_id")]
        public virtual REF_REASON_DETERMINATION REF_REASON_DETERMINATION { get; set; }
    }
}
