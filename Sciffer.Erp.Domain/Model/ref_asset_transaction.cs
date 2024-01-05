using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
   public class ref_asset_transaction
    {
        [Key]
        public int asset_ledger_id { get; set; }
        public int? asset_id { get; set; }
        public int? dep_area_id { get; set; }
        public string transaction_code { get; set; }
        public DateTime? posting_date { get; set; }
        public decimal? value { get; set; }
        public decimal? cum_value { get; set; }
        public int? dep_area_posting_period_id { get; set; }
        public int? created_by { get; set; }
        public DateTime? created_ts { get; set; }
        public bool is_active { get; set; }
        public int? modify_by { get; set; }
        public DateTime? modify_ts { get; set; }


    }
}
