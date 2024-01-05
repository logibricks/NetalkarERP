using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class jobwork_rejection_detail
    {
        [Key]
        public int jobwork_rejection_detail_id { get; set; }
        
        public int job_work_detail_in_id { get; set; }
        [ForeignKey("job_work_detail_in_id")]
        public virtual in_jobwork_in_detail in_jobwork_in_detail { get; set; }
        public int item_id { get; set; }
        [ForeignKey("item_id")]
        public virtual REF_ITEM REF_ITEM { get; set; }
        public int uom_id { get; set; }
        [ForeignKey("uom_id")]
        public virtual REF_UOM REF_UOM { get; set; }

        public int sloc_id { get; set; }
        [ForeignKey("sloc_id")]
        public virtual REF_STORAGE_LOCATION REF_STORAGE_LOCATION { get; set; }

        public int batch_id { get; set; }
        [ForeignKey("batch_id")]
        public virtual inv_item_batch inv_item_batch { get; set; }
        public double batch_bal_qty { get; set; }
         
        public double quantity { get; set; }
        public int bucket_id { get; set; }
        [ForeignKey("bucket_id")]
        public virtual ref_bucket ref_bucket { get; set; }        
        public int jobwork_rejection_id { get; set; }        
        public double rate { get; set; }
        public double value { get; set; }
    }
}
