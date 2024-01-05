using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
    public class REF_SOURCE
    {
        [Key]
        public int SOURCE_ID { get; set; }
        [Display(Name ="Source Name")]
        [Required]
        public string SOURCE_NAME { get; set; }
    }
}
