using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
   public class prod_downtime
    {
        [Key]
        public int prod_downtime_id { get; set; }
        public int prod_plan_detail_id { get; set; }
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
        public int? created_by { get; set; }
        public DateTime? created_ts { get; set; }
        public int? modified_by { get; set; }
        public DateTime? modified_ts { get; set; }
        public bool is_active { get; set; }
    }
}
