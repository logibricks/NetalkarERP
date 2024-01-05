using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class inv_Inventory_stock_detail
    {
        [Key]
        public int inventory_stock_detail_id { get; set; }
        public int inventory_stock_id { get; set; }
        [ForeignKey("inventory_stock_id")]
        public virtual inv_Inventory_stock inv_Inventory_stock { get; set; }
        public int item_id { get; set; }
        public string item_code { get; set; }
        //[ForeignKey("item_id")]
        //public virtual REF_ITEM REF_ITEM { get; set; }
        public int item_batch_id { get; set; }
        public string batch_number { get; set; }
        public int uom_id { get; set; }
        public string UOM_NAME { get; set; }
        //[ForeignKey("uom_id")]
        //public virtual REF_UOM REF_UOM { get; set; }
        public double actual_qty { get; set; }
        public int rowIndex1 { get; set; }
    }
}
