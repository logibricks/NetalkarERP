using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_sales_order_header_level_vm: Report_Sales_Contribution_vm
    {
        public string billing_city { get; set; }
        public string Priority { get; set; }
        public DateTime Delivery_Date { get; set; }
        public string ship_to_party { get; set; }
        public string ship_to_party_description { get; set; }
        public string shipping_city { get; set; }
        public string business_unit { get; set; }
        public string Sales_Quotation_Number { get; set; }
        public string customer_po_number { get; set; }
        public DateTime customer_po_date { get; set; }
        public string sales_RM { get; set; }
        public string Freight_terms { get; set; }
        public string sales_against_form { get; set; }
        public string territory { get; set; }
        public string payment_terms { get; set; }
        public double total_value { get; set; }
        public double total_taxes { get; set; }
        public double gross_value { get; set; }

    }
}
