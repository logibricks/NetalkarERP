using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class fin_ledger_detail
    {
        [Key]
        public int fin_ledger_detail_id { get; set; }
        public int fin_ledger_id { get; set; }
        public int sub_ledger_id { get; set; }
        public float amount { get; set; }
        //public float? dr_amount { get; set; }
        //public float? cr_amount { get; set; }
        public float amount_local { get; set; }
        public bool is_active { get; set; }
        //public int party_id { get; set; }
        public int cost_center_id { get; set; }
        [ForeignKey("cost_center_id")]
        public virtual ref_cost_center ref_cost_center { get; set; }
        public string line_remarks { get; set; }
    }
}
