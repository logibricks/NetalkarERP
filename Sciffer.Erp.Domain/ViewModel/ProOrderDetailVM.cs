using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class ProOrderDetailVM
    {
        public int prod_order_detail_id { get; set; }
        public int item_batch_detail_id { get; set; }
        public int batch_id { get; set; }
        public string batch_number { get; set; }
        public int? tag_id { get; set; }
        public string tag_no { get; set; }
        public int in_item_id { get; set; }
        public string in_item_name { get; set; }        
        public double tag_qty { get; set; }
        public double item_value { get; set; }
        public double? tag_bal_qty { get; set; }
        public double batch_bal_qty { get; set; }
        public double batch_qty { get; set; }
        public int item_category_id { get; set; }
        public string item_category_name { get; set; }
        public double prod_bal_qty { get; set; }
        public Int64 rowIndex { get; set; }
    }
}
