using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
  public class REF_PRIORITY
    {
        [Key]
        public int PRIORITY_ID { get; set; }
        [Required]
        [Display(Name = "Priority")]
        public string PRIORITY_NAME { get; set; }
        public bool is_active { get; set; }
        public int form_id { get; set; }
        public bool is_blocked { get; set; }
    }
    public class ref_priority_vm
    {
        public int PRIORITY_ID { get; set; }      
        public string PRIORITY_NAME { get; set; }
        public bool is_active { get; set; }
        public int form_id { get; set; }
        public string form_name { get; set; }
        public bool is_blocked { get; set; }
    }
}
