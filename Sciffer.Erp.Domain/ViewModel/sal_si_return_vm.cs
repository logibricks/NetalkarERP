using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sciffer.Erp.Domain.ViewModel
{
    public partial class sal_si_return_vm
    {
        public int sales_return_id { get; set; }
        [Display(Name = "Category *")]
        public int category_id { get; set; }
        public string category_name { get; set; }
        [Display(Name = "Posting Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime posting_date { get; set; }
        [Display(Name = "Bill to party *")]
        public int buyer_id { get; set; }
        public string buyer_name { get; set; }
        public string buyer_code { get; set; }
        [Display(Name = "Ship to Party *")]
        public int consignee_id { get; set; }
        public string consignee_name { get; set; }
        public string consignee_code { get; set; }
        [Display(Name = "Net Value *")]
        public double net_value { get; set; }
        [Display(Name = "Gross Value *")]
        public double gross_value { get; set; }
        public int net_currency_id { get; set; }
        public string net_currency_name { get; set; }
        public string gross_currency_name { get; set; }
        public string gross_currency_id { get; set; }
        [Display(Name = "Business Unit *")]
        public int business_unit_id { get; set; }
        public string business_unit_name { get; set; }
        public string business_desc { get; set; }
        [Display(Name = "Plant *")]
        public int plant_id { get; set; }
        public string plant_name { get; set; }
        public string plant_code { get; set; }
        [Display(Name = "Territory")]
        public int? territory_id { get; set; }
        public string territory_name { get; set; }
        [Display(Name = "Sales RM")]
        public int? sales_rm_id { get; set; }
        public string sales_rm_name { get; set; }
        [Display(Name = "Billing Address *")]
        public string billing_address { get; set; }
        [Display(Name = "Billing City *")]
        public string billing_city { get; set; }
        [Display(Name = "Billing Pincode *")]
        public string billing_pincode { get; set; }
        [Display(Name = "Billing Country *")]
        public int billing_country_id { get; set; }
        public string billing_country_name { get; set; }
        [Display(Name = "Billing State *")]
        public int billing_state_id { get; set; }
        public string billing_state_name { get; set; }
        [Display(Name = "Billing Email")]
        public string billing_email_id { get; set; }
        [Display(Name = "Shipping Address *")]
        public string shipping_address { get; set; }
        [Display(Name = "Shipping City *")]
        public string shipping_city { get; set; }
        [Display(Name = "Shipping Pincode *")]
        public string shipping_pincode { get; set; }
        [Display(Name = "Shipping Country *")]
        public int shipping_country_id { get; set; }
        public string shipping_country_name { get; set; }
        [Display(Name = "Shipping State *")]
        public int shipping_state_id { get; set; }
        public string shipping_state_name { get; set; }
        [Display(Name = "Payment Cycle Type *")]
        public int payment_cycle_type_id { get; set; }
        public string payment_cycle_type_name { get; set; }
        [Display(Name = "Payment Cycle *")]
        public int payment_cycle_id { get; set; }
        public string payment_cycle_name { get; set; }
        [Display(Name = "Payment Terms *")]
        public int payment_terms_id { get; set; }
        public string payment_terms_name { get; set; }
        [Display(Name = "Credit Available After Invoice")]
        public double? credit_avail_after_invoice { get; set; }
        [Display(Name = "TDS Code")]
        public int? tds_code_id { get; set; }
        public string tds_code_name { get; set; }
        [Display(Name = "GST TDS")]
        public int? gst_tds_code_id { get; set; }
        public string gst_tds_code_name { get; set; }
        [Display(Name = "Available Credit Limit")]
        public double? available_credit_limit { get; set; }
        [Display(Name = "Internal Remarks")]
        [DataType(DataType.MultilineText)]
        public string internal_remarks { get; set; }
        [Display(Name = "Remarks on Document")]
        [DataType(DataType.MultilineText)]
        public string remarks_on_document { get; set; }
        [Display(Name = "Attachment")]
        public string attachment { get; set; }
        [Display(Name = "Gate Entry No")]
        public string gate_entry_no { get; set; }
        [Display(Name = "Gate Entry Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? gate_entry_date { get; set; }
        [Display(Name = "Document Number")]
        public string invoice_number { get; set; }
        [Display(Name = "Reason for Return *")]
        [DataType(DataType.MultilineText)]
        public string reason_for_return { get; set; }
        [Display(Name = "Number")]
        public string return_number { get; set; }
        public string si_number { get; set; }
        [Display(Name = "Document Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? invoice_date { get; set; }
   
        public double? round_off { get; set; }
        [NotMapped]
        public HttpPostedFileBase FileUpload { get; set; }
        public virtual List<sal_si_return_detail> sal_si_return_detail { get; set; }
        public List<sal_si_return_detail_vm> sal_si_return_detail_vm { get; set; }
    }
    public partial class sal_si_return_vm
    {
        public List<string> si_detail_ids { get; set; }
        public List<string> si_ids { get; set; }
        public List<string> item_ids { get; set; }
        public List<string> uom_ids { get; set; }
        public List<string> unit_prices { get; set; }
        public List<string> discounts { get; set; }
        public List<string> effective_unit_prices { get; set; }
        public List<string> sales_values { get; set; }
        public List<string> assessable_rates { get; set; }
        public List<string> assessable_values { get; set; }
        public List<string> tax_ids { get; set; }
        public List<string> storage_location_ids { get; set; }
        public List<string> drawing_nos { get; set; }
        public List<string> sac_hsn_ids { get; set; }
        public List<string> plant_ids { get; set; }       
        public List<string> issue_quantitys { get; set; }
        public List<string> item_batch_detail_ids { get; set; }
        public List<string> item_batch_ids { get; set; }
    }
    public partial class sal_si_return_vm
    {
        public List<string> b_si_detail_batch_id { get; set; }
        public List<string> b_si_detail_id { get; set; }
        public List<string> b_item_batch_detail_id { get; set; }
        public List<string> b_batch_number { get; set; }
        public List<string> b_item_id { get; set; }
        public List<string> b_item_name { get; set; }
        public List<string> b_batch_quantity { get; set; }
        public List<string> b_quantity { get; set; }
    }
    public partial class sal_si_return_vm
    {
        public List<string> t_si_detail_id { get; set; }
        public List<string> t_item_id { get; set; }
        public List<string> t_item_name { get; set; }
        public List<string> t_tag_no { get; set; }
        public List<string> t_quantity { get; set; }
    }
}
