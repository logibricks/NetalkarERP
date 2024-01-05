using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
    public class MFG_BATCH
    {
        [Key]
        public int BATCH_ID { get; set; }
        [Display(Name ="Batch Number")]
        public string BATCH_NUMBER { get; set; }
    }
}
