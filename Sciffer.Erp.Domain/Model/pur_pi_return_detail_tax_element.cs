using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class pur_pi_return_detail_tax_element
    {
        public int pi_id { get; set; }
        public int pi_detail_id { get; set; }
        public int tax_element_id { get; set; }
        public double tax_element_rate { get; set; }
        public double tax_element_value { get; set; }
    }
}
