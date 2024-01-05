using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class pur_grn_detail_tax_element
    {
        [Key]
        [Column(Order = 0)]
        public int grn_id { get; set; }
        [Key]
        [Column(Order = 1)]
        public int grn_detail_id { get; set; }
        public int tax_element_id { get; set; }
        public double tax_element_rate { get; set; }
        public double tax_element_value { get; set; }

    }
}
