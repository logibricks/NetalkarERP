using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class fin_credit_debit_note
    {

        [Key]
        public int fin_credit_debit_node_id { get; set; }
        public int category_id { get; set; }
        [ForeignKey("category_id")]
        public virtual ref_document_numbring ref_document_numbring { get; set; }
        public string document_no { get; set; }
        public int credit_debit_id { get; set; }
        public int entity_type_id { get; set; }
        public int entity_id { get; set; }
        public DateTime posting_date { get; set; }
        public int currency_id { get; set; }
        [ForeignKey("currency_id")]
        public virtual REF_CURRENCY REF_CURRENCY { get; set; }
        public string remarks { get; set; }
        public int created_by { get; set; }
        public DateTime created_ts { get; set; }
        public int? plant_id { get; set; }
        public bool is_rcm { get; set; }
        public double? net_value { get; set; }
        public double? gross_value { get; set; }
        public int? business_unit_id { get; set; }
        public int? payment_terms_id { get; set; }
        public string billing_address { get; set; }
        public string billing_city { get; set; }
        public string billing_pin_code { get; set; }
        public int? billing_state_id { get; set; }
        public int? billing_country_id { get; set; }
        public string pan_no { get; set; }
        public string gstin_no { get; set; }
        public int? payment_cycle_id { get; set; }
        public int? payment_cycle_type_id { get; set; }
        public string internal_remarks { get; set; }
        public string remarks_on_document { get; set; }
        public string attachement { get; set; }
        public string email_id { get; set; }
        public int? gst_category_id { get; set; }
        public decimal? round_off { get; set; }
        public int? tds_code_id { get; set; }
        public int? cancellation_reason_id { get; set; }
        public string cancellation_remarks { get; set; }
        public int? cancelled_by { get; set; }
        public DateTime? cancellation_ts { get; set; }
        public virtual ICollection<fin_credit_debit_note_detail> fin_credit_debit_node_detail { get; set; }
    }
}
