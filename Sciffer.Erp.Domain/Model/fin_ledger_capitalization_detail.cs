using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class fin_ledger_capitalization_detail
    {
        [Key]
        public int fin_ledger_capitalization_detail_id { get; set; }
        public int fin_ledger_capitalization_id { get; set; }
        public int fin_ledger_detail_id { get; set; }
        public decimal amount { get; set; }

    }
}
