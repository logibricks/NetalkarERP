using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class inv_item_batch
    {
        [Key]
        public int item_batch_id { get; set; }
        public string batch_number { get; set; }
        public bool batch_yes_no { get; set; }
        public int batch_manual_yes_no { get; set; }
        public string document_code { get; set; }
        public int document_id { get; set; }
        public int? document_detail_id { get; set; }
        public int item_id { get; set; }
        [ForeignKey("item_id")]
        public virtual REF_ITEM REF_ITEM { get; set; }
        public DateTime? expirary_date { get; set; }

    }
}
