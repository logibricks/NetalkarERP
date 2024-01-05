using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class gstr_report_vm
    {
        public string document_type { get; set; }
        public string category { get; set; }
        public string document_no { get; set; }
        public DateTime? document_date { get; set; }
        public string plant_code { get; set; }
        public string plant_name { get; set; }
        public string plant_gst_number { get; set; }
        public string gst_customer_type_name { get; set; }
        public string party_gstin { get; set; }
        public string state_ut_code { get; set; }
        public string state_name { get; set; }
        public string is_rcm { get; set; }
        public string party_doc_no { get; set; }
        public DateTime? party_doc_date { get; set; }
        public string shipping_bill_no { get; set; }
        public DateTime? shipping_bill_date { get; set; }
        public string tax_code { get; set; }
        public string tax_name { get; set; }
        public string original_document_no { get; set; }
        public DateTime? original_document_date { get; set; }
        public string original_shipping_bill_no { get; set; }
        public DateTime? original_shipping_bill_date { get; set; }
        public double? IGST_rate { get; set; }
        public double? IGST { get; set; }
        public double? SGST_rate { get; set; }
        public double? SGST { get; set; }
        public double? CGST_rate { get; set; }
        public double? CGST { get; set; }
        public double? Cess { get; set; }
        public string entity_code { get; set; }
        public string entity_name { get; set; }
        public double? taxable_amount { get; set; }
        public string sac_hsn_code { get; set; }
        public double? net_amount { get; set; }
        public double? gross_amount { get; set; }
        public string shipping_port_no { get; set; }
        public double? quantity { get; set; }
        public string item_name { get; set; }
        public double? round_off { get; set; }
        public string source_document_no { get; set; }
        public decimal? tcs_amount { get; set; }
    }
}
