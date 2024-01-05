using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sciffer.Erp.Domain.Model;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class fin_internal_reconcile_vm
    {
        [Key]
        public int? internal_reconcile_id { get; set; }
        [Display(Name = "Document Category *")]
        public int category_id { get; set; }
        [ForeignKey("category_id")]
        public virtual ref_document_numbring ref_document_numbring { get; set; }
        [Display(Name = "Document Number")]
        public string document_no { get; set; }
        [Display(Name = "Reconcial Date *")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MMMM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime posting_date { get; set; }
        [Display(Name = "Reconcial Date *")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MMMM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime reconcileDate { get; set; }
        [Display(Name = "Entity Type *")]
        public int entity_type_id { get; set; }
        [ForeignKey("entity_type_id")]
        public virtual REF_ENTITY_TYPE REF_ENTITY_TYPE { get; set; }
        [Display(Name = "Entity Name *")]
        public int entity_id { get; set; }
        public virtual List<fin_internal_reconcile_detail> fin_internal_reconcile_detail { get; set; }

        public List<string> internal_reconcile_detail_id1 { get; set; }
        public List<string> internal_reconcile_id1 { get; set; }
        public List<string> doc_type_id1 { get; set; }
        public List<string> doc_category_id1 { get; set; }
        public List<string> doc_no1 { get; set; }
        public List<string> doc_posting_date1 { get; set; }
        public List<string> amount1 { get; set; }
        public List<string> balance_amount1 { get; set; }
        public List<string> reconcile_amount1 { get; set; }
        public List<string> fin_ledger_detail_id { get; set; }
        public string category_name { get; set; }
        public string entity_type_name { get; set; }
        public string entity_name { get; set; }
        public string entity_code { get; set; }

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
        [Display(Name = "Created By")]
        public int? created_by { get; set; }
        [Display(Name = "Created Date")]
        public DateTime? created_ts { get; set; }
    }
    public class fin_internal_reconcile_detail_vm
    {
        public int fin_ledger_detail_id { get; set; }
        public int document_type_id { get; set; }
        public string document_type_name { get; set; }
        public string source_document_no { get; set; }
        public string ledger_date { get; set; }
        public double amount_local { get; set; }
        public double balance_local { get; set; }
        public long rowIndex { get; set; }
        public string emptyString { get; set; }
        public long rowIndex1 { get; set; }
        public string emptyString1 { get; set; }
        public string ref1 { get; set; }
        public string ref2 { get; set; }
        public string ref3 { get; set; }
    }
}
