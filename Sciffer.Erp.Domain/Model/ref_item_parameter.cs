using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public  class ref_item_parameter
    {
        [Key]        
        public int parameter_id { get; set; }       
        public int item_id { get; set; }
        [ForeignKey("item_id")]
        public virtual REF_ITEM REF_ITEM { get; set; }
        public string parameter_name { get; set; }
        public string parameter_range { get; set; }
        public bool? is_active { get; set; }
    }
}
