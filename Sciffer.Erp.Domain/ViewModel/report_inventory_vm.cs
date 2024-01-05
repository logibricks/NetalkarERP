using System;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class report_inventory_vm
    {
        public string item_code { get; set; }
        public string item_name { get; set; }
        public string plant_code { get; set; }
        public string plant_name { get; set; }
        public string rack_no { get; set; }
        public string storage_location_name { get; set; }
        public string storage_location_name1 { get; set; }
        public string bucket_name { get; set; }
        public string bucket_name1 { get; set; }
        public string batch_number { get; set; }
        public double? quantity { get; set; }
        public string uom_name { get; set; }
        public string category { get; set; }
        public string document_no { get; set; }
        public DateTime? posting_date { get; set; }
        public string header_remarks { get; set; }
        public string reason_determination_name { get; set; }
        public double? rate { get; set; }
        public double? value { get; set; }
        public string gl_ledger_name { get; set; }
        public string order_number { get; set; }
        public string machine_name { get; set; }
        public DateTime? document_date { get; set; }
        public double? differential_value { get; set; }
        public double? differential_rate { get; set; }
        public double? new_rate { get; set; }
        public double? old_rate { get; set; }
        public string mrn_type { get; set; }
        public double? required_qty { get; set; }
        public double? balance_quantity { get; set; }
        public string status_name { get; set; }
        public int? day_diff { get; set; }
        public string item_valuation_name { get; set; }
        public string gl_ledger_code { get; set; }
        public string transaction_type_code { get; set; }
        public string issue_type { get; set; }
        public string ref_document_number { get; set; }
        public string plant { get; set; }
        public string Sloc { get; set; }
        public double? Receipts { get; set; }
        public double? Issue { get; set; }
        public double? Opening { get; set; }
        public double? Closing { get; set; }
        public string Item { get; set; }
        public string document_type_code { get; set; }
        public string Doc_Category { get; set; }
        public string Doc_Number { get; set; }
      
        public DateTime? System_Date { get; set; }
       
        public double? item_quantity { get; set; }
        public double? Balance { get; set; }
        public string item_type_name { get; set; }
        public string item_category_name { get; set; }
        public string item_group_name { get; set; }
        //public string item_valuation_name { get; set; }
        public string item_accounting_name { get; set; }
        public string Inventory_Account { get; set; }
        public string Consumption_Account { get; set; }
        public string Revaluation_Account { get; set; }
        public string COGP_Account { get; set; }
        public string COGS_Account { get; set; }
        public string Scrap_P_and_L_Account { get; set; }
        public string Price_Difference_Account { get; set; }
        public string Stock_Differences_Account { get; set; }
        public string By_Product_P_and_L_Account { get; set; }
        public string GRIR_Clearing_Account { get; set; }
        public string Sales_GL { get; set; }
        public string Sales_Return_GL { get; set; }
        public string Costing_Difference { get; set; }


        public double? document_qty { get; set; }
        public string customer_chalan_no { get; set; }
        public DateTime? customer_chalan_date { get; set; }
        public int? sr_no { get; set; }

        public double? item_value { get; set; }
        public double? itm_value { get; set; }
        public string Item_category { get; set; }
        public string Operation { get; set; }
        public string Machine { get; set; }
        public string UOM { get; set; }
        public string Name { get; set; }
        public string From_Range { get; set; }
        public string To_Range { get; set; }
    }
}
