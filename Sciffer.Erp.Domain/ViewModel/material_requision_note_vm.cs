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
    public class material_requision_note_vm
    {
        public int material_requision_note_id { get; set; }
        public string category_name { get; set; }
        [Display(Name ="Category *")]
        public int category_id { get; set; }
        //[ForeignKey("category_id")]
        //public virtual ref_document_numbring ref_document_numbring { get; set; } 
        [Display(Name = "Number")]
        public string number { get; set; }
        [Display(Name = "Document Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? document_date { get; set; }
        public string requirement_by_name { get; set; }
        [Display(Name = "Requirement by")]
        public int? requirement_by { get; set; }
        public string plant_name { get; set; }
        [Display(Name = "Plant *")]
        public int plant_id { get; set; }
        //[ForeignKey("plant_id")]
        //public virtual REF_PLANT REF_PLANT { get; set; } 
        [Display(Name = "Remarks")]
        public string remarks { get; set; }
        public string receiving_sloc_name { get; set; }
        [Display(Name = "Receiving Sloc *")]
        public int receiving_sloc { get; set; }
        //[ForeignKey("receiving_sloc")]
        //public virtual REF_STORAGE_LOCATION REF_STORAGE_LOCATION_receiving { get; set; }
        public string sending_sloc_name { get; set; }
        [Display(Name = "Sending Sloc *")]
        public int sending_sloc { get; set; }
        //[ForeignKey("sending_sloc")]
        //public virtual REF_STORAGE_LOCATION REF_STORAGE_LOCATION_sending { get; set; }
        [Display(Name = "Type")]
        public int? type { get; set; }
        public string type_name { get; set; }
        public string status_name { get; set; }
        [Display(Name = "Status")]
        public int status_id { get; set; }
        //[ForeignKey("status_id")]
        //public virtual ref_status ref_status { get; set; }
        public bool is_active { get; set; }
        public string details { get; set; }
        [Display(Name ="Posting Date *")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime posting_date { get; set; }
        public string mrn_date_number { get; set; }
        public virtual List<material_requision_note_detail> material_requision_note_detail { get; set; }
        public List<string> material_requision_note_detail_id1 { get; set; }
        public List<string> material_requision_note_id1 { get; set; }
        public List<string> item_id1 { get; set; }
        public List<string> uom_id1 { get; set; }
        public List<string> required_qty1 { get; set; }
        public List<double> rate { get; set; }
        public List<string> cost_center_id1 { get; set; }
        public List<string> machine1 { get; set; }
        public List<string> reason1 { get; set; }
        public List<string> order_type1 { get; set; }
        public List<string> order_number1 { get; set; }
        public List<string> line_remarks1 { get; set; }
        public List<string> is_active1 { get; set; }
        public List<string> crankshaft_id1 { get; set; }
        public List<string> tool_category_id1 { get; set; }
        public List<string> tool_usage_type_id1 { get; set; }
        public List<string> process_id1 { get; set; }
        public string deleteids { get; set; }
        [Display(Name ="Approval Status")]
        public int? approval_status { get; set; }
        public string approval_status_name { get; set; }
        public string approval_comments { get; set; }
        public int? approved_by { get; set; }
        public string approval_user { get; set; }
    }
}
