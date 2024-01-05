using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
    public class REF_REASON_DETERMINATION
    {
        [Key]
        public int REASON_DETERMINATION_ID { get; set; }
        [Display(Name ="Reason")]
        public string REASON_DETERMINATION_NAME { get; set; }
        [Display(Name = "Reason Type")]
        public int REASON_DETERMINATION_TYPE { get; set; }
        public string reason_determination_code { get; set; }
        public string transaction_type_code { get; set; }
        public bool is_blocked { get; set; }
        public bool is_active { get; set; }
    }

    public class reasonvm
    {
        public int REASON_DETERMINATION_ID { get; set; }
        public string REASON_DETERMINATION_NAME { get; set; }
        public int REASON_DETERMINATION_TYPE { get; set; }
        public string REASON_DETERMINATION_TYPE_NAME { get; set; }
        public bool is_blocked { get; set; }
        public bool is_active { get; set; }
    }
}
