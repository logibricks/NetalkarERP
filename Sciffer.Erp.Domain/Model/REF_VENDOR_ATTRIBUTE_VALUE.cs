using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
  public  class REF_VENDOR_ATTRIBUTE_VALUE
    {
        [Key]
        [Column(Order = 0)]
        public int VENDOR_ID { get; set; }
        [ForeignKey("VENDOR_ID")]
        public virtual REF_VENDOR REF_VENDOR { get; set; }
        [Key]
        [Column(Order = 1)]
        public int ATTRIBUTE_ID { get; set; }
        [ForeignKey("ATTRIBUTE_ID")]
        public virtual REF_ATTRIBUTES REF_ATTRIBUTES { get; set; }
        public string ATTRIBUTE_VALUE { get; set; }
    }
}
