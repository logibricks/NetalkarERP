using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
   public class ref_plant_notification
    {
        [Key]
        public int plant_notification_id { get; set; }
        [Display(Name = "Notification Category *")]
        public int category_id { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Notification Date *")]
        public DateTime notification_date { get; set; }
        [Display(Name = "Notification Type *")]
        public int notification_type { get; set; }
        [Display(Name = "Notification Number *")]
        public string doc_number { get; set; }
        [Display(Name = "Notification For *")]
        public string notification_for { get; set; }
        [Display(Name = "Plant *")]
        public int plant_id { get; set; }
        [Display(Name = "Machine Code *")]
        public int machine_id { get; set; }
        [Display(Name = "Order *")]
        public int order_id { get; set; }
        [Display(Name = "Planned Breakdown Start Date *")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime pl_breakdown_start_date { get; set; }
        [Display(Name = "Planned Breakdown End Date *")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime pl_breakdown_end_date { get; set; }
        [Display(Name = "Planned Breakdown Start Time *")]
        [DataType(DataType.Time)]
        public TimeSpan pl_breakdown_start_time { get; set; }
        [Display(Name = "Planned Breakdown End Time   *")]
        [DataType(DataType.Time)]
        public TimeSpan pl_breakdown_end_time { get; set; }
        [Display(Name = "Actual Breakdown Start Date *")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? a_breakdown_start_date { get; set; }
        [Display(Name = "Actual Breakdown End Date *")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? a_breakdown_end_date { get; set; }
        [Display(Name = "Actual Breakdown Start Time *")]
        [DataType(DataType.Time)]
        public TimeSpan? a_breakdown_start_time { get; set; }
        [Display(Name = "Actual Breakdown End Time *")]
        [DataType(DataType.Time)]
        public TimeSpan? a_breakdown_end_time { get; set; }
        public int created_by { get; set; }
        public DateTime created_ts { get; set; }
    }
}
