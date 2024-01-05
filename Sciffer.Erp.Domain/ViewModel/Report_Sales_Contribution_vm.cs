using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_Sales_Contribution_vm
    {
        public string bill_to_party_code { get; set; }
        public string bill_to_party_description { get; set; }
        public string so_number { get; set; }
        public DateTime so_date { get; set; }
        public string Document_category { get; set; }
        public string document_no { get; set; }
        public DateTime posting_date { get; set; }
        public string currency { get; set; }
        public string plant { get; set; }
        public string item_code { get; set; }
        public string item_description { get; set; }
        public double qty { get; set; }
        public double unit_price { get; set; }
        public double discount { get; set; }
        public double effective_unit_price { get; set; }
        public double cost { get; set; }
        public string contribution { get; set; }
        public double sales_value { get; set; }
        public double total_cost { get; set; }
        public double total_contribution { get; set; }

    }
}
