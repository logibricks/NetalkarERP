using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class create_stock_sheet_detail
    {
        [Key]
        public int create_stock_sheet_detail_id { get; set; } 
        public int? update_stock_count_id { get; set; }
        public int create_stock_sheet_id { get; set; }
        public int item_id { get; set; }
        [ForeignKey("item_id")]
        public virtual REF_ITEM REF_ITEM { get; set; }
        public int uom_id { get; set; }
        [ForeignKey("uom_id")]
        public virtual REF_UOM REF_UOM { get; set; }
        public double? stock_qty { get; set; }
        public string batch_number { get; set; }
        public double? actual_qty { get; set; }
        public double? diff_qty { get; set; }
        public double? rate { get; set; }
        public double? value { get; set; }
        public bool new_batch { get; set; }
    }
}
