using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_bank
    {
        [Key]
        public int bank_id { get; set; }
        [Display(Name ="Bank Name")]
        public string bank_name { get; set; }
        public string bank_code { get; set; }
        public bool? is_active { get; set; }
        public bool is_blocked { get; set; }
    }
    public class ref_bank_vm
    {
        public string bank_code { get; set; }
        public int bank_id { get; set; }
        public string bank_name { get; set; }
    }
}
