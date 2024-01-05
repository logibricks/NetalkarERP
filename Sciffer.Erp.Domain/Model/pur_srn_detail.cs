using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class pur_srn_detail
    {
        [Key]
        public int srn_detail_id { get; set; }
        public int srn_id { get; set; }
        public int item_id { get; set; }
        [ForeignKey("item_id")]
        public virtual REF_ITEM REF_ITEM { get; set; }
        public int po_detail_id { get; set; }
        public DateTime? delivery_date { get; set; }
        public int storage_location_id { get; set; }
        [ForeignKey("storage_location_id")]
        public virtual REF_STORAGE_LOCATION REF_STORAGE_LOCATION { get; set; }
        public double quantity { get; set; }
        public int uom_id { get; set; }
        [ForeignKey("uom_id")]
        public virtual REF_UOM REF_UOM { get; set; }
        public double unit_price { get; set; }
        public double discount { get; set; }
        public double eff_unit_price { get; set; }
        public double purchase_value { get; set; }       
        public int tax_id { get; set; }
        [ForeignKey("")]
        public virtual ref_tax REF_TAX { get; set; }
        public bool is_active { get; set; }
        public string user_description { get; set; }       
        public bool order_status { get; set; }
        public int sac_hsn_id { get; set; }
        public int staggerred_delivery_detail_id { get; set; }

    }
}
