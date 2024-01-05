using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class inv_revaluation_vm
    {
        public int inventory_revaluation_id { get; set; }
        [Display(Name = "Posting Date *")]
        [Required(ErrorMessage = "posting date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime inventory_revaluation_date { get; set; }
        [Display(Name = "Document Date")]       
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? inventory_revaluation_document_date { get; set; }
       
        [Display(Name = "Inventory Number *")]
        public string inventory_revaluation_number { get; set; }
      
        [Display(Name = "Remark")]
        [DataType(DataType.MultilineText)]
        public string inventory_revaluation_remark { get; set; }
        [NotMapped]
        public HttpPostedFileBase FileUpload { get; set; }
        [Display(Name = "Attachment")]
        public string attachement { get; set; }
        [Required(ErrorMessage = "category is required")]
        [Display(Name = "Category *")]
        public int category_id { get; set; }
        public string category_name { get; set; }
        [Display(Name = "Plant *")]
        public int plant_id { get; set; }
        public string plant_name { get; set; }
        public virtual List<inventory_revaluation_detail> inventory_revaluation_detail { get; set; }
        public string deleteids { get; set; }
        public bool? is_active { get; set; }
        public int item_id { get; set; }
        public string item_code { get; set; }
        public List<string> inv_revaluation_detail_id { get; set; }
        public List<string> item_id1 { get; set; }
        public List<string> quantity { get; set; }
        public List<string> uom_id { get; set; }
        public List<string> new_rate { get; set; }
        public List<string> old_rate { get; set; }
        public List<string> differential_rate { get; set; }
        public List<string> differential_value { get; set; }
        public List<string> general_ledger_id { get; set; }

    }
}
