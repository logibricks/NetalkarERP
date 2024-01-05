using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_vendor_item_group
    {
        [Key]
        [Column(Order = 0)]
        public int vendor_id { get; set; }
        [ForeignKey("vendor_id")]
        public virtual REF_VENDOR REF_VENDOR { get; set; }
        [Key]
        [Column(Order = 1)]
        public int item_group_id { get; set; }
        [ForeignKey("item_group_id")]
        public virtual REF_ITEM_GROUP REF_ITEM_GROUP { get; set; }
        public double rate { get; set; }
    }
}
