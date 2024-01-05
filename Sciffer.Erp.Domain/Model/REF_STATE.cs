using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
    public class REF_STATE
    {
        [Key]
        public int STATE_ID { get; set; }
        [Required]
        [Display(Name = "State Name")]
        public string STATE_NAME { get; set; }
        public virtual REF_COUNTRY REF_COUNTRY { get; set; }
        [Required]
        [Display(Name = "Country")]
        public int COUNTRY_ID { get; set; }
        public string state_ut_code { get; set; }
        public bool is_active { get; set; }
        public bool is_blocked { get; set; }
    }
    public class state_vm
    {
        public int state_id { get; set; }
        public string state_name { get; set; }
        public string country_name { get; set; }
        public int Country_id { get; set; }
        public string state_ut_code { get; set; }
        public bool is_active { get; set; }
        public bool is_blocked { get; set; }
    }
}
