using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace Sciffer.Erp.Domain.ViewModel
{
    public class prod_receipt_VM
    {

        [Key]
        public int? prod_receipt_id { get; set; }
        public string prod_receipt_number { get; set; }
        public int category_id { get; set; }
        [ForeignKey("category_id")]
        public virtual ref_document_numbring ref_document_numbring { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime posting_date { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime document_date { get; set; }
        //public DateTime posting_date { get; set; }
       // public DateTime document_date { get; set; }
        public int plant_id { get; set; }
        [ForeignKey("plant_id")]
        public virtual REF_PLANT REF_PLANT { get; set; }
        public string header_remarks { get; set; }
        public bool is_active { get; set; }

        public int prod_order_id { get; set; }
        //[ForeignKey("prod_order_id")]
        //public virtual mfg_prod_order mfg_prod_order { get; set; }

        public int storage_location_id { get; set; }
        [ForeignKey("storage_location_id")]
        public virtual REF_STORAGE_LOCATION REF_STORAGE_LOCATION { get; set; }

        public string deleteids { get; set; }               
        public string category_name { get; set; }
        public string plant_name { get; set; }
        public string prod_order_no { get; set; }
        public List<string> out_item_id { get; set; }
        public List<string> uom_id { get; set; }
        public List<string> sloc_id { get; set; }
        public List<string> po_quantity { get; set; }
        public List<string> prod_receipt_detail_id { get; set;}
        public List<string> prod_order_detail_id { get; set; }
        public List<string> batch_id { get; set; }
        public List<string> tag_id { get; set; }
        public string created_by { get; set; }
        public DateTime? created_ts { get; set; }

        public virtual IList<prod_receipt_detail> prod_receipt_detail { get; set; }
        public virtual IList<prod_receipt_details> prod_receipt_details { get; set; }
        public string item_name { get; set; }
    }
    public class prod_receipt_details
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
    }
}
