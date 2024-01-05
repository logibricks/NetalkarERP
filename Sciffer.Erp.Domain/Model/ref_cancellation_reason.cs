using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Sciffer.Erp.Domain.Model
{
   public class ref_cancellation_reason
    {
        [Key]
        public int cancellation_reason_id { get; set; }
        [Required(ErrorMessage = "Transaction is required")]
        [Display(Name = "Transaction")]
        public int module_form_id { get; set; }
        [Required(ErrorMessage = "Reason is required")]
        [Display(Name = "Notification Description")]
        public string cancellation_reason { get; set; }
        public bool is_active { get; set; }
        public bool is_blocked { get; set; }
        public int created_by { get; set; }
        public DateTime created_ts { get; set; }
    }
    public class ref_cancellation_reason_vm
    {
        [Key]
        public int cancellation_reason_id { get; set; }
        public int module_form_id { get; set; }
        public string module_form_name { get; set; }
        public string cancellation_reason { get; set; }
        public bool is_active { get; set; }
        public bool is_blocked { get; set; }
        public int created_by { get; set; }
        public DateTime created_ts { get; set; }
    }
}
