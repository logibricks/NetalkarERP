using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class report_material_in_out_vm
    {
        public string category { get; set; }
        public string document_number { get; set; }
        
        public DateTime? posting_date { get; set; }
        public string VENDOR_NAME { get; set; }
        public string BUSINESS_UNIT_NAME { get; set; }
        public string PLANT_NAME { get; set; }
        public string ge_number { get; set; }
        public DateTime? ge_date { get; set; }
        public string employee_name { get; set; }
        public string returnable_nonreturnable { get; set; }
        public string ITEM_NAME { get; set; }
        public string user_description { get; set; }
        public string UOM_NAME { get; set; }
        public double? quantity { get; set; }
        public double? rate { get; set; }
        public double? value { get; set; }
        public string reason { get; set; }
        public DateTime? er_date { get; set; }
        public double? balance_qty { get; set; }
        public string remarks { get; set; }
        public string internal_remarks  { get; set; }
        public string remarks_on_document { get; set; }

        public string mo_doc { get; set; }
        public string mo_vendor_name { get; set; }
        public DateTime? mo_date { get; set; }
        public string mo_gst { get; set; }
        public string mo_item { get; set; }
        public string mo_uom { get; set; }
        public string mo_hsn { get; set; }
        public double? mo_qty { get; set; }
        public double? mo_rate { get; set; }
        public string igst_text { get; set; }
        public string cgst_text { get; set; }
        public string sgst_text { get; set; }
        public double? mo_value { get; set; }
        public string mi_doc_number { get; set; }        
        public DateTime? mi_posting_date { get; set; }
        public double? rec_qty { get; set; }
        public string mi_uom { get; set; }
        public double? mi_rate { get; set; }
        public string mi_igst_text { get; set; }
        public string mi_cgst_text { get; set; }
        public string mi_sgst_text { get; set; }
        public double? mi_value { get; set; }
        public string vendor_invoice_no { get; set; }
        public DateTime? vendor_invoice_date { get; set; }
        public double? mi_balance_qty { get; set; }
        public string hsn_code { get; set; }
        public string tax_code { get; set; }
        public int? hsn_id { get; set; }
        public int? tax_id { get; set; }
        public int? ageing { get; set; }

        public DateTime mo_posting_date { get; set; }
        public string gst_no { get; set; }
        public decimal? gross_value { get; set; }
    }
}
