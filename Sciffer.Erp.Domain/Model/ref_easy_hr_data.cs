using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
   public class ref_easy_hr_data
    {
        [Key]
        public Int64 easy_hr_data_id { get; set; }
        public DateTime shift_date { get; set; }
        public int operator_id { get; set; }
        public string operator_code { get; set; }
        public DateTime in_time { get; set; }
        public DateTime out_time { get; set; }
        public int shift_id { get; set; }
        public double time_diff { get; set; }
    }
}
