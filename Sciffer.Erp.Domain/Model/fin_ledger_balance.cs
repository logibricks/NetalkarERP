using System;
using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
    public  class fin_ledger_balance
    {
        [Key]
        public int sub_ledger_id { get; set; }
        public double op_balance { get; set; }
        public string op_balance_ts { get; set; }
        public double cu_balance { get; set; }
        public DateTime cu_balance_ts { get; set; }
        public int modify_user { get; set; }
        public DateTime modify_ts { get; set; }

    }
}
