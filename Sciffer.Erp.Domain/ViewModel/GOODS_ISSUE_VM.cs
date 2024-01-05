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
    public class GOODS_ISSUE_VM
    {
        [Key]
        public int? goods_issue_id { get; set; }

        [Display(Name = "Goods Issue Number")]
        public string goods_issue_number { get; set; }

        public int document_numbring_id { get; set; }
        [ForeignKey("document_numbring_id")]
        public virtual ref_document_numbring ref_document_numbring { get; set; }

        [Display(Name = "Posting Date")]
        [DataType(DataType.Date)]        
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime posting_date { get; set; }

        [Display(Name = "Document Date")]
        [DataType(DataType.Date)]        
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime document_date { get; set; }

        public int plant_id { get; set; }
        [ForeignKey("plant_id")]
        public virtual REF_PLANT REF_PLANT { get; set; }

        public string plant_name { get; set; }
        public string ref1 { get; set; }

        [Display(Name = "Remarks")]
        [DataType(DataType.MultilineText)]
        public string remarks { get; set; }
        [NotMapped]
        [Display(Name = "Attachment")]
        public HttpPostedFileBase FileUpload { get; set; }

        public string attachment { get; set; }
        public bool is_active { get; set; }
        public string deleteids { get; set; }      

        public int? document_id { get; set; }
        public string mrn_number { get; set; }
        public string category_name { get; set; }

        public string document_code { get; set; }

        public virtual List<goods_issue_detail> goods_issue_detail { get; set; }
        public virtual List<goods_issue_detail_tag> goods_issue_detail_tag { get; set; }
        public virtual List<goods_issue_detail_nontagitems> goods_issue_detail_nontagitems { get; set; }
        public virtual List<goods_issue_detail_tagitems> goods_issue_detail_tagitems { get; set; }

        public List<int> goods_issue_detail_id1 { get; set; }
        public List<int> item_id1 { get; set; }
        public List<int> batch_id1 { get; set; }
        public List<int> sloc_id1 { get; set; }
        public List<int> bucket_id1 { get; set; }
        public List<int> gl_ledger_id1 { get; set; }
        public List<double> issue_quantity1 { get; set; }
        public List<double> rate1 { get; set; }
        public List<double> value1 { get; set; }
        public List<int> document_detail_id1 { get; set; }
        public List<int> uom_id1 { get; set; }
        public List<int> batch_detail_id1 { get; set; }
        public List<bool> is_active1 { get; set; }

        public List<int> batch_item_id { get; set; }
        public List<int> batch_batch_id { get; set; }
        public List<double> batch_issue_quantity { get; set; }
        public List<string> batch_number { get; set; }
        public List<int> reason_id { get; set; }
        public List<int> machine_id { get; set; }
        public string header_reference { get; set; }
        public string item_name { get; set; }
        public string category { get; set; }

        public string item_quantity { get; set; }

    }
    public class goods_issue_detail_nontagitems
    {
        public string item_code { get; set; }
        public string item_desc { get; set; }
        public string reason { get; set; }
        public string batch_number { get; set; }
        public string quantity { get; set; }
        public string rate { get; set; }
        public string value { get; set; }
        public string bucket { get; set; }
        public string sloc { get; set; }
        public string mrn_qty { get; set; }
        public string machine_code { get; set; }
        public string batch_bal_qty { get; set; }
    }
    public class goods_issue_detail_tagitems
    {
        public string item_code { get; set; }
        public string item_desc { get; set; }
        public string reason { get; set; }
        public string batch_number { get; set; }
        public string quantity { get; set; }
        public string rate { get; set; }
        public string value { get; set; }
        public string bucket { get; set; }
        public string sloc { get; set; }
        public string mrn_qty { get; set; }
        public string machine_code { get; set; }
        public string tag_no { get; set; }
        public string tag_qty { get; set; }
    }
}
