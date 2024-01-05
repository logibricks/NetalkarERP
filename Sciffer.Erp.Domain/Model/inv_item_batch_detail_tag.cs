using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class inv_item_batch_detail_tag
    {
        [Key]
        public int tag_id { get; set; }
        public int item_batch_id { get; set; }
        [ForeignKey("item_batch_id")]
        public virtual inv_item_batch inv_item_batch { get; set; }
        public string tag_no { get; set; }
        public double? qty { get; set; }
        public double? balance_qty { get; set; }
        public int plant_id { get; set; }
        [ForeignKey("plant_id")]
        public virtual REF_PLANT REF_PLANT { get; set; }
        public int sloc_id { get; set; }
        [ForeignKey("sloc_id")]
        public virtual REF_STORAGE_LOCATION REF_STORAGE_LOCATION { get; set; }
        public int bucket_id { get; set; }
        [ForeignKey("bucket_id")]
        public virtual ref_bucket ref_bucket { get; set; }

        public string heat_code { get; set; }
        public string run_code { get; set; }
        public string tag_no_two { get; set; }
    }
    public class inv_item_batch_detail_tag_vm
    {
        public int tag_id { get; set; }
        public string tag_no { get; set; }
        public int item_id { get; set; }
        public double? balance_qty { get; set; }
    }
}
