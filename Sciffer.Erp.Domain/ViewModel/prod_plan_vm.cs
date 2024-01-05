using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
   public class prod_plan_vm
    {
        public int prod_plan_id { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Posting Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? posting_date { get; set; }
        public DateTime? prod_date { get; set; }
    }
}
