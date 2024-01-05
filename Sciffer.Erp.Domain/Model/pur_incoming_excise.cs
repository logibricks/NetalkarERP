using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class pur_incoming_excise
    {
        [Key]
        public int incoming_excise_id { get; set; }
        public string incoming_excise_number { get; set; }
        public DateTime incoming_excise_posting_date { get; set; }
        public string excise_ref_no { get; set; }
        public DateTime excise_ref_date { get; set; }
        public int? vendor_id { get; set; }
        [ForeignKey("vendor_id")]
        public virtual REF_VENDOR REF_VENDOR { get; set; }
        public int category_id { get; set; }
        [ForeignKey("category_id")]
        public virtual ref_document_numbring ref_document_numbring { get; set; }
        public int? business_unit_id { get; set; }
        [ForeignKey("business_unit_id")]
        public virtual REF_BUSINESS_UNIT REF_BUSINESS_UNIT { get; set; }
        public int? plant_id { get; set; }
        [ForeignKey("plant_id")]
        public virtual REF_PLANT REF_PLANT { get; set; }
        public int? freight_terms_id { get; set; }
        [ForeignKey("freight_terms_id")]
        public virtual REF_FREIGHT_TERMS REF_FREIGHT_TERMS { get; set; }
        public DateTime? delivery_date { get; set; }
        public string vendor_doc_no { get; set; }
        public DateTime? vendor_doc_date { get; set; }
        public string gate_entry_number { get; set; }
        public DateTime? gate_entry_date { get; set; }
        public int? created_by { get; set; }
        public string billing_address { get; set; }
        public string billing_city { get; set; }
        public int? billing_state_id { get; set; }
        [ForeignKey("billing_state_id")]
        public virtual REF_STATE REF_STATE { get; set; }
        public string billing_pin_code { get; set; }
        public int? payment_terms_id { get; set; }
        [ForeignKey("payment_terms_id")]
        public virtual REF_PAYMENT_TERMS REF_PAYMENT_TERMS { get; set; }
        public int? payment_cycle_id { get; set; }
        [ForeignKey("payment_cycle_id")]
        public virtual REF_PAYMENT_CYCLE REF_PAYMENT_CYCLE { get; set; }
        public bool is_active { get; set; }
        public int? grn_id { get; set; }
        [ForeignKey("grn_id")]
        public virtual pur_grn pur_grn { get; set; }
        public double incoming_excise_net_value { get; set; }
        public int? net_currency_id { get; set; }
        [ForeignKey("net_currency_id")]
        public virtual REF_CURRENCY REF_CURRENCY_net { get; set; }
        public double? incoming_excise_gross_value { get; set; }
        public int? gross_currency_id { get; set; }
        [ForeignKey("gross_currency_id")]
        public virtual REF_CURRENCY REF_CURRENCY_gross { get; set; }
        public string pan_no { get; set; }
        public string ecc_no { get; set; }
        public string vat_tin_no { get; set; }
        public string cst_tin_no { get; set; }
        public string service_tax_no { get; set; }
        public string gst_no { get; set; }
        public string email_id { get; set; }
        public virtual ICollection<pur_incoming_excise_detail> pur_incoming_excise_detail { get; set; }
        public string internal_remarks { get; set; }
        public string remarks_on_doc { get; set; }
        public string attachment { get; set; }
        public string cancellation_remarks { get; set; }
        public int? cancellation_reason_id { get; set; }
        public int? status_id { get; set; }
        public virtual ref_status ref_status { get; set; }
        public DateTime created_ts { get; set; }
        public int? modify_by { get; set; }
        public DateTime? modify_ts { get; set; }
        public int? cancelled_by { get; set; }
        public DateTime? cancelled_date { get; set; }
    }
}
