using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
    public class REF_NOTIFICATION_TYPE
    {
        [Key]
        public int NOTIFICATION_ID { get; set; }
        [Required(ErrorMessage = "notification type is required")]
        [Display(Name = "Notification Type")]
        public string NOTIFICATION_TYPE { get; set; }
        [Required(ErrorMessage = "notification description is required")]
        [Display(Name = "Notification Description")]
        public string NOTIFICATION_DESCRIPTION { get; set; }
        public bool is_active { get; set; }
        public bool is_blocked { get; set; }
        public int created_by { get; set; }
        public DateTime created_ts { get; set; }
    }
}
