using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
   public  class REF_ENTITY_TYPE
    {
        [Key]
        public int ENTITY_TYPE_ID { get; set; }
        [Required]
        [Display(Name = "Entity Type")]
        public string ENTITY_TYPE_NAME { get; set; }
        public bool IS_ACTIVE { get; set; }
    }
}
