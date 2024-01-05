using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class rejection_receipt_VM
    {
        public int? reject_receipt_id { get; set; }
        public string reject_receipt_number { get; set; }
        public int category_id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime posting_date { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime document_date { get; set; }

        public int plant_id { get; set; }
        public string header_remarks { get; set; }
        public bool is_active { get; set; }

        public string  prod_order_id { get; set; }

        public int storage_location_id { get; set; }
        public string deleteids { get; set; }
        public string category_name { get; set; }
        public string plant_name { get; set; }
        public string prod_order_no { get; set; }
        public List<string> out_item_id { get; set; }
        public List<string> uom_id { get; set; }
        public List<string> sloc_id { get; set; }
        public List<string> po_quantity { get; set; }
        public List<string> reject_receipt_detail_id { get; set; }
        public List<string> prod_order_detail_id { get; set; }
        public List<string> batch_id { get; set; }
        public List<string> tag_id { get; set; }
        public List<int> reason_id { get; set; }
        public virtual IList<rejection_receipt_detail> rejection_receipt_detail { get; set; }
        public virtual IList<reject_receipt_details> reject_receipt_details { get; set; }
        public string item_name { get; set; }
    }

    public class reject_receipt_details
    {
        public string item_code { get; set; }
        public string item_desc { get; set; }
        public string uom_code { get; set; }
        public string uom_name { get; set; }
        public string sloc_code { get; set; }
        public string sloc_desc { get; set; }
        public string batch_no { get; set; }
        public string tag_no { get; set; }
        public string quantity { get; set; }

        public string reason { get; set; }
        public string prod_order_no { get; set; }
    }
}
