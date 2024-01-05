using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sciffer.Erp.Domain.Model
{
    public class pur_qa
    {
        [Key]
        public int qa_id { get; set; }
        public int? category_id { get; set; }
        public string document_no{get; set;}
        public DateTime posting_date { get; set; }
        public int item_id { get; set; }
        [ForeignKey("item_id")]
        public virtual REF_ITEM REF_ITEM { get; set; }
        public int batch_id { get; set; }
        public string batch_no { get; set; }
        public int status_id { get; set; }
        [ForeignKey("status_id")]
        public virtual ref_status ref_status { get; set; }
        public int document_id { get; set; }
        public DateTime document_date { get; set; }
        public int item_number { get; set; }
        public int document_qty { get; set; }
        public int inspection_lot_qty { get; set; }
        public int sample_size_checked { get; set; }
        public int sample_size_accepted { get; set; }
        public int sample_size_rejected { get; set; }
        public int total_accepted_qty { get; set; }
        public int total_rejected_qty { get; set; }
        public int total_wip_qty { get; set; }
        public int shelf_life { get; set; }
        public int shelf_life_uom_id { get; set; }
        public DateTime grn_date { get; set; }
        public int date_based_on { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public string internal_remarks{get; set;}
        public string remrarks_on_doc{get; set;}
        public string attachment{get; set;}
        public string document_type_code { get; set; }
        public bool is_active { get; set; }      
        public int? plant_id { get; set; }
        public int? sloc_id { get; set; }
        public string source_document_no { get; set; }
        public virtual ICollection<pur_qa_detail> pur_qa_detail { get; set; }
    }
    public class pur_qa_VM
    {
        public int qa_id { get; set; }
        [Display(Name = "Category *")]
        public int? category_id { get; set; }
        public string category_name { get; set; }
        [Display(Name = "Document Number *")]
        public string document_no { get; set; }
        [Display(Name = "Posting Date *")]
        public DateTime posting_date { get; set; }
        [Display(Name = "Item *")]
        public int item_id { get; set; }
        public string item_name { get; set; }
        [Display(Name = "Batch *")]
        public int batch_id { get; set; }
        [Display(Name = "Batch No ")]
        public string batch_no { get; set; }
        [Display(Name = "Status *")]
        public int status_id { get; set; }
        [ForeignKey("status_id")]
        public virtual ref_status ref_status { get; set; }
        public string status_name { get; set; }
        [Display(Name = "Document *")]
        public int document_id { get; set; }
        public string document_number { get; set; }
        [Display(Name = "Document Date *")]
        public DateTime document_date { get; set; }
        [Display(Name = "Item Number *")]
        public int item_number { get; set; }
        [Display(Name = "Document Quantity *")]
        public int document_qty { get; set; }
        [Display(Name = "Inspection Lot Quantity *")]
        public int inspection_lot_qty { get; set; }
        [Display(Name = "Sample Size Checked *")]
        public int sample_size_checked { get; set; }
        [Display(Name = "Sample Size Accepted *")]
        public int sample_size_accepted { get; set; }
        [Display(Name = "Sample Size Rejected *")]
        public int sample_size_rejected { get; set; }
        [Display(Name = "Total Accepted Quantity *")]
        public int total_accepted_qty { get; set; }
        [Display(Name = "Total Rejected Quantity *")]
        public int total_rejected_qty { get; set; }
        [Display(Name = "Total WIP Quantity *")]
        public int total_wip_qty { get; set; }
        [Display(Name = "Shelf Life *")]
        public int shelf_life { get; set; }
        [Display(Name = "Shelf Life UoM *")]
        public int shelf_life_uom_id { get; set; }
        public string shelf_life_uom_name { get; set; }
        [Display(Name = "GRN Date *")]
        public DateTime grn_date { get; set; }
        [Display(Name = "Date Based On *")]
        public int date_based_on { get; set; }
        [Display(Name = "Start Date *")]
        public DateTime start_date { get; set; }
        [Display(Name = "SLED *")]
        public DateTime end_date { get; set; }
        [Display(Name = "Internal Remarks")]
        [DataType(DataType.MultilineText)]
        public string internal_remarks { get; set; }
        [Display(Name = "Remarks On Doc")]
        [DataType(DataType.MultilineText)]
        public string remrarks_on_doc { get; set; }
        [Display(Name = "Attachment")]
        public string attachment { get; set; }
        [Display(Name = "Document Type *")]
        public string document_type_code { get; set; }
        [Display(Name ="Plant *")]
        public int? plant_id { get; set; }
        [Display(Name = "SLoc *")]
        public int? sloc_id { get; set; }       
        public virtual IList<pur_qa_detail> pur_qa_detail { get; set; }
        public string deleteids { get; set; }
        [NotMapped]
        public HttpPostedFileBase FileUpload { get; set; }
        public List<string> qa_detail_id { get; set; }
        public List<string> parameter_id { get; set; }
        public List<string> parameter_range { get; set; }
        public List<string> actual_value { get; set; }
        public List<string> method_used { get; set; }
        public List<string> checked_by { get; set; }
        public List<string> document_reference { get; set; }
        public List<string> pass_fail { get; set; }
    }
}
