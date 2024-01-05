using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class inv_item_inventory
    {
        [Key]
        public int item_inventory_id { get; set; }
        public int item_id { get; set; }
        [ForeignKey("item_id")]
        public virtual REF_ITEM REF_ITEM { get; set; }
        public int plant_id { get; set; }
        [ForeignKey("plant_id")]
        public virtual REF_PLANT REF_PLANT { get; set; }
        public int sloc_id { get; set; }
        [ForeignKey("sloc_id")]
        public virtual REF_STORAGE_LOCATION REF_STORAGE_LOCATION { get; set; }
        public double op_stock_quality { get; set; }
        public double op_stock_free { get; set; }
        public double op_stock_blocked { get; set; }
        public double op_stock_transit { get; set; }
        public DateTime op_stock_ts { get; set; }
        public double cu_stock_quality { get; set; }
        public double cu_stock_free { get; set; }
        public double cu_stock_blocked { get; set; }
        public double cu_stock_transit { get; set; }
        public DateTime cu_stock_ts { get; set; }
        public int modify_user { get; set; }
       
    }
}
