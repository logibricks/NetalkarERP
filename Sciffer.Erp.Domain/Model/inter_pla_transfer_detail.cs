using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class inter_pla_transfer_detail
    {
        [Key]
        public int pla_transfer_detail_id { get; set; }
        [Required(ErrorMessage = "Quantity is required")]
        [Display(Name = "Quantity")]
        public double pla_qty { get; set; }      
        [Required(ErrorMessage = "uom is required")]
        [Display(Name = "Uom")]
        public int uom_id { get; set; }
        [ForeignKey("uom_id")]
        public virtual REF_UOM REF_UOM { get; set; }
        public int batch_id { get; set; }
        [ForeignKey("batch_id")]
        public virtual inv_item_batch inv_item_batch { get; set; }
        public int item_id { get; set; }
        [ForeignKey("item_id")]
        public virtual REF_ITEM REF_ITEM { get; set; }
        public int pla_transfer_id { get; set; }
        [ForeignKey("pla_transfer_id")]
        public virtual inter_pla_transfer intra_pla_transfer { get; set; }
        public bool is_active { get; set; } 
        public int sr_no { get; set; }
        public int item_batch_detail_id { get; set; }
        [ForeignKey("item_batch_detail_id")]
        public virtual inv_item_batch_detail_tag inv_item_batch_detail_tag { get; set; }
    }
}
