using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class ref_mfg_incentive_benchmark_vm
    {
        public int mfg_incentive_benchmark_id { get; set; }
        [Display(Name = "Plant *")]
        public int plant_id { get; set; }
        [Display(Name = "Machine *")]
        public int machine_id { get; set; }
        [Display(Name = "Operation *")]
        public int operation_id { get; set; }
        [Display(Name = "Item *")]
        public int item_id { get; set; }
        [Display(Name = "Benchmark Qty. *")]
        public double reporting_quantity { get; set; }
        [Display(Name = "Rate *")]
        public double incentive { get; set; }
        public int? created_by { get; set; }
        public DateTime? created_ts { get; set; }
        public int? modified_by { get; set; }
        public DateTime? modified_ts { get; set; }
        public int srno { get; set; }
        public string item { get; set; }
        public string machine { get; set; }
        public string operation { get; set; }
        public string plant { get; set; }
        public double per_hr_qty { get; set; }
    }
}
