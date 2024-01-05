using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class sal_so_detail
    {
        [Key]
        public int? so_detail_id { get; set; }
        public int so_id { get; set; }
        [ForeignKey("so_id")]
        public virtual sal_so sal_so { get; set; }
        public int sr_no { get; set; }
        public int item_id { get; set; }
        public virtual REF_ITEM REF_ITEM { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime delivery_date { get; set; }
        public double quantity { get; set; }
        public int uom_id { get; set; }
        [ForeignKey("uom_id")]
        public virtual REF_UOM REF_UOM { get; set; }
        public double unit_price { get; set; }
        public double discount { get; set; }
        public double effective_unit_price { get; set; }
        public double sales_value { get; set; }
        public double assessable_rate { get; set; }
        public double assessable_value { get; set; }
        public int tax_id { get; set; }
        [ForeignKey("tax_id")]
        public virtual ref_tax ref_tax { get; set; }
        public bool? is_active { get; set; }
        public int storage_location_id { get; set; }
        [ForeignKey("storage_location_id")]
        public virtual REF_STORAGE_LOCATION REF_STORAGE_LOCATION { get; set; }
        public int quotation_detail_id { get; set; }
        public string drawing_no { get; set; }
        public double material_cost_per_unit { get; set; }
        public decimal? machine_charges { get; set; }
    }
}
