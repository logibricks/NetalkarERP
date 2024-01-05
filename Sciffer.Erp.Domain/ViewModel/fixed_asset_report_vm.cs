using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class fixed_asset_report_vm
    {
        public string asset_class { get; set; }
        public string asset_group { get; set; }
        public decimal? rate_of_depreciation { get; set; }
        public decimal? group_of_opening { get; set; }
        public decimal? group_of_addition { get; set; }
        public decimal? group_of_deletion { get; set; }
        public decimal? group_of_closing { get; set; }

        public decimal? depreciation_opening { get; set; }
        public string depreciation_for_the_year { get; set; }
        public decimal? depreciation_disposal_adjustment { get; set; }
        public decimal? depreciation_closing { get; set; }
        public decimal? net_wdv { get; set; }

        //Block
        public decimal? opening_wdv { get; set; }
        public decimal? sales_proceed { get; set; }
        public decimal? addition_used_more_than_eighty_day { get; set; }
        public decimal? addition_used_less_than_eighty_day { get; set; }
        public decimal? total { get; set; }
        public decimal? depreciation_on_opening_wdv { get; set; }
        public decimal? depreciation_on_addition_used_for_one_eighty_or_more { get; set; }
        public decimal? depreciation_on_addition_used_for_one_eighty_or_less { get; set; }
        public decimal? additional_depreciation { get; set; }
        public decimal? total_depreciation { get; set; }
        public decimal? closing_wdv { get; set; }

        //Asset Ledger
        public string TransactionName { get; set; }
        public DateTime? PostingDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal? amount { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]

        public decimal? closingValue { get; set; }
        public string remark { get; set; }
        
        //list of Additons
        public string asset_code { get; set; }
        //public string asset_class { get; set; }
        //public string asset_group { get; set; }
        public DateTime? capitization_date { get; set; }
        public string asset_capitalisation_document_no { get; set; }
        public DateTime? asset_capitalisation_document_date { get; set; }
        
        //list of deletion report
        public decimal? original_cost { get; set; }
        public decimal? accu_depr_at_sale { get; set; }
       // public decimal? net_wdv { get; set; }
        public decimal? sale_value { get; set; }
        public DateTime? sale_date { get; set; }
        public string profit_loss { get; set; }
    }
}
