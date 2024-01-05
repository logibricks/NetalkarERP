using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
    public class REF_GENDER
    {
        [Key]
        public int GENDER_ID { get; set; }
        [Display(Name ="Gender")]
        public string GENDER_NAME { get; set; }
    }
}
