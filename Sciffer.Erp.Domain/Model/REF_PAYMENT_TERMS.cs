using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
    public class REF_PAYMENT_TERMS
    {
        [Key]
        public int payment_terms_id { get; set; }    
        [Display(Name = "Payment Terms Days")]
        public int payment_terms_days { get; set; }       
        [Display(Name = "Payment Terms Code")]
        public string payment_terms_code { get; set; }
        [Display(Name = "Description")]
        public string payment_terms_description { get; set; }
        public int payment_terms_due_date_id { get; set; }
        public virtual REF_PAYMENT_TERMS_DUE_DATE REF_PAYMENT_TERMS_DUE_DATE { get; set; }
        public bool is_active { get; set; }
        public bool is_blocked { get; set; }
    }

    public class payment_terms_vm
    {
        public int payment_terms_id { get; set; }
        public string payment_terms_code { get; set; }
        public string payment_terms_description { get; set; }
        public int payment_terms_days { get; set; }
        public string payment_terms_due_date { get; set; }
        public bool is_blocked { get; set; }
        public int payment_terms_due_date_id { get; set; }
    }
}