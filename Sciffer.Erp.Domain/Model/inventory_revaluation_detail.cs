using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class inventory_revaluation_detail
    {
        [Key]
        public int inv_revaluation_detail_id { get; set; }
        [Required(ErrorMessage ="Quantity is required")]
        [Display(Name ="Quantity")]
        public double quantity { get; set; }
        [Required(ErrorMessage ="new rate is required")]
        [Display(Name ="New Rate")]
        public double new_rate { get; set; }
        [Required(ErrorMessage ="old rate is required")]
        [Display(Name ="Old Rate")]
        public double old_rate { get; set; }
        [Required(ErrorMessage ="differential rate is required")]
        [Display(Name ="Diff Rate")]
        public double differential_rate { get; set; }
        [Required(ErrorMessage ="differential value is required")]
        [Display(Name ="Diff Value")]
        public double differential_value { get; set; }
        public int general_ledger_id { get; set; }
        [ForeignKey("general_ledger_id")]
        public virtual ref_general_ledger REF_GENERAL_LEDGER { get; set; }
        public int item_id { get; set; }
        [ForeignKey("item_id")]
        public virtual REF_ITEM REF_ITEM { get; set; }
        public int uom_id { get; set; }
        [ForeignKey("uom_id")]
        public virtual REF_UOM REF_UOM { get; set; }
        public int sr_no { get; set; }
        public bool is_active { get; set; }
        public int inventory_revaluation_id { get; set; }
        [ForeignKey("inventory_revaluation_id")]
        public virtual inventory_revaluation inventory_revaluation { get; set; }

    }
}
