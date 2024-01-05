using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class pur_pi_return_detail
    {
        [Key]
        public int pi_return_detail_id { get; set; }
        public int pi_return_id { get; set; }
        [ForeignKey("pi_return_id")]
        public virtual pur_pi_return pur_pi_return { get; set; }

        public int pi_id { get; set; }
        [ForeignKey("pi_id")]
        public virtual pur_pi pur_pi { get; set; }

        public int pi_detail_id { get; set; }
        public int item_id { get; set; }
        [ForeignKey("item_id")]
        public virtual REF_ITEM REF_ITEM { get; set; }
        public int sloc_id { get; set; }
        [ForeignKey("sloc_id")]
        public virtual REF_STORAGE_LOCATION REF_STORAGE_LOCATION { get; set; }
        public double quantity { get; set; }
        public int uom_id { get; set; }
        [ForeignKey("uom_id")]
        public virtual REF_UOM REF_UOM { get; set; }

        public double unit_price { get; set; }
        public double? discount { get; set; }
        public double eff_unit_price { get; set; }
        public double purchase_value { get; set; }
        public double assessable_rate { get; set; }
        public double assessable_value { get; set; }
        public int tax_id { get; set; }
        [ForeignKey("tax_id")]
        public virtual ref_tax ref_tax { get; set; }        
        public double? grir_value { get; set; }
        public double basic_value { get; set; }
        public bool is_active { get; set; }
        public int batch_id { get; set; }
        [ForeignKey("batch_id")]
        public virtual inv_item_batch inv_item_batch { get; set; }
        public int bucket_id { get; set; }
        [ForeignKey("bucket_id")]
        public virtual ref_bucket ref_bucket { get; set; }
    }
}
