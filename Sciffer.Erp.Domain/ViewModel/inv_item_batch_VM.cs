using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class inv_item_batch_VM
    {
        public int item_batch_id { get; set; }
        public string batch_number { get; set; }
        public bool batch_yes_no { get; set; }
        public int batch_manual_yes_no { get; set; }
        public string document_code { get; set; }
        public int document_id { get; set; }
        public int? document_detail_id { get; set; }
        public int item_id { get; set; }       
        public DateTime? expirary_date { get; set; }
        public IList<inv_item_batch_detail> inv_item_batch_detail { get; set; }
        public string item_name { get; set; }
    }
}
