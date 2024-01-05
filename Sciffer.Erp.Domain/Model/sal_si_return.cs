using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
   public class sal_si_return
    {
        [Key]
        public int sales_return_id  { get; set; }
        public int si_id { get; set; }
        public int category_id { get; set; }
        public DateTime posting_date { get; set; }
        public int buyer_id { get; set; }
        public int consignee_id { get; set; }
        public double net_value { get; set; }
        public double gross_value { get; set; }
        public int net_currency_id { get; set; }
        public int gross_currency_id { get; set; }
        public int business_unit_id { get; set; }
        public int plant_id { get; set; }
        public int? territory_id { get; set; }
        public int? sales_rm_id { get; set; }
        public string billing_address { get; set; }
        public string billing_city { get; set; }
        public string billing_pincode { get; set; }
        public int billing_state_id { get; set; }
        public string billing_email_id { get; set; }
        public string shipping_address { get; set; }
        public string shipping_city { get; set; }
        public string shipping_pincode { get; set; }
        public int shipping_state_id { get; set; }
        public int payment_cycle_id { get; set; }
        public int payment_terms_id { get; set; }
        public double? credit_avail_after_invoice { get; set; }
        public int? tds_code_id { get; set; }
        public int? gst_tds_code_id { get; set; }
        public double? available_credit_limit { get; set; }
        public string internal_remarks { get; set; }
        public string remarks_on_document { get; set; }
        public string attachment { get; set; }
        public string gate_entry_no { get; set; }
        public DateTime? gate_entry_date { get; set; }
        public string invoice_number { get; set; }
        public string reason_for_return { get; set; }
        public string return_number { get; set; }
        public DateTime? invoice_date { get; set; }      
        public double? round_off { get; set; }
        public double? net_value_local { get; set; }
        public double? gross_value_local { get; set; }
        public virtual ICollection<sal_si_return_detail> sal_si_return_detail { get; set; }
    }
}
