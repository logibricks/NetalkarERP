using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class cycle_time_excel
    {
        public int cycle_time_id { get; set; }
        public int item_id { get; set; }
        public int operation_id { get; set; }
        public int machine_id { get; set; }
        public int cycle_time { get; set; }
        public int other { get; set; }
        public int loading_unloading { get; set; }

        public int? total_cycle_time { get; set; }
        public DateTime effective_date { get; set; }
        public decimal? incentive_rate  { get; set; }

        public string item_name { get; set; }
        public string machine_name { get; set; }
        public string process_name { get; set; }
        public string MyProperty { get; set; }
        public bool? is_active { get; set; }
    }
}
