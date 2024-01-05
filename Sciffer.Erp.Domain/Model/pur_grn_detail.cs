using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class pur_grn_detail
    {
        [Key]
        public int? grn_detail_id { get; set; }
        public int item_id { get; set; }
        [ForeignKey("item_id")]
        public virtual REF_ITEM REF_ITEM { get; set; }
        public DateTime delivery_date { get; set; }
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
        public double assessable_rate { get; set; }
        public double assessable_value { get; set; }
        public int tax_id { get; set; }
        [ForeignKey("tax_id")]
        public virtual ref_tax REF_TAX { get; set; }
        public bool is_active { get; set; }
        public int grn_id { get; set; }
        public int po_detail_id { get; set; }
        [ForeignKey("grn_id")]
        public virtual pur_grn pur_grn { get; set; }
        [Display(Name ="User Description")]
        public string user_description { get; set; }            
        [Display(Name = "Bucket")]
        public int bucket_id { get; set; }
        [ForeignKey("bucket_id")]
        public virtual ref_bucket ref_bucket { get; set; }
        public string batch { get; set; }
        public bool? batch_managed { get; set; }
        public DateTime? expirary_date { get; set; }
        public int sac_hsn_id { get; set; }
        [ForeignKey("sac_hsn_id")]
        public virtual ref_hsn_code ref_hsn_code { get; set; }
        public int? staggerred_delivery_detail_id { get; set; }
    }
}
