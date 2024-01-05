using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class pur_po_detail_tax_element
    {
        [Key]
        [Column(Order = 0)]
        public int po_id { get; set; }
        [Key]
        [Column(Order = 1)]
        public int po_detail_id { get; set; }
        [Key]
        [Column(Order = 2)]
        public int tax_element_id { get; set; }
        public double tax_element_rate { get; set; }
        public double tax_element_value { get; set; }
    }
}
