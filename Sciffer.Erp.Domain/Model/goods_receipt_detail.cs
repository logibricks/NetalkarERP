using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class goods_receipt_detail
    {
        [Key]
        public int goods_detail_id { get; set; }
        public int sr_no { get; set; }
        public int? goods_receipt_id { get; set; }
        [ForeignKey("goods_receipt_id")]
        public virtual goods_receipt goods_receipt { get; set; }
        [Display(Name = "Item ID")]
        public int item_id { get; set; }
        [ForeignKey("item_id")]
        public virtual REF_ITEM REF_ITEM { get; set; }

        public int storage_location_id { get; set; }
        [ForeignKey("storage_location_id")]
        public virtual REF_STORAGE_LOCATION REF_STORAGE_LOCATION { get; set; }

        [Display(Name = "Bucket")]
        public int bucket_id { get; set; }
        [ForeignKey("bucket_id")]
        public virtual ref_bucket ref_bucket { get; set; }
        
        public bool batch_yes_no { get; set; }
        public string batch_manual { get; set; }

        [Display(Name = "Reason")]
        public int reason_determination_id { get; set; }
        [ForeignKey("reason_determination_id")]
        public virtual REF_REASON_DETERMINATION REF_REASON_DETERMINATION { get; set; }

        [Display(Name = "Batch")]
        public int item_batch_id { get; set; }
        [ForeignKey("item_batch_id")]
        public virtual inv_item_batch inv_item_batch { get; set; }

        [Display(Name = "General Ledger Code")]// To be reviewed what is to be written in display
        public int general_ledger_id { get; set; }
        [ForeignKey("general_ledger_id")]
        public virtual ref_general_ledger REF_GENERAL_LEDGER { get; set; }

        [Display(Name = "Quantity")]
        public double quantity { get; set; }

        public int uom_id { get; set; }
        [ForeignKey("uom_id")]
        public virtual REF_UOM REF_UOM { get; set; }

        [Display(Name = "Rate")]
        public double rate { get; set; }

        [Display(Name = "Value")]
        public double value { get; set; }

        public string remark { get; set; }
        public bool is_active { get; set; }
        public int? grn_id { get; set; }
        [ForeignKey("grn_id")]
        public virtual pur_grn pur_grn { get; set; }
    }
    public class goods_receipt_detail_VM
    {
        public int goods_detail_id { get; set; }
        public int sr_no { get; set; }
        public int? goods_receipt_id { get; set; }
        public int item_id { get; set; }
        public int storage_location_id { get; set; }
        public int bucket_id { get; set; }
        public bool batch_yes_no { get; set; }
        public string batch_manual { get; set; }
        public int reason_determination_id { get; set; }
        public int item_batch_id { get; set; }
        public int general_ledger_id { get; set; }
        public double quantity { get; set; }
        public int uom_id { get; set; }
        public double rate { get; set; }
        public double value { get; set; }
        public string remark { get; set; }
        public bool is_active { get; set; }
        public string batch_number { get; set; }
    }
}
