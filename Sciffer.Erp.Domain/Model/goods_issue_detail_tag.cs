using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class goods_issue_detail_tag
    {
        [Key]
        public int goods_issue_detail_tag_id { get; set; }
        public int goods_issue_id { get; set; }
        [ForeignKey("goods_issue_id")]
        public virtual goods_issue good_issue { get; set; }
        public int item_id { get; set; }
        [ForeignKey("item_id")]
        public virtual REF_ITEM REF_ITEM { get; set; }
        public int reason_id { get; set; }
        [ForeignKey("reason_id")]
        public virtual REF_REASON_DETERMINATION REF_REASON_DETERMINATION { get; set; }
        public int batch_id { get; set; }
        [ForeignKey("batch_id")]
        public virtual inv_item_batch inv_item_batch { get; set; }
        public double quantity { get; set; }
        public double rate { get; set; }
        public double value { get; set; }
        public int bucket_id { get; set; }
        [ForeignKey("bucket_id")]
        public virtual ref_bucket ref_bucket { get; set; }
        public int sloc_id { get; set; }
        [ForeignKey("sloc_id")]
        public virtual REF_STORAGE_LOCATION REF_STORAGE_LOCATION { get; set; }
        public int mrn_detail_id { get; set; }
        [ForeignKey("mrn_detail_id")]
        public virtual material_requision_note_detail material_requision_note_detail { get; set; }
        public bool order_status { get; set; }
        public int batch_detail_id { get; set; }
        [ForeignKey("batch_detail_id")]
        public virtual inv_item_batch_detail inv_item_batch_detail { get; set; }
        public int tag_id { get; set; }
        [ForeignKey("tag_id")]
        public virtual inv_item_batch_detail_tag inv_item_batch_detail_tag { get; set; }
        public int? machine_id { get; set; }
        [ForeignKey("machine_id")]
        public virtual ref_machine ref_machine { get; set; }
    }
}
