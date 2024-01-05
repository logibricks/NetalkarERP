using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class inv_item_batch_detail
    {
        [Key]
        public int item_batch_detail_id { get; set; }
        public int item_batch_id { get; set; }
        [ForeignKey("item_batch_id")]
        public virtual inv_item_batch inv_item_batch { get; set; }
        public int plant_id { get; set; }
        [ForeignKey("plant_id")]
        public virtual REF_PLANT REF_PLANT { get; set; }
        public int sloc_id { get; set; }
        [ForeignKey("sloc_id")]
        public virtual REF_STORAGE_LOCATION REF_STORAGE_LOCATION { get; set; }
        public int bucket_id { get; set; }
        [ForeignKey("bucket_id")]
        public virtual ref_bucket ref_bucket { get; set; }
        public double? qty { get; set; }
        public double? balance_qty { get; set; }
        public int uom_id { get; set; }
        [ForeignKey("uom_id")]
        public virtual REF_UOM REF_UOM { get; set; }
    }
}
