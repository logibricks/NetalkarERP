using Sciffer.Erp.Domain.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class fin_contra_entry_vm
    {
        public int contra_entry_id { get; set; }
        [Display(Name = "Category *")]
        public int category_id { get; set; }
        public string category_name { get; set; }
        [Display(Name = "Number")]
        public string document_no { get; set; }
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
        [Display(Name = "Transfer funds from *")]
        public int transfer_funds_from { get; set; }
        public string transfer_funds_from_name { get; set; }
        [Display(Name = "Transfer funds to *")]
        public int transfer_funds_to { get; set; }
        public int from_cash_bank { get; set; }
        public int to_cash_bank { get; set; }
        public string transfer_funds_to_name { get; set; }
        [Display(Name = "Transfer Amount *")]
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
        public bool? is_active { get; set; }
        [NotMapped]
        public HttpPostedFileBase FileUpload { get; set; }
        [Display(Name = "Cancellation Remarks")]
        [DataType(DataType.MultilineText)]
        public string cancellation_remarks { get; set; }
        [Display(Name = "Status")]
        public int? status_id { get; set; }
        public virtual ref_status ref_status { get; set; }
        public string status_name { get; set; }
        public int? cancellation_reason_id { get; set; }
        [Display(Name = "Cancelled Date")]
        public DateTime? cancelled_date { get; set; }
        [Display(Name = "Cancelled By")]
        public int? cancelled_by { get; set; }
        [Display(Name = "Last Modified Date")]
        public DateTime? modify_ts { get; set; }
        [Display(Name = "Last Modified By")]
        public int? modify_by { get; set; }
        [Display(Name = "Created By")]
        public int? created_by { get; set; }
        [Display(Name = "Created Date")]
        public DateTime? created_ts { get; set; }
    }
    public class Current_Balance
    {
        public double cu_balance { get; set; }
    }
}
