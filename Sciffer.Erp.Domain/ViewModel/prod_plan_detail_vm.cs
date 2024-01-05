using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
   public class prod_plan_detail_vm
    {
        public int prod_plan_detail_id { get; set; }
        public int prod_plan_id { get; set; }
        public int machine_id { get; set; }
        public int item_id { get; set; }
        public decimal? quantity { get; set; }
        public bool is_active { get; set; }
        public string item_name { get; set; }
        public string machine_code { get; set; }
        public string prod_dates { get; set; }
        public DateTime prod_date { get; set; }
        public int? supervisor_id { get; set; }
        public int? shift_id { get; set; }
        public string shift_code { get; set; }
        public string supervisor_code { get; set; }
        public decimal? target_qty { get; set; }
    }
}
