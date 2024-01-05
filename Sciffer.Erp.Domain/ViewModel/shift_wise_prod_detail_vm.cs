using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
   public class shift_wise_prod_detail_vm
    {
        public int shift_wise_production_detail_id { get; set; }
        public int shift_wise_production_id { get; set; }
        public int machine_id { get; set; }
        public int plant_id { get; set; }
        public decimal? target_qty { get; set; }
        public bool is_active { get; set; }
        public string plant_code { get; set; }
        public string machine_code { get; set; }
        public string prod_dates { get; set; }
        public DateTime prod_date { get; set; }    
        public int? shift_id { get; set; }
        public string shift_code { get; set; }
        
    }
}
