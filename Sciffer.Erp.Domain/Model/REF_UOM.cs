using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
   public  class REF_UOM
    {
        [Key]
        public int UOM_ID { get; set; }
        [Display(Name = "Unit")]
        public string UOM_NAME { get; set; }
        [Display(Name ="Description")]
        public string UOM_DESCRIPTION { get; set; }
        public bool is_active { get; set; }
        public bool is_blocked { get; set; }
    }
}
