using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class pur_srn
    {
        [Key]
        public int srn_id { get; set; }
        public int po_id { get; set; }
        public string document_no { get; set; }
        public int vendor_id { get; set; }
        [ForeignKey("vendor_id")]
        public virtual REF_VENDOR REF_VENDOR { get; set; }
        public double net_value { get; set; }
        public int category_id { get; set; }
        public int net_currency_id { get; set; }
        public double gross_value { get; set; }
        public int gross_currency_id { get; set; }
        [DataType(DataType.Date)]
        public DateTime posting_date { get; set; }
        public int business_unit_id { get; set; }
        public int plant_id { get; set; }
        public int freight_terms_id { get; set; }
        [DataType(DataType.Date)]
        public DateTime? delivery_date { get; set; }
        public string vendor_doc_no { get; set; }
        [DataType(DataType.Date)]
        public DateTime? vendor_doc_date { get; set; }
        public string gate_entry_number { get; set; }
        [DataType(DataType.Date)]
        public DateTime? gate_entry_date { get; set; }
        public int created_by { get; set; }
        public string billing_address { get; set; }
        public string billing_city { get; set; }
        public int billing_state_id { get; set; }
        public string billing_pin_code { get; set; }
        public string email_id { get; set; }
        public int payment_terms_id { get; set; }
        public int payment_cycle_id { get; set; }
        public int? status_id { get; set; }
        public string internal_remarks { get; set; }
        public string remarks_on_doc { get; set; }
        public string attachment { get; set; }
        public bool is_active { get; set; }
        public string pan_no { get; set; }
        public string ecc_no { get; set; }
        public string vat_tin_no { get; set; }
        public string cst_tin_no { get; set; }
        public string service_tax_no { get; set; }
        public string gst_no { get; set; }
        public bool order_status { get; set; }
        public DateTime? created_ts { get; set; }
        public int place_of_supply_id { get; set; }
        public int gst_vendor_type_id { get; set; }
        public string gst_in { get; set; }
        public string cancellation_remarks { get; set; }
        public int? cancellation_reason_id { get; set; }
        public DateTime? cancelled_date { get; set; }
        public int? cancelled_by { get; set; }
        public int? modify_by { get; set; }
        public DateTime? modify_ts { get; set; }
    }
}
