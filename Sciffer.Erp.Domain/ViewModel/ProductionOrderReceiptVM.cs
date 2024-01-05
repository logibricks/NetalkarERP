namespace Sciffer.Erp.Domain.ViewModel
{
    public class ProductionOrderReceiptVM
    {
        public long rowIndex { get; set; }
        public int prod_order_detail_id { get; set; }
        public int batch_id { get; set; }
        public string batch_number { get; set; }
        public int out_item_id { get; set; }
        public int tag_id { get; set; }
        public string tag_no { get; set; }
        public double balance_qty { get; set; }
        public string out_item_name { get; set; }
        public int process_sequence_id { get; set; }
        public int task_machine_id { get; set; }
        public int process_id { get; set; }
        public string machine_id { get; set; }
        public int uom_id { get; set; }
        public string uom_name { get; set; }
        public int sloc_id { get; set; }
        public string sloc_name { get; set; }
        public int plant_id { get; set; }
        public int parent_sloc_id { get; set; }
        public int machine_qunatity { get; set; }

        public string root_cause_details { get; set; }
        public string nc_status_desc { get; set; }
        public string prod_order_no { get; set; }
    }
}
