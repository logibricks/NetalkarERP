using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class pur_incoming_excise_detail
    {
        [Key]
        public int incoming_excise_detail_id { get; set; }
        [Display(Name = "Item")]
        public int item_id { get; set; }
        [ForeignKey("item_id")]
        public virtual REF_ITEM REF_ITEM { get; set;}
        public int grn_detail_id { get; set; }
        [Display(Name = "Delivery Date")]
        public DateTime delivery_date { get; set; }
        [Display(Name = "Storage Location")]
        public int storage_location_id { get; set; }
        [ForeignKey("storage_location_id")]
        public virtual REF_STORAGE_LOCATION REF_STORAGE_LOCATION { get; set; }
        [Display(Name = "Quantity")]
        public double quantity { get; set; }
        [Display(Name = "UoM")]
        public int uom_id { get; set; }
        [ForeignKey("uom_id")]
        public virtual REF_UOM REF_UOM { get; set; }
        [Display(Name = "Unit Price")]
        public double unit_price { get; set; }
        [Display(Name = "Discount")]
        public double discount { get; set; }
        [Display(Name = "Effective Unit Price")]
        public double eff_unit_price { get; set; }
        [Display(Name = "Purchase Value")]
        public double purchase_value { get; set; }
        [Display(Name = "Assessable Rate")]
        public double assessable_rate { get; set; }
        [Display(Name = "Assessable Value")]
        public double assessable_value { get; set; }
        [Display(Name = "Tax Code")]
        public int tax_id { get; set; }
        [ForeignKey("tax_id")]
        public virtual ref_tax ref_tax { get; set; }
        public bool is_active { get; set; }
        public int incoming_excise_id { get; set; }
        [ForeignKey("incoming_excise_id")]
        public virtual pur_incoming_excise pur_incoming_excise { get; set; }
        public double? tax_value { get; set; }
    }

    public class pur_incoming_excise_detail_vm
    {
        
        //public int incoming_excise_detail_id { get; set; }
        //[Display(Name = "Item")]
        //public int item_id { get; set; }
        //[ForeignKey("item_id")]
        //public virtual REF_ITEM REF_ITEM { get; set; }
        //public int grn_detail_id { get; set; }
        //[Display(Name = "Delivery Date")]
        //public DateTime delivery_date { get; set; }
        //[Display(Name = "Storage Location")]
        //public int storage_location_id { get; set; }
        //[ForeignKey("storage_location_id")]
        //public virtual REF_STORAGE_LOCATION REF_STORAGE_LOCATION { get; set; }
        //[Display(Name = "Quantity")]
        //public double quantity { get; set; }
        //[Display(Name = "UoM")]
        //public int uom_id { get; set; }
        //[ForeignKey("uom_id")]
        //public virtual REF_UOM REF_UOM { get; set; }
        //[Display(Name = "Unit Price")]
        //public double unit_price { get; set; }
        //[Display(Name = "Discount")]
        //public double discount { get; set; }
        //[Display(Name = "Effective Unit Price")]
        //public double eff_unit_price { get; set; }
        //[Display(Name = "Purchase Value")]
        //public double purchase_value { get; set; }
        //[Display(Name = "Assessable Rate")]
        //public double assessable_rate { get; set; }
        //[Display(Name = "Assessable Value")]
        //public double assessable_value { get; set; }
        //[Display(Name = "Tax Code")]
        //public int tax_id { get; set; }
        //[ForeignKey("tax_id")]
        //public virtual ref_tax ref_tax { get; set; }
        //public bool is_active { get; set; }
        //public int incoming_excise_id { get; set; }
        //[ForeignKey("incoming_excise_id")]
        //public virtual pur_incoming_excise pur_incoming_excise { get; set; }

        public int iexid { get; set; }
        public string itemcode { get; set; }
        public string Itemdescription { get; set; }
        public double Qty { get; set; }
        public string UoM { get; set; }
        public double Ass_rate { get; set; }
        public double Eff_rate { get; set; }
        public double Ass_value { get; set; }
        public double Purchase_value { get; set; }
        public int Tax_Code { get; set; }
        public int iexdetail { get; set; }
    }
}
