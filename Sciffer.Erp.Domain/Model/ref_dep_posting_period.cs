using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
   public class ref_dep_posting_period
    {
        [Key]
        public int dep_area_posting_period_id { get; set; }
        public int dep_area_id { get; set; }
        public int financial_year_id { get; set; }
        public string posting_periods_code { get; set; }
        public DateTime from_date { get; set; }
        public DateTime to_date { get; set; }
        public int status_id { get; set; }
        public DateTime created_ts { get; set; }
        public int created_by { get; set; }
        public int is_active { get; set; }

    }
}
