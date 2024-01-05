using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class update_stock_sheet_detail_vm
    {
        public int create_stock_sheet_detail_id { get; set; }
        public int? update_stock_count_id { get; set; }
        public int create_stock_sheet_id { get; set; }
        public int item_id { get; set; }
        public string item_code { get; set; }
        public int uom_id { get; set; }
        public string UOM { get; set; }
        public double? stock_qty { get; set; }
        public string batch_number { get; set; }
        public double? actual_qty { get; set; }
        public double? diff_qty { get; set; }
        public double? rate { get; set; }
        public double? value { get; set; }
        public bool new_batch { get; set; }
    }
}
