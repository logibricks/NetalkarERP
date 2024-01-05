using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sciffer.Erp.Domain.Model
{
     public class ref_pm_notification
    {
        [Key]
        public int notification_id { get; set; }
        [Display(Name ="Category *")]
        public int category_id { get; set; }
        [Display(Name = "Notification Number *")]
        public string doc_number { get; set; }
        [Display(Name = "Notification Type *")]
        public int notification_type { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Notification Date *")]
        public DateTime notification_date { get; set; }
        [Display(Name = "Notification Description")]
        [DataType(DataType.MultilineText)]
        public string notification_description { get; set; }
        [Display(Name = "Plant *")]
        public int plant_id { get; set; }
        [Display(Name = "Machine Code *")]
        public int machine_id { get; set; }
        [Display(Name = "Notified By *")]
        public int employee_id { get; set; }
        [Display(Name = "Malfunction Start Date *")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime start_date { get; set; }
        [Display(Name = "Malfunction End Date *")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime end_date { get; set; }
        [Display(Name = "Malfunction Start Time *")]
        [DataType(DataType.Time)]
        public TimeSpan start_time { get; set; }
        [Display(Name = "Malfunction End Time *")]
        [DataType(DataType.Time)]
        public TimeSpan end_time { get; set; }
        [Display(Name = "Detailed Problem *")]
        [DataType(DataType.MultilineText)]
        public string detail_problem { get; set; }
        [Display(Name = " Attachment *")]
        public string problem_attachment { get; set; }
        [Display(Name = "Detailed Solution")]
        [DataType(DataType.MultilineText)]
        public string detail_solution { get; set; }
        [Display(Name = "Attended By *")]
        public string attended_by { get; set; }
        [Display(Name = "Reviewed By *")]
        public string reviewed_by { get; set; }
        [Display(Name = "Whether Breakdown *")]
        public bool is_breakdown { get; set; }
      
        [Display(Name = "Actual Breakdown Start Date *")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime breakdown_start_date { get; set; }
        [Display(Name = "Actual Breakdown End Date *")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime breakdown_end_date { get; set; }
        [Display(Name = "Actual Breakdown Start Time *")]
        [DataType(DataType.Time)]
        public TimeSpan breakdown_start_time { get; set; }
        [Display(Name = "Actual Breakdown End Time *")]
        [DataType(DataType.Time)]
        public TimeSpan breakdown_end_time { get; set; }
        public int? created_by { get; set; }
        public DateTime? created_ts { get; set; }
        public bool? open_close { get; set; }
        public int? closed_by { get; set; }
        public DateTime? closed_ts { get; set; }
        public string attachment { get; set; }
        [NotMapped]
        public HttpPostedFileBase FileUpload { get; set; }
        public string attended_by_id { get; set; }
        public int? reviewed_by_id { get; set; }




        [Display(Name = "Attending Date *")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? attending_date { get; set; }

        [Display(Name = "Attending Time *")]
        [DataType(DataType.Time)]
        public TimeSpan? attending_time { get; set; }
        [Display(Name = "Under Taken By  *")]
        public int? under_taken_by_id { get; set; }


        [Display(Name = "Operator")]

        public int? operator_id { get; set; }

        [Display(Name = "Malfunction Closure Date  *")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? malfunction_closure_date { get; set; }

        [Display(Name = "Malfunction Closure Time *")]
        [DataType(DataType.Time)]
        public TimeSpan? malfunction_closure_time { get; set; }
       
        
        [Display(Name = "Status")]

        public int? status_id { get; set; }
        //public int? user_id { get; set; }


        //public int plan_breakdown_order_id { get; set; }
        

    }

}
