using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
    public class REF_EXCISE_CATEGORY
    {
        [Key]
        public int EXCISE_CATEGORY_ID { get; set; }
        [Display(Name = "Excise Category")]
        public string EXCISE_CATEGORY_NAME { get; set; }
    }
}
