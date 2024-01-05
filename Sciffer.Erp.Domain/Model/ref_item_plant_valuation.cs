using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public  class ref_item_plant_valuation
    {
        [Key]
        [Column(Order = 0)]
        public int item_id { get; set; }
        [Key]
        [Column(Order = 1)]
        public int plant_id { get; set; }
        [ForeignKey("plant_id")]
        public virtual REF_PLANT REF_PLANT { get; set; }
        public double item_value { get; set; }
    }
}
