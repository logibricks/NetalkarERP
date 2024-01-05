using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class sal_quotation_detail_tax
    {
        [Key]
        [Column(Order =0)]
        public int quotation_detail_id { get; set; }
        [ForeignKey("quotation_detail_id")]
        public  virtual SAL_QUOTATION_DETAIL SAL_QUOTATION_DETAIL { get; set; }
        [Key]
        [Column(Order = 1)]
        public int tax_element_detail_id { get; set; }
        [ForeignKey("tax_element_detail_id")]
        public virtual ref_tax_element_detail ref_tax_element_detail { get; set; }
        public double tax_element_rate { get; set; }
        public double tax_element_value { get; set; }
        
    }
}
