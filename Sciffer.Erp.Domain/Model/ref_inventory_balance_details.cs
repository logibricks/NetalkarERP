using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_inventory_balance_details
    {
        [Key]
        public int? inventory_balance_detail_id { get; set; }
        public int? inventory_balance_id { get; set; }
        [ForeignKey("inventory_balance_id")]
        public virtual ref_inventory_balance ref_inventory_balance { get; set; }
        public int item_id { get; set; }
        [ForeignKey("item_id")]
        public virtual REF_ITEM REF_ITEM { get; set; }
        public int plant_id { get; set; }
        [ForeignKey("plant_id")]
        public virtual REF_PLANT REF_PLANT { get; set; }
        public int sloc_id { get; set; }
        [ForeignKey("sloc_id")]
        public virtual REF_STORAGE_LOCATION REF_STORAGE_LOCATION { get; set; }
        public int bucket_id { get; set; }
        [ForeignKey("bucket_id")]
        public virtual ref_bucket ref_bucket { get; set; }
        public string batch { get; set; }
        public int qty { get; set; }
        public int uom_id { get; set; }
        [ForeignKey("uom_id")]
        public virtual REF_UOM REF_UOM { get; set; }
        public double rate { get; set; }
        public double value { get; set; }
        public string line_remarks { get; set; }
        public bool is_active { get; set; }
        

    }
}
