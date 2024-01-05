using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_sales_Quotation_header_level_VM: Report_Sales_detail_level_VM
    {
        public string Priority { get; set; }
        public string business_unit { get; set; }
        public string customer_RFQ { get; set; }
        public DateTime customer_RFQ_Date { get; set; }
        public DateTime Quotation_Expiry_Date { get; set; }
        public string sales_RM { get; set; }
        public string Freight_terms { get; set; }
        public string sales_against_form { get; set; }
        public string territory { get; set; }
        public string payment_terms { get; set; }
        public double total_value { get; set; }
        public double gross_value { get; set; }

    }
}
