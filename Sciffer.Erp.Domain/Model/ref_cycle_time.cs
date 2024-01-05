using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_cycle_time
    {
        [Key]
        public int cycle_time_id { get; set; }
        public int item_id { get; set; }
        public int machine_id { get; set; }
        public int process_id { get; set; }
        public int cycle_time { get; set; }
        public int loading_unloading { get; set; }
        public int others { get; set; }
        public int total { get; set; }
        public int created_by { get; set; }
        public DateTime created_ts { get; set; }
        public bool is_active { get; set; }
        public int modified_by { get; set; }
        public DateTime modified_ts { get; set; }
        public DateTime effective_date { get; set; }
        public decimal incentive_rate { get; set; }

    }
}
