using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class inventory_revaluation
    {
        [Key]
        public int inventory_revaluation_id { get; set; }
        [Display(Name ="Posting Date")]
        [Required(ErrorMessage ="posting date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime inventory_revaluation_date { get; set; }
        [Display(Name = "Document Date")]        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? inventory_revaluation_document_date { get; set; }
       
        [Display(Name ="Number")]
        public string inventory_revaluation_number { get; set; }
       
        [Display(Name ="Remark")]
        [DataType(DataType.MultilineText)]
        public string inventory_revaluation_remark { get; set; }
        [Display(Name = "Attachment")]
        public string attachement { get; set; }
        [Required(ErrorMessage = "category is required")]
        public int category_id { get; set; }
        public int plant_id { get; set; }
        public virtual ICollection<inventory_revaluation_detail> inventory_revaluation_detail { get; set; }
        public bool? is_active { get; set; }
    }
}
