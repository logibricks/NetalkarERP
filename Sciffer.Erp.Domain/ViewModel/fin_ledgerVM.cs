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
    public class fin_ledgerVM
    {
        [Key]
        public int fin_ledger_id { get; set; }
        public string document_type_code { get; set; }
        [Display(Name = "Category")]
        public int category_id { get; set; }
        public int source_document_id { get; set; }
        public string document_no { get; set; }
        public string source_document_no { get; set; }
        public string ref_bill_no { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Posting Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ledger_date { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Document Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? document_date { get; set; }
        [Display(Name = "Amount")]
        public double ledger_amount { get; set; }
        public double ledger_amount_local { get; set; }
        [Display(Name = "Currency Indicator")]
        public int currency_id { get; set; }
        [ForeignKey("currency_id")]
        public virtual REF_CURRENCY REF_CURRENCY { get; set; }
        [Display(Name = "Header Remarks")]
        [DataType(DataType.MultilineText)]
        public string narration { get; set; }
        [Display(Name = "Created By")]
        public int create_user { get; set; }
        [Display(Name = "Created Date")]
        public DateTime create_ts { get; set; }
        [Display(Name = "Last Modified By")]
        public int modify_user { get; set; }
        [Display(Name = "Last Modified Date")]
        public DateTime modify_ts { get; set; }
        public bool is_active { get; set; }
        [Display(Name = "Ref1")]
        public string ref1 { get; set; }
        [Display(Name = "Ref2")]
        public string ref2 { get; set; }
        [Display(Name = "Ref3")]
        public string ref3 { get; set; }
        [Display(Name = "Due Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? due_date { get; set; }
        public virtual List<fin_ledger_detail> fin_ledger_detail { get; set; }
        public virtual List<GetFinLedgerDetailById> GetFinLedgerDetailById { get; set; }
        public virtual List<GetDocDetail> GetDocDetail { get; set; }
        public string entry_detail { get; set; }
        public string category_name { get; set; }
        public string currency_name { get; set; }
        public List<string> fin_ledger_detail_id { get; set; }
        public List<string> entity_type_id { get; set; }
        public List<string> gl_ledger_id { get; set; }
        public List<string> dr_amount { get; set; }
        public List<string> cr_amount { get; set; }
        public List<string> cost_center_id { get; set; }
        public List<string> line_remarks { get; set; }
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
    }
    public class GetFinLedgerDetailById
    {
        public int fin_ledger_detail_id { get; set; }
        public int entity_type_id { get; set; }
        public string entity_type_name { get; set; }
        public int entity_id { get; set; }
        public string entity_name { get; set; }
        public double debit { get; set; }
        public double credit { get; set; }
        public string currency_name { get; set; }
        public double fc_debit { get; set; }
        public double fc_credit { get; set; }
        public string cost_center_description { get; set; }
        public int cost_center_id { get; set; }
        public string line_remarks { get; set; }
    }
    public class EntityType
    {
        public int id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
    }
    public class GetDocDetail
    {
        public string document_type_name { get; set; }
        public string doc_number { get; set; }
        public double amount { get; set; }
        public int document_id { get; set; }
        public string ref_bill_no { get; set; }
    }
}
