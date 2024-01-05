using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
  public  class REF_VENDOR_CONTACTS
    {
        [Key]
        public int? VENDOR_CONTACT_ID { get; set; }
        public int VENDOR_ID { get; set; }
        [ForeignKey("VENDOR_ID")]
        public REF_VENDOR REF_VENDOR { get; set; }
        [Required]
        [Display(Name = "Contact Name")]
        public string CONTACT_NAME { get; set; }
        [Display(Name = "Designation")]
        public string DESIGNATION { get; set; }
        public string EMAIL_ADDRESS { get; set; }
        [Display(Name = "Mobile No.")]       
        public string MOBILE_NO { get; set; }
        [Display(Name = "Phone No.")]      
        public string PHONE_NO { get; set; }
        [Display(Name = "Send SMS")]
        public bool SEND_SMS_FLAG { get; set; }
        [Display(Name = "Send Email")]
        public bool SEND_EMAIL_FLAG { get; set; }
        public bool? IS_ACTIVE { get; set; }
    }
}
