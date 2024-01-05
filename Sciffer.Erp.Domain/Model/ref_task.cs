using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
   public  class ref_task
    {
        [Key]
        public int task_id { get; set; }
       
        public int? task_type_id { get; set; }

       
        public int? task_doer_id { get; set; }

        public int? task_reviewer_id { get; set; }

     
        public DateTime? due_date { get; set; }

        
        public string task_name { get; set; }

       
        public bool is_recurring { get; set; }

       
        public int? task_periodicity_id { get; set; }

       
        public int? remind_before_days { get; set; }

       
        public int? first_escalation_days { get; set; }
    
        public int? second_escalation_days { get; set; }     
        public string task_details { get; set; }
        public string oroginal_attachment { get; set; }
        public int? parent_task_id { get; set; }
        public int? status_id { get; set; }
        public string new_remarks { get; set; }
        public string new_attachment { get; set; }   
        public bool is_active { get; set; }
        public int? created_by { get; set; }
        public DateTime? created_ts { get; set; }
        public int? modified_by { get; set; }
        public DateTime? modified_ts { get; set; }
        public string document_no { get; set; }
    }
}
