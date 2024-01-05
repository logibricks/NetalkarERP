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
    public class ProdIssueVM
    {
        public int? prod_issue_id { get; set; }

        public string prod_issue_number { get; set; }
        [Display(Name = "Category")]
        public int category_id { get; set; }
        [Display(Name = "Posting Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime posting_date { get; set; }

        [Display(Name = "Document Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]        
        public DateTime document_date { get; set; }

        [Display(Name = "Plant")]
        public int plant_id { get; set; }
        [Display(Name = "Remarks")]
        public string remarks { get; set; }
        public bool is_active { get; set; }
        [Display(Name = "Production Order")]
        public int? prod_order_id { get; set; }

        public string deleteids { get; set; }
        public string plant_name { get; set; }
        public string category_name { get; set; }
        public string prod_order_no { get; set; }

        [NotMapped]
        public HttpPostedFileBase FileUpload { get; set; }
        public string attachment { get; set; }

        public virtual IList<prod_issue_detail> prod_issue_detail { get; set; }
        public virtual IList<prod_issue_detail_tag> prod_issue_detail_tag { get; set; }
        public virtual List<Prod_order_issue_detail_tagitems> Prod_order_issue_detail_tagitems { get; set; }
        public virtual List<Prod_order_issue_detail_nontagitems> Prod_order_issue_detail_nontagitems { get; set; }
        public List<string> tag_prod_issue_detail_tag_id { get; set; }
        public List<string> tag_prod_order_detail_id { get; set; }
        public List<string> tag_item_batch_detail_id { get; set; }
        public List<string> tag_in_item_id { get; set; }
        public List<string> tag_tag_id { get; set; }
        public List<string> tag_batch_id { get; set; }
        public List<string> tag_quantity { get; set; }
        public List<string> tag_rate { get; set; }
        public List<string> tag_value { get; set; }       

        public List<string> ntag_prod_issue_detail_id { get; set; }
        public List<string> ntag_prod_order_detail_id { get; set; }
        public List<string> ntag_item_batch_detail_id { get; set; }
        public List<string> ntag_in_item_id { get; set; }
        public List<string> ntag_batch_id { get; set; }
        public List<string> ntag_quantity { get; set; }
        public List<string> ntag_rate { get; set; }
        public List<string> ntag_value { get; set; }
        public string item_name { get; set; }
    }
    public class Prod_order_issue_detail_tagitems
    {
        public string item_code { get; set; }
        public string item_desc { get; set; }
        public string batch_number { get; set; }
        public string tag_no { get; set; }
        public string required_qty { get; set; }
        public string rate { get; set; }
        public string value { get; set; }
    }
    public class Prod_order_issue_detail_nontagitems
    {
        public string item_code { get; set; }
        public string item_desc { get; set; }
        public string batch_number { get; set; }
        public string required_qty { get; set; }
        public string rate { get; set; }
        public string value { get; set; }
    }
}
