using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Sciffer.Erp.Domain.Model
{
  public class inv_material_in_detail
    {
        [Key]
        public int material_in_detail_id { get; set; }

        public int material_in_id { get; set; }
        [ForeignKey("material_in_id")]
        public virtual inv_material_in inv_material_in { get; set; }

        public int item_id { get; set; }
        [ForeignKey("item_id")]
        public virtual REF_ITEM REF_ITEM { get; set; }

        public string user_description { get; set; }

        public int uom_id { get; set; }
        [ForeignKey("uom_id")]
        public virtual REF_UOM REF_UOM { get; set; }

        public double quantity { get; set; }

        public string reason { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime er_date { get; set; }

        public double balance_qty { get; set; }



    }
}
