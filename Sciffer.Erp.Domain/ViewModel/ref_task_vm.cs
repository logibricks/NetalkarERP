using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace Sciffer.Erp.Domain.ViewModel
{
   public class ref_task_vm
    {
      public int task_id { get; set; }
      [Display(Name ="Task Type *")]
      public int? task_type_id { get; set; }

      [Display(Name = "Task Doer *")]
      public int? task_doer_id { get; set; }

      [Display(Name = "Task Reviewer *")]
      public int? task_reviewer_id { get; set; }

      [Display(Name = "Due Date *")]
      [DataType(DataType.Date)]
      [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
      public DateTime? due_date { get; set; }

      [Display(Name = "Task Name *")]
      public string task_name { get; set; }

      [Display(Name = "Whether Recurring ? ")]
      public bool is_recurring { get; set; }

      [Display(Name = "Periodicity")]
      public int? task_periodicity_id { get; set; }

      
      [Display(Name = "Remind How many days before due date? *")]
      
      public int?  remind_before_days { get; set; }

     
      [Display(Name = "1st Escalation After How many days? *")]
      public int? first_escalation_days { get; set; }

     
      [Display(Name = "2nd Escalation After How many days? *")]
      public int? second_escalation_days { get; set; }

      [Display(Name = "Task Details *")]
      [DataType(DataType.MultilineText)]
      public string task_details { get; set; }

      [Display(Name = "Attachment")]
      public string attachment { get; set; }
      [NotMapped]
      public HttpPostedFileBase FileUpload { get; set; }
      [NotMapped]
      public HttpPostedFileBase FileUpload1 { get; set; }

        [Display(Name = "Category *")]
      public int? task_category_id { get; set; }
      
      public string document_no { get; set; }
        // oroginal_attachment
      public int?  parent_task_id { get; set; }

      [Display(Name = "Status *")]
      public int?  status_id { get; set; }

      [Display(Name = "Remarks")]
      [DataType(DataType.MultilineText)]
      public string new_remarks { get; set; }

      [Display(Name = "Attachment")]
      public string new_attachment { get; set; }     
      public bool is_active { get; set; }

      public string task_type { get; set; }
      public string reviewer { get; set; }
      public string doer { get; set; }
      public string due_date1 { get; set; }
      public string recurring { get; set; }
      public string periodicity_name { get; set; }
      public DateTime? created_ts { get; set; }
      public DateTime? modified_ts { get; set; }
      public string status { get; set; }
      public int? old_status_id { get; set; }
      public int? task_log_id { get; set; }

     public List<Task_Status_vm>Status_log { get; set; }

    }

    public class Task_Status_vm
    {
        public int? old_status_id { get; set; }
        public int? new_status_id { get; set; }
        public string old_status { get; set; }
        public string new_status { get; set; }
        public string created_ts { get; set; }
        public string oroginal_attachment { get; set; }
        public string new_remarks { get; set; }
        public string new_attachment { get; set; }
        public int? task_log_id { get; set; }
    }
}
