using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class sal_si
    {
        [Key]
        public int si_id { get; set; }
        public int so_id { get; set; }
        [ForeignKey("so_id")]
        public virtual sal_so sal_so { get; set; }
        public int sales_category_id { get; set; }
        [ForeignKey("sales_category_id")]
        public virtual ref_document_numbring ref_document_numbring { get; set; }
        [Display(Name = "SI Number")]
        public string si_number { get; set; }
        [Display(Name = "SI Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? si_date { get; set; }
        public int buyer_id { get; set; }
        [ForeignKey("buyer_id")]
        public virtual REF_CUSTOMER REF_CUSTOMER { get; set; }
        public int consignee_id { get; set; }
        [ForeignKey("consignee_id")]
        public virtual REF_CUSTOMER REF_CUSTOMER1 { get; set; }
        [Display(Name = "Net Value")]
        public double sal_net_value { get; set; }
        public int net_value_currency_id { get; set; }
        [ForeignKey("net_value_currency_id")]
        public virtual REF_CURRENCY REF_CURRENCY { get; set; }
        [Display(Name = "Gross Value")]
        public double sal_gross_value { get; set; }
        public int gross_value_currency_id { get; set; }
        [ForeignKey("gross_value_currency_id")]
        public virtual REF_CURRENCY REF_CURRENCY1 { get; set; }
        public int business_unit_id { get; set; }
        [ForeignKey("business_unit_id")]
        public virtual REF_BUSINESS_UNIT REF_BUSINESS_UNIT { get; set; }
        public int plant_id { get; set; }
        [ForeignKey("plant_id")]
        public virtual REF_PLANT REF_PLANT { get; set; }
        public int freight_terms_id { get; set; }
        [ForeignKey("freight_terms_id")]
        public virtual REF_FREIGHT_TERMS REF_FREIGHT_TERMS { get; set; }
        public int? territory_id { get; set; }
        [ForeignKey("territory_id")]
        public virtual REF_TERRITORY REF_TERRITORY { get; set; }
        public int sales_rm_id { get; set; }
        [ForeignKey("sales_rm_id")]
        public virtual REF_USER REF_USER { get; set; }
        [Display(Name = "Removal Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? removal_date { get; set; }
        [Display(Name = "Removal Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:t}", ApplyFormatInEditMode = true)]
        public DateTime? removal_time { get; set; }
        [Display(Name = "Customer PO Number")]
        public string customer_po_no { get; set; }
        [Display(Name = "Customer PO Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? customer_po_date { get; set; }
        public int payment_terms_id { get; set; }
        [ForeignKey("payment_terms_id")]
        public virtual REF_PAYMENT_TERMS REF_PAYMENT_TERMS { get; set; }
        public int payment_cycle_id { get; set; }
        [ForeignKey("payment_cycle_id")]
        public virtual REF_PAYMENT_CYCLE REF_PAYMENT_CYCLE { get; set; }
        [Display(Name = "Credit Available After Invoice")]
        public double? credit_avail_after_invoice { get; set; }
        [Display(Name = "Internal Remarks")]
        public string internal_remarks { get; set; }
        [Display(Name = "Remarks on Document")]
        public string remarks { get; set; }
        [Display(Name = "Billing Address")]
        [DataType(DataType.MultilineText)]
        public string billing_address { get; set; }
        [Display(Name = "Billing City")]
        public string billing_city { get; set; }
        [Display(Name = "Billing Pincode")]
        public int billing_pincode { get; set; }
        public int billing_state_id { get; set; }
        [ForeignKey("billing_state_id")]
        public virtual REF_STATE REF_STATE { get; set; }
        [Display(Name = "Email ID")]
        public string billing_email_id { get; set; }
        [Display(Name = "Shipping Address")]
        [DataType(DataType.MultilineText)]
        public string shipping_address { get; set; }
        [Display(Name = "Shipping City")]
        public string shipping_city { get; set; }
        [Display(Name = "Shipping Pincode")]
        public int shipping_pincode { get; set; }
        public int shipping_state_id { get; set; }
        [ForeignKey("shipping_state_id")]
        public virtual REF_STATE REF_STATE1 { get; set; }
        public bool is_active { get; set; }
        [Display(Name = "Vehicle Number")]
        public string vehicle_no { get; set; }
        public virtual ICollection<sal_si_detail> SAL_SI_DETAIL { get; set; }
        public virtual ICollection<sal_si_form> SAL_SI_FORM { get; set; }
        [Display(Name = "PAN *")]
        public string pan_no { get; set; }
        [Display(Name = "ECC No")]
        public string ecc_no { get; set; }
        [Display(Name = "VAT TIN No")]
        public string vat_tin_no { get; set; }
        [Display(Name = "CST TIN No")]
        public string cst_tin_no { get; set; }
        [Display(Name = "Service Tax No")]
        public string service_tax_no { get; set; }
        [Display(Name = "GST No")]
        public string gst_no { get; set; }
        public string attachment { get; set; }
        [Display(Name = "Commisionerate")]
        public string commisionerate { get; set; }
        [Display(Name = "Range")]
        public string range { get; set; }
        [Display(Name = "Division")]
        public string division { get; set; }
        public int? doc_currency_id { get; set; }
        [Display(Name = "TDS Code")]
        public int tds_code_id { get; set; }
        [Display(Name = "ASN No ")]
        public string asn_no { get; set; }
        [Display(Name = "Transporter ")]
        public string transporter { get; set; }
        [Display(Name = "LR No ")]
        public string lr_no { get; set; }
        public double? round_off { get; set; }
         public int? supply_state_id { get; set; }
        public int? delivery_state_id { get; set; }
        public int? mode_of_transport { get; set; }
        public DateTime? lr_date { get; set; }
        public string e_way_bill { get; set; }
        public DateTime? e_way_bill_date { get; set; }
        public string shipping_email_id { get; set; }       
        public string shipping_pan_no { get; set; }       
        public string shipping_gst_no { get; set; }
        public double? available_credit_limit { get; set; }
        public int? gst_tds_code_id { get; set; }
        public DateTime? out_date { get; set; }
        public TimeSpan? out_time { get; set; }
        public string cancellation_remarks { get; set; }
        //public int? status_id { get; set; }
        public int? cancellation_reason_id { get; set; }
        public DateTime? cancelled_date { get; set; }
        public int? cancelled_by { get; set; }
        //public virtual ref_status ref_status { get; set; }

    }
}
