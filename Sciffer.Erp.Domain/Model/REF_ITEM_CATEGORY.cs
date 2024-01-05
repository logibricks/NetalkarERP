using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class REF_ITEM_CATEGORY
    {
        [Key]
        public int ITEM_CATEGORY_ID { get; set; }
        public string ITEM_CATEGORY_NAME { get; set; }
        public string ITEM_CATEGORY_DESCRIPTION { get; set; }
        public bool is_blocked { get; set; }
        public bool is_active { get; set; }
        public int? item_type_id { get; set; }
        public int? prefix_sufix_id { get; set; }
        public string prefix_sufix { get; set; }
        public string from_number { get; set; }
        public string current_number { get; set; }
    }
}
