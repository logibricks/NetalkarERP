using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
    public class REF_DIVISION
    {
        [Key]
        public int DIVISION_ID { get; set; }
        [Display(Name ="Division Name")]
        public string DIVISION_NAME { get; set; }

    }
}
