using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_sales_Order_Detail_level_vm: Report_Sales_Contribution_vm
    {
        public string billing_city { get; set; }
        public string ship_to_party { get; set; }
        public string ship_to_party_description { get; set; }
        public string shipping_city { get; set; }
        public string customer_po_number { get; set; }
        public DateTime customer_po_date { get; set; }
        public string Sloc { get; set; }
        public DateTime Delivery_Date { get; set; }
        public double assessable_rate { get; set; }
        public double assesssabl_evaue { get; set; }
        public string tax_code { get; set; }
        public string tax_descriptiption { get; set; }
        public string tax_element_1 { get; set; }
        public string tax_element_2 { get; set; }
        public string tax_element_3 { get; set; }
        public double total_taxes { get; set; }
        public double net_total { get; set; }

    }
}
