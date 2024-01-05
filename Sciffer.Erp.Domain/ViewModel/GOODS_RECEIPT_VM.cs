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
    public class GOODS_RECEIPT_VM
    {
        [Key]
        public int? goods_receipt_id { get; set; }
        [Display(Name = "Goods Receipt Number")]
        public string goods_receipt_number { get; set; }
        [Display(Name = "Status")]

        public int? status_id { get; set; }

        public int category_id { get; set; }
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

        [DataType(DataType.MultilineText)]
        public string ref1 { get; set; }

        [Display(Name = "Remarks")]
        [DataType(DataType.MultilineText)]
        public string remarks { get; set; }

        [Display(Name = "Cancellation Remarks")]
        [DataType(DataType.MultilineText)]
        public string cancellation_remarks { get; set; }
        [Display(Name = "Status")]
        public virtual ref_status ref_status { get; set; }
        public string status_name { get; set; }
        public int? cancellation_reason_id { get; set; }
        [Display(Name = "Cancelled Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? cancelled_date { get; set; }
        [Display(Name = "Cancelled By")]
        public int? cancelled_by { get; set; }
        [Display(Name = "Last Modified Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? modify_ts { get; set; }
        [Display(Name = "Last Modified By")]
        public int? modify_by { get; set; }
        public string cancellation_reason { get; set; }
        public bool is_active { get; set; }

        public string deleteids { get; set; }
         
        public virtual List<goods_receipt_detail> goods_receipt_detail { get; set; }
        public virtual List<good_receipt_detail> good_receipt_detail { get; set; }
        public string batch_number { get; set; }
        public string machine_name { get; set; }
        public string grn { get; set; }
        [Display(Name = "Attachment")]
        public string attachment { get; set; }
        public string category_name { get; set; }

        [NotMapped]
        public HttpPostedFileBase FileUpload { get; set; }

        public List<string> goods_detail_id { get; set; }
        public List<string> sr_no { get; set; }        
        public List<string> item_id { get; set; }        
        public List<string> storage_location_id { get; set; }
        public List<string> bucket_id { get; set; }
        public List<string> batch_yes_no { get; set; }
        public List<string> batch_manual { get; set; }
        public List<string> reason_determination_id { get; set; }
        public List<string> item_batch_id { get; set; }
        public List<string> general_ledger_id { get; set; }
        public List<string> quantity { get; set; }
        public List<string> uom_id { get; set; }
        public List<string> rate { get; set; }
        public List<string> value { get; set; }
        public List<string> remark { get; set; }
        public List<string> grn_id { get; set; }
        public string item_name { get; set; }
        public string item_quantity { get; set; }
    }
    public class good_receipt_detail
    {
        public string item_code { get; set; }
        public string item_desc { get; set; }
        public string reason { get; set; }
        public string sloc { get; set; }
        public string batch_yes_no { get; set; }
        public string batch { get; set; }
        public string quantity { get; set; }
        public string rate { get; set; }
        public string value { get; set; }
        public string gl { get; set; }
        public string grn { get; set; }
        public string remarks { get; set; }
        public string uom { get; set; }
        public string bucket { get; set; }
        public string batch_manual { get; set; }
    }
}
