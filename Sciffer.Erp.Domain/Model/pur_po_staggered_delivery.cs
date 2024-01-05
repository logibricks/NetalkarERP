using System;
using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
    public class pur_po_staggered_delivery
    {
        [Key]
        public int po_staggered_delivery_id { get; set; }
        public int po_id { get; set; }
        public DateTime staggered_date { get; set; }
        public double staggered_qty { get; set; }
    }
}
