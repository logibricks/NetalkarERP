using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class goods_issue_detail
    {
        [Key]
        public int goods_issue_detail_id { get; set; }

        public int? goods_issue_id { get; set; }
        [ForeignKey("goods_issue_id")] 
        public virtual goods_issue GOODS_ISSUE { get; set; }

        [Display(Name ="Item ID")]
        public int item_id { get; set; }
        [ForeignKey("item_id")]
        public virtual REF_ITEM REF_ITEM { get; set; }   
        
        public int uom_id { get; set; }
        [ForeignKey("uom_id")]
        public virtual REF_UOM REF_UOM { get; set; }

        public int sloc_id { get; set; }
        [ForeignKey("sloc_id")]
        public virtual REF_STORAGE_LOCATION REF_STORAGE_LOCATION { get; set; }

        [Display(Name = "Bucket")]
        public int bucket_id { get; set; }
        [ForeignKey("bucket_id")]
        public virtual ref_bucket ref_bucket { get; set; }

        [Display(Name = "Quantity")]
        public double quantity { get; set; }

        [Display(Name = "Rate")]
        public double rate { get; set; }

        [Display(Name = "Value")]
        public double value { get; set; }

        public int document_detail_id { get; set; }

        public bool is_active { get; set; }        

        [Display(Name ="Reason")]
        public int reason_id { get; set; }
        [ForeignKey("reason_id")]
        public virtual REF_REASON_DETERMINATION REF_REASON_DETERMINATION { get; set; }
       
        public int? machine_id { get; set; }
        [ForeignKey("machine_id")]
        public virtual ref_machine ref_machine { get; set; }        
    }

    public class goods_issue_detail_VM
    {
        public int goods_issue_detail_id { get; set; }
        public int? goods_issue_id { get; set; }
        public int item_id { get; set; }
        public int uom_id { get; set; }
        public int sloc_id { get; set; }
        public int bucket_id { get; set; }
        public double quantity { get; set; }
        public double rate { get; set; }
        public double value { get; set; }
        public int document_detail_id { get; set; }
        public bool is_active { get; set; }
        public int reason_id { get; set; }
        public int? machine_id { get; set; }
        public string header_referenec { get; set; }
    }
}
