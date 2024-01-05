using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
    public class REF_ITEM_GROUP
    {
        [Key]
        public int ITEM_GROUP_ID { get; set; }
        [Display(Name = "Item Group")]
        public string ITEM_GROUP_NAME { get; set; }
        [Display(Name ="Description")]
        public string ITEM_GROUP_DESCRIPTION { get; set; }
        public bool is_active { get; set; }
        public bool is_blocked { get; set; }
    }
}
