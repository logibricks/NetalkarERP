namespace Sciffer.Erp.Domain.ViewModel
{
    public class pur_pi_return_detail_vm
    {
        public int? pi_return_detail_id { get; set; }
        public int? pi_id { get; set; }
        public int? pi_detail_id { get; set; }
        public string document_no { get; set; }
        public int item_id { get; set; }
        public string item_name { get; set; }
        public decimal quantity { get; set; }
        public int uom_id { get; set; }
        public string uom_name { get; set; }
        public double unit_price { get; set; }
        public decimal discount { get; set; }
        public double eff_unit_price { get; set; }
        public decimal purchase_value { get; set; }
        public double assessable_rate { get; set; }
        public double assessable_value { get; set; }
        public int tax_id { get; set; }
        public string tax_name { get; set; }
        public int storage_location_id { get; set; }
        public string storage_location_name { get; set; }
        public double? grir_value { get; set; }
        public string delivery_date { get; set; }
        public double balance_quantity { get; set; }
        public double? basic_value { get; set; }
        public int? cost_center_id { get; set; }
        public string cost_center_code { get; set; }
        public int rowIndex { get; set; }
        public string emptyQuantity { get; set; }
        public string emptyBucketId { get; set; }
        public string emptyBucketName { get; set; }
        public int plant_id { get; set; }
        public int bucket_id { get; set; }
        public string bucket_name { get; set; }
        public decimal stock_quantity { get; set; }
        public int sac_hsn_id { get; set; }
        public string hsn_code { get; set; }
        public int batch_id { get; set; }
        public string batch_number { get; set; }
        public int? pi_return_id { get; set; }
        public string tax_type_name { get; set; }
        public decimal tax_element_value { get; set; }
        public decimal tax_element_rate { get; set; }
        public string tax_element_code { get; set; }
        public decimal cgst { get; set; }
        public decimal sgst { get; set; }
        public decimal igst { get; set; }
        public string hsn_sac_code { get; set; }

        public decimal eff_price { get; set; }

    }
}
