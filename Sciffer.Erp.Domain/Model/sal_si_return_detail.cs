using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class sal_si_return_detail
    {
        [Key]
        public int sales_return_detail_id { get; set; }
        public int sales_return_id { get; set; }
        public int item_id { get; set; }
        [ForeignKey("item_id")]
        public virtual REF_ITEM REF_ITEM { get; set; }
      //  public double? quantity { get; set; }
        public int uom_id { get; set; }
        [ForeignKey("uom_id")]
        public virtual REF_UOM REF_UOM { get; set; }
        public double? unit_price { get; set; }
        public double? discount { get; set; }
        public double? effective_unit_price { get; set; }
        public double? sales_value { get; set; }
        public double? assessable_rate { get; set; }
        public double? assessable_value { get; set; }
        public int tax_id { get; set; }
        [ForeignKey("tax_id")]
        public virtual ref_tax ref_tax { get; set; }
        public int storage_location_id { get; set; }
        [ForeignKey("storage_location_id")]
        public virtual REF_STORAGE_LOCATION REF_STORAGE_LOCATION { get; set; }
        public string drawing_no { get; set; }
        public int sac_hsn_id { get; set; }
        [ForeignKey("sac_hsn_id")]
        public virtual ref_hsn_code ref_hsn_code { get; set; }
        public int plant_id { get; set; }
        [ForeignKey("plant_id")]
        public virtual REF_PLANT REF_PLANT { get; set; }
        public double? issue_quantity { get; set; }
        public int item_batch_detail_id { get; set; }
        public int item_batch_id { get; set; }
        public int si_id { get; set; }
        public int si_detail_id { get; set; }
    }
}
