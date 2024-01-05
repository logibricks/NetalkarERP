using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class pur_pi_detail
    {
        [Key]
        public int? pi_detail_id { get; set; }
        public int pi_id { get; set; }
        [ForeignKey("pi_id")]
        public virtual pur_pi pur_pi { get; set; }
        public int item_id { get; set; }
        [ForeignKey("item_id")]
        public virtual REF_ITEM REF_ITEM { get; set; }
        public string user_description{get;set;}
        public DateTime? delivery_date { get; set; }
        public int storage_loaction_id { get; set; }
        public double quantity { get; set; }
        public int uom_id { get; set; }        
        public double unit_price { get; set; }
        public double discount { get; set; }
        public double eff_unit_price { get; set; }
        public double purchase_value { get; set; }
        public double assessable_rate { get; set; }
        public double assessable_value { get; set; }
        public int tax_id { get; set; }
        [ForeignKey("tax_id")]
        public virtual ref_tax ref_tax { get; set; }
        public int cost_center_id { get; set; }
        public int grn_detail_id { get; set; }
        public double grir_value { get; set; }
        public double basic_value { get; set; }
        public bool is_active { get; set; }
        public int sac_hsn_id { get; set; }
        public int? item_type_id { get; set; }
    }
    public class pi_detail_vm
    {
        public int pi_detail_id { get; set; }
        public int pi_id { get; set; }
        public int item_id { get; set; }
        public string user_description { get; set; }
        public DateTime? delivery_date { get; set; }
        public int storage_loaction_id { get; set; }
        public double quantity { get; set; }
        public int uom_id { get; set; }
        public double unit_price { get; set; }
        public double discount { get; set; }
        public double eff_unit_price { get; set; }
        public double purchase_value { get; set; }
        public double assesable_rate { get; set; }
        public double assesable_value { get; set; }
        public int tax_id { get; set; }
        public int cost_center_id { get; set; }
        public int grn_detail_id { get; set; }
        public bool is_active { get; set; }
        public string sloc_name { get; set; }
        public string item_name { get; set; }
        public string uom_name { get; set; }
        public string tax_name { get; set; }
        public string cost_center_description { get; set; }
        public string item_code { get; set; }
        public double grir_value { get; set; }
        public double basic_value { get; set; }
        public int? hsn_id { get; set; }
        public string hsn_code { get; set; }
        public int? item_type_id { get; set; }
        public string grn_no { get; set; }
        public string grn_date { get; set; }
    }
}
