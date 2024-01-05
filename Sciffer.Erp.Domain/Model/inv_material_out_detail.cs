using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
   public class inv_material_out_detail
    {
        [Key]
        public int material_out_detail_id { get; set; }

        public int material_out_id { get; set; }
        [ForeignKey ("material_out_id")]
        public virtual inv_material_out inv_material_out { get; set; }

        public int item_id { get; set; }
        [ForeignKey("item_id")]
        public virtual REF_ITEM REF_ITEM { get; set; }

        public string user_description { get; set; }

        public int uom_id { get; set; }
        [ForeignKey("uom_id")]
        public virtual REF_UOM REF_UOM { get; set; }

        public double quantity { get; set; }
        public double? rate { get; set; }
        public double? value { get; set; }

        public string reason { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime er_date { get; set; }

        public double balance_qty { get; set; }

        [Display(Name = "Remarks")]
        [DataType(DataType.MultilineText)]
        public string remarks { get; set; }
        public bool is_active { get; set; }
        public int? tax_id { get; set; }
        public int? hsn_id { get; set; }
    }
}
