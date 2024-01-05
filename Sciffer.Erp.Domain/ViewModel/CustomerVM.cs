using System;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class CustomerVM
    {
        public int customer_id { get; set; }
        public string customer_parent { get; set; }
        public string customer_name { get; set; }
        public string customer_code { get; set; }
        public string customer_display_name { get; set; }
        public string org_type { get; set; }
        public string priority { get; set; }
        public string territory { get; set; }
        public string billing_address { get; set; }
        public string billing_city { get; set; }
        public int? billing_pincode { get; set; }
        public string std_code { get; set; }
        public string billing_state { get; set; }
        public string billing_country { get; set; }
        public string core_address { get; set; }
        public string core_city { get; set; }
        public int? core_pincode { get; set; }
        public string core_state { get; set; }
        public string core_country { get; set; }
        public string primary_email { get; set; }
        public string telephone { get; set; }
        public string fax { get; set; }
        public string payment_terms { get; set; }
        public double? credit_limit { get; set; }
        public string payment_cycle_type { get; set; }
        public string payment_cycle { get; set; }
        public string freight_terms { get; set; }
        public string website { get; set; }
        public string pan_no { get; set; }
        public string ecc_no { get; set; }
        public string vat_tin_no { get; set; }
        public string cst_tin_no { get; set; }
        public string service_tax_no { get; set; }
        public string gst_no { get; set; }
        public int? tds_id { get; set; }
        public string attachment { get; set; }
        public string down_payment_account_name { get; set; }
        public string bank_name { get; set; }
        public string bank_account_number { get; set; }
        public string ifsc_code { get; set; }
        public string commisionerate { get; set; }
        public string range { get; set; }
        public string division { get; set; }
        public string category_name { get; set; }
        public string sales_rm_code { get; set; }
        public string sales_rm_name { get; set; }
        public bool has_parent { get; set; }
        public bool blocked { get; set; }
        public string telephone_code { get; set; }
        public string default_currency { get; set; }
        public string aditional_info { get; set; }
        public string tds_no { get; set; }
        public string vendor_code { get; set; }
        public DateTime? gst_registration_date { get; set; }
        public string gst_customer_type_name { get; set; }
        public string gst_tds_code { get; set; }
        public int? gst_customer_type_id { get; set; }
        public bool gst_tds_applicable { get; set; }
        public int? gst_tds_id { get; set; }
        public int payment_terms_id { get; set; }
        public int payment_cycle_id { get; set; }
        public int billing_state_id { get; set; }
        public int payment_cycle_type_id { get; set; }
        public int billing_country_id { get; set; }
        public int default_currency_id { get; set; }
    }
}
