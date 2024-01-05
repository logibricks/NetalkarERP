using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
   public class Report_OEE_vm
    {
        public DateTime? prod_date { get; set; }
        public string machine_code { get; set; }
        public string user_code { get; set; }
        public string item_code { get; set; }
        public string shift_code { get; set; }
        public decimal? avl_time { get; set; }
        public decimal? production { get; set; }
        public decimal? net_prod_target { get; set; }
        public decimal? actual_production { get; set; }
        public decimal? mac_breakdown { get; set; }
        public decimal? pm { get; set; }
        public decimal? no_power { get; set; }
        public decimal? no_operator { get; set; }
        public decimal? no_load { get; set; }
        public decimal? setup { get; set; }
        public decimal? restart { get; set; }
        public decimal? tool_change { get; set; }
        public decimal? quality_check { get; set; }
        public decimal? no_plan { get; set; }
        public decimal? training { get; set; }
        public decimal? jh { get; set; }
        public decimal? remarks { get; set; }
        public decimal? total_loss_time { get; set; }
        public decimal? net_avl_time { get; set; }
        public decimal? opp_efficiency { get; set; }
        public decimal? mac_utilization { get; set; }
        public decimal? oee { get; set; }
        public int cycle_time { get; set; }
    }
}
