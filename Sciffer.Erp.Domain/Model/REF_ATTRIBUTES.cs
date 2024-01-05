using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
   public class REF_ATTRIBUTES
    {
        [Key]
        public int ATTRIBUTE_ID { get; set; }
        [Required]
        [Display(Name = "Attribute Name")]
        public string ATTRIBUTE_NAME { get; set; }
        [Display(Name = "Attribute Code")]
        public string ATTRIBUTE_CODE { get; set; }
        [Display(Name = "Attribute DataType")]
        public string ATTRIBUTE_DATATYPE { get; set; }
        [Display(Name = "IS Nullable")]
        public bool IS_NULLABLE { get; set; }
        public virtual REF_ENTITY_TYPE REF_ENTITY_TYPE { get; set; }
        [Required]
        [Display(Name = "Entity Type")]
        public int ENTITY_TYPE_ID { get; set; }
    }
}
