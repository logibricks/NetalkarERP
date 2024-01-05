using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
   public class ref_incentive_status
    {
        [Key]
        public int incentive_status_id { get; set; }
        public string incentive_status_code { get; set; }
        public string incentive_status_name { get; set; }
    }
}
