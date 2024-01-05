using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class map_machine_breakdown_order
    {
        [Key]
        public int map_machine_breakdown_order_id { get; set; }
        public int machine_id { get; set; }
        public int plan_breakdown_order_id { get; set; }
        public bool? is_active { get; set; }
    }
}
