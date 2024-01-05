using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
   public class mfg_operator_incentive
    {
        [Key]
        public int mfg_operator_incentive_id { get; set; }
        public DateTime date { get; set; }
        public int shift_id { get; set; }
        public int user_id { get; set; }
        public bool is_incentive_appl { get; set; }
        public decimal incentive_amt { get; set; }
        public int approval_status_id { get; set; }


    }
}
