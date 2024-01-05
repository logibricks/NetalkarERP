using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
   public  class REF_COUNTRY
    {
        [Key]
        public int COUNTRY_ID { get; set; }
        [Required(ErrorMessage ="country name is required")]
        [Display(Name = "Country Name")]
        public string COUNTRY_NAME { get; set; }
        [Required(ErrorMessage = "country code is required")]
        [Display(Name = "Country Code")]
        public string COUNTRY_CODE { get; set; }
        public bool is_active { get; set; }
        public bool is_blocked { get; set; }
    }
}
