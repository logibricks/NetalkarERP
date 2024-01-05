using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
   public class pla_transfer_batch
    {
        [Key]
        public int pla_transfer_batch_id { get; set; }
        public int batch_item_id { get; set; }
        public int batch_id { get; set; }
        public string batch_number { get; set; }
        public double issue_batch_quantity { get; set; }
        public int? batch_detail_id { get; set; }
        public int pla_transfer_detail_id { get; set; }
    }
}
