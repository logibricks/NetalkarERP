using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
   public class SAL_QUOTATION_DETAIL
    {
        [Key]
        public int? QUOTATION_DETAIL_ID { get; set; }
        public int QUOTATION_ID { get; set; }
        [ForeignKey("QUOTATION_ID")]
        public virtual SAL_QUOTATION SAL_QUOTATION { get; set; }
        public int SR_NO { get; set; }
        public int ITEM_ID { get; set; }
        public virtual REF_ITEM REF_ITEM { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DELIVERY_DATE { get; set; }
        public double QUANTITY { get; set; }
        public int SLOC_ID { get; set; }
        [ForeignKey("SLOC_ID")]
        public virtual REF_STORAGE_LOCATION REF_STORAGE_LOCATION { get; set; }
        public int UOM_ID { get; set; }
        [ForeignKey("UOM_ID")]
        public virtual REF_UOM REF_UOM { get; set; }
        public double UNIT_PRICE { get; set; }
        public double DISCOUNT { get; set; }
        public double EFFECTIVE_UNIT_PRICE { get; set; }
        public double SALES_VALUE { get; set; }
        public double ASSESSABLE_RATE { get; set; }
        public double ASSESSABLE_VALUE { get; set; }
        public int tax_id { get; set; }
        [ForeignKey("tax_id")]
        public virtual ref_tax ref_tax { get; set; }
        public bool? IS_ACTIVE { get; set; }
        public double sales_value_local { get; set; }
        public double assessable_value_local { get; set; }
        public string drawing_no { get; set; }
        public double material_cost_per_unit { get; set; }
        public int sac_hsn_id { get; set; }
    }
}
