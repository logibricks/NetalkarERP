using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_plan_breakdown_service_cost
    {
        [Key]
        public int plan_breakdown_service_cost_id { get; set; }
        public int purchase_invoice_id { get; set; }
        [ForeignKey("purchase_invoice_id")]
        public virtual pur_pi pur_pi { get; set; }
        public DateTime posting_date { get; set; }
        public int vendor_id { get; set; }
        [ForeignKey("vendor_id")]
        public virtual REF_VENDOR REF_VENDOR { get; set; }
        public double amount { get; set; }
        public int plan_breakdown_order_id { get; set; }
        [ForeignKey("plan_breakdown_order_id")]
        public virtual ref_plan_breakdown_order ref_plan_breakdown_order { get; set; }
    }
}
