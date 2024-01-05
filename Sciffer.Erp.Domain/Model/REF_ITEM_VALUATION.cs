using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
    public class REF_ITEM_VALUATION
    {
        [Key]
        public int ITEM_VALUATION_ID { get; set; }
        [Display(Name = "Item Valuation Name")]
        public string ITEM_VALUATION_NAME { get; set; }
    }
}
