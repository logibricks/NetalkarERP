using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_Sales_header_level_VM: Report_Sales_Contribution_vm
    {
        public string billing_city { get; set; }
        public string Priority { get; set; }
        public string control_account { get; set; }
        public string ship_to_party { get; set; }
        public string ship_to_party_description { get; set; }
        public string shipping_city { get; set; }
        public string business_unit { get; set; }
        public string customer_po_number { get; set; }
        public DateTime customer_po_dat { get; set; }
        public string territory { get; set; }
        public string payment_terms { get; set; }
        public double total_value { get; set; }
        public double total_taxes { get; set; }
        public double gross_value { get; set; }
        public double net_value { get; set; }
    }
}
