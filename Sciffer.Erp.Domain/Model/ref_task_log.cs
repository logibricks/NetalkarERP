using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
  public  class ref_task_log
    {
      [Key]
      public  int task_log_id { get; set; }
      public int? task_id { get; set; }
      public int? old_status_id { get; set; }
      public int?  new_status_id { get; set; }
      public int?  created_by { get; set; }
      public DateTime? created_ts { get; set; }
      public string oroginal_attachment { get; set; }
      public string new_attachment { get; set; }
    }
}
