using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class REF_ITEM_CATEGORYVM
    {
        [Key]
        public int ITEM_CATEGORY_ID { get; set; }
        [Display(Name = "Item Category Code *")]
        [Required]
        public string ITEM_CATEGORY_NAME { get; set; }
        [Display(Name = "Item Category Description *")]
        public string ITEM_CATEGORY_DESCRIPTION { get; set; }
        [Display(Name = "Block")]
        public bool is_blocked { get; set; }
        public bool is_active { get; set; }
        public string ledgeraccounttype { get; set; }
        public string item_type_name { get; set; }
        [Display(Name = "Item Type *")]
        public int? item_type_id { get; set; }

        [Display(Name = "Prefix/Suffix*")]
        public int? prefix_sufix_id { get; set; }
        public string prefix_sufix { get; set; }
        [Display(Name = "From Number")]
        public string from_number { get; set; }
        [Display(Name = "Last Used")]
        public string current_number { get; set; }
    }
}
