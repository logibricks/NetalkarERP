using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
   public class prod_plan_detail
    {
        [Key]
        public int prod_plan_detail_id { get; set; }
        public int prod_plan_id { get; set; }
        public int machine_id { get; set; }
        public int item_id { get; set; }
        public decimal? quantity { get; set; }
        public bool is_active { get; set; }
        public DateTime? prod_date { get; set; }
        public int? supervisor_id { get; set; }
        public int? shift_id { get; set; }
    }
}
