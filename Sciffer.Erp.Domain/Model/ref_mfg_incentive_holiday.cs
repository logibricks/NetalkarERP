using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_mfg_incentive_holiday
    {
        [Key]
        public DateTime holiday_date { get; set; }
        public string holiday_desc { get; set; }
    }
}
