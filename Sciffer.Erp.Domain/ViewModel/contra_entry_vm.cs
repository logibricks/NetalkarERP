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
    public class contra_entry_vm
    {
        public int contra_entry_id { get; set; }
        public string document_numbering_name { get; set; }
        [Display(Name = "Category *")]
        public int document_numbering_id { get; set; }
        [Display(Name = "Number")]
        public string number { get; set; }
        [Display(Name = "Posting Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime posting_date { get; set; }
        [Display(Name = "Document Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? document_date { get; set; }
        [Display(Name = "Ref Doc. No")]
        public string ref_doc_no { get; set; }
        [Display(Name = "Currency")]
        public int curreny_id { get; set; }
        public string curreny_name { get; set; }
        [Display(Name = "Header Remarks")]
        [DataType(DataType.MultilineText)]
        public string header_remarks { get; set; }
        [Display(Name = "Transfer funds from")]
        public int transfer_funds_from { get; set; }
        public string transfer_funds_from_name { get; set; }
        [Display(Name = "Transfer funds to")]
        public int transfer_funds_to { get; set; }
        public string transfer_funds_to_name { get; set; }
        [Display(Name = "Transfer Amount")]
        public double transfer_amount { get; set; }
        [Display(Name = "Current Balance")]
        public double? current_balance_from { get; set; }
        [Display(Name = "Current Balance")]
        public double? current_balance_to { get; set; }
        [Display(Name = "Remarks")]
        [DataType(DataType.MultilineText)]
        public string reamrks { get; set; }
        [Display(Name = "Attachment")]
        public string attachment { get; set; }
        public bool is_active { get; set; }
        [NotMapped]
        public HttpPostedFileBase FileUpload { get; set; }
    }
}
