using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
   public class ref_mfg_operator_incentive_appl
    {
        [Key]
        public int mfg_operator_incentive_appl_id { get; set; }
        public int user_id { get; set; }
        public bool? is_incentive_applicable { get; set; }
    }
}
