using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
   public class ref_mfg_incentive_benchmark
    {
        [Key]
        public int mfg_incentive_benchmark_id { get; set; }
        public int plant_id { get; set; }
        public int machine_id { get; set; }
        public int operation_id { get; set; }
        public int item_id { get; set; }
        public double reporting_quantity { get; set; }
        public double incentive { get; set; }
        public int? created_by { get; set; }
        public DateTime? created_ts { get; set; }
        public int? modified_by { get; set; }
        public DateTime? modified_ts { get; set; }
        public bool is_active { get; set; }

    }
}
