using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_Forex_Sales__VM
    {
        public string bill_to_party_code { get; set; }
        public string bill_to_party_description { get; set; }
        public string billing_city { get; set; }
        public string billing_Country { get; set; }
        public string Priority { get; set; }
        public string control_account { get; set; }
        public string ship_to_party { get; set; }
        public string ship_to_party_description { get; set; }
        public string shipping_city { get; set; }
        public string shipping_COuntry { get; set; }
        public string business_unit { get; set; }
        public string plant { get; set; }
        public string so_number { get; set; }
        public DateTime so_date { get; set; }
        public string Document_category { get; set; }
        public string document_no { get; set; }
        public DateTime posting_date { get; set; }
        public string customer_po_number { get; set; }
        public DateTime customer_po_dat { get; set; }
        public string sales_RM { get; set; }
        public string Freight_terms { get; set; }
        public string sales_against_form { get; set; }
        public string territory { get; set; }
        public string payment_terms { get; set; }
        public string currency { get; set; }
        public string Forex_rate { get; set; }
        public double AMount_in_Doc_Currency { get; set; }
        public double Amount_in_Local_Currency { get; set; }

    }
}
