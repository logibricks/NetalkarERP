using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_item_type
    {
        [Key]
        public int item_type_id { get; set; }
        public string item_type_name { get; set; }
    }
}
