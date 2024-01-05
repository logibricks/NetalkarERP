using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_customer_item_category
    {
        [Key]
        [Column(Order = 0)]
        public int customer_id { get; set; }
        [ForeignKey("customer_id")]
        public virtual REF_CUSTOMER REF_CUSTOMER { get; set; }
        [Key]
        [Column(Order = 1)]
        public int item_category_id { get; set; }
        [ForeignKey("item_category_id")]
        public virtual REF_ITEM_CATEGORY REF_ITEM_CATEGORY { get; set; }
        public double rate { get; set; }
    }
}
