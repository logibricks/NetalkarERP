using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class SubProdOrderDetailVM
    {
        public int sub_prod_order_detail_id { get; set; }
        public Int64 rowIndex { get; set; }
        public int item_id { get; set; }
        public string item_name { get; set; }
        public int batch_id { get; set; }
        public string batch_number { get; set; }
        public int? sloc_id { get; set; }
        public string sloc_name { get; set; }
        public int? bucket_id { get; set; }
        public string bucket_name { get; set; }
        public int gl_id { get; set; }
        public string gl_name { get; set; }
        public double sub_po_quantity { get; set; }
        public string sub_po_quantityStr { get; set; }
        public double rate { get; set; }
        public double? value { get; set; }
        public int sub_prod_order_id { get; set; }
        public int batch_detail_id { get; set; }
        public int uom_id { get; set; }
        public string uom_name { get; set; }
        public double? batch_quantity { get; set; }
        public string batch_quantityStr { get; set; }
        public string emtpy1 { get; set; }
        public string emtpy2 { get; set; }
        public string emtpy3 { get; set; }
        public int item_batch_detail_id { get; set; }
        public int document_id { get; set; }
        public int document_detail_id { get; set; }
        public int tag_id { get; set; }
        public string tag_no { get; set; }
        public string rack_no { get; set; }
        public string machine_code { get; set; }
        public string REASON_DETERMINATION_NAME { get; set; }
        public int? machine_id { get; set; }
        public int? REASON_DETERMINATION_ID { get; set; }
        public int? parent_child_id { get; set; }
        public string parent_child_name { get; set; }
        public int? child_item_id { get; set; }
        public string child_item_name { get; set; }
        public string add1 { get; set; }
        public int? c_item_id { get; set; }
        public string c_item_name { get; set; }
        public double? remaining_qty { get; set; }
    }
}
