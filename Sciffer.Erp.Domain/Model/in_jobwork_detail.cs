using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sciffer.Erp.Domain.ViewModel;

namespace Sciffer.Erp.Domain.Model
{
    public class in_jobwork_detail
    {
        [Key]
        public int job_work_detail_in_id { get; set; }
        
        public int job_work_in_id { get; set; }
        [ForeignKey("job_work_in_id")]
        public virtual in_jobwork_in job_work_in { get; set; }

        public int item_id { get; set; }
        [ForeignKey("item_id")]
        public virtual REF_ITEM REF_ITEM { get; set; }

        public int quantity { get; set; }

        public int uom_id { get; set; }
        [ForeignKey("uom_id")]
        public virtual REF_UOM REF_UOM { get; set; }

        public string batch { get; set; }

        public int sloc_id { get; set; }
        [ForeignKey("sloc_id")]
        public virtual REF_STORAGE_LOCATION REF_STORAGE_LOCATION { get; set; }

        public int bucket_id { get; set; }
        [ForeignKey("bucket_id")]
        public virtual ref_bucket ref_bucket { get;set;}
    }
}
