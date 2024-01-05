using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class MRNDetailListVM
    {
        public int item_id { get; set; }
        public string item_name { get; set; }
        public string order_number { get; set; }
        public int material_requision_note_detail_id { get; set; }       
        public double mrn_quantity { get; set; }
        public int machine { get; set; }
        public string machine_name { get; set; }
        public double item_rate { get; set; }
        public int? item_batch_detail_id { get; set; }
        public double batch_qty { get; set; }
        public string batch_number { get; set; }
        public int? tag_id { get; set; }
        public double tag_quantity { get; set; }
        public string tag_no { get; set; }
        public int item_category_id { get; set; }
        public string item_category_name { get; set; }
        public int sending_sloc { get; set; }
        public double balance_quantity { get; set; }
        public int? item_batch_id { get; set; }
        public double rate { get; set; }
        public int reason { get; set; }
        public string REASON_DETERMINATION_NAME { get; set; }
    }

}
