using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
    public class REF_ITEM_ACCOUNTING
    {
        [Key]
        public int ITEM_ACCOUNTING_ID { get; set; }
        [Display(Name = "Item Accounting")]
        public string ITEM_ACCOUNTING_NAME { get; set; }
    }
}
