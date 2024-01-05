using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class accounts_report_vm
    {
        public double? total_contribution { get; set; }
        public double? total_cost { get; set; }
        public double? sales_value { get; set; }
        public double? contribution { get; set; }
        public double? item_basic_value { get; set; }
        public double? effective_unit_price { get; set; }
        public double? discount { get; set; }
        public double? unit_price { get; set; }
        public double? quantity { get; set; }
        public string item_name { get; set; }
        public string item_code { get; set; }
        public string plant_name { get; set; }
        public decimal? tcs_amount { get; set; }
        public string currency_name { get; set; }
        public DateTime si_date { get; set; }
        public string si_number { get; set; }
        public DateTime? so_date { get; set; }
        public string so_number { get; set; }
        public string employee_name { get; set; }
        public string employee_code { get; set; }
        public string gl_ledger_name { get; set; }
        public string gl_account_type { get; set; }
        public string customer_name { get; set; }
        public string customer_code { get; set; }
        public decimal? Closing_Bal { get; set; }
        public decimal? cr { get; set; }
        public decimal? dr { get; set; }
        public decimal? Opening_Bal { get; set; }
        public string VENDOR_NAME { get; set; }
        public string VENDOR_CODE { get; set; }
        public double? tds_amount { get; set; }
        public string org_type_name { get; set; }
        public string pan_no { get; set; }
        public string tds_code_description { get; set; }
        public string tds_code { get; set; }
        public int entity_id { get; set; }
        public string entity_code { get; set; }
        public string entity_name { get; set; }
        public int? parent_id { get; set; }
        public string document_type_name { get; set; }
        public string source_document_no { get; set; }
        public DateTime? posting_date { get; set; }
        public int? day_diff { get; set; }
        public double? not_due { get; set; }
        public double? thirty_days { get; set; }
        public double? sixty_days { get; set; }
        public double? ninety_days { get; set; }
        public double? one_twenty_days { get; set; }
        public double? one_fifty_days { get; set; }
        public double? one_eighty_days { get; set; }
        public double? sixty_five_days { get; set; }
        public double? three_sixty_five_days { get; set; }
        public double? total { get; set; }
        public double? total_due { get; set; }
        public string category { get; set; }
        public string pr_doc_category { get; set; }
        public DateTime? ledger_date { get; set; }
        public DateTime? due_date { get; set; }
        public DateTime? document_date { get; set; }
        public string narration { get; set; }
        public string ref1 { get; set; }
        public string ref2 { get; set; }
        public string ref3 { get; set; }
        public double? credit { get; set; }
        public double? debit { get; set; }
        public double? credit_fr { get; set; }
        public double? debit_fr { get; set; }
        public string document_no { get; set; }
        public string currency { get; set; }
        public string status_name { get; set; }
        public string gl_ledger_code { get; set; }
        public string document_category { get; set; }
        public string doc_number { get; set; }
        public string source_doc_type { get; set; }
        public string source_doc_no { get; set; }
        public string description { get; set; }
        public double? dr_lc { get; set; }
        public double? cr_lc { get; set; }
        public double? dr_fc { get; set; }
        public double? cr_fc { get; set; }
        public double? balance { get; set; }
        public string cost_center { get; set; }
        public string line_remarks { get; set; }
    }
}
