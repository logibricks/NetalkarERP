using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class goods_issue
    {
        [Key]
        public int? goods_issue_id { get; set; }
        [Display(Name ="Goods Issue Number")]
        public string goods_issue_number { get; set; }
        public int document_numbring_id { get; set; }
        [ForeignKey("document_numbring_id")]
        public virtual ref_document_numbring ref_document_numbring { get; set; }
        [Display(Name = "Posting Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime posting_date { get; set; }        
        [Display(Name = "Document Date")]
        [Required(ErrorMessage = "document date is required")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime document_date { get; set; }
        public int plant_id { get; set; }
        [ForeignKey("plant_id")]
        public virtual REF_PLANT REF_PLANT { get; set; }
        public string ref1 { get; set; }  
        [Display(Name = "Remarks")]
        public string remarks { get; set; }
        [Display(Name ="Attachment")]
        public string attachment { get; set; }
        public bool is_active { get; set; }
        public int? document_id { get; set; }
        public string document_code { get; set; }
        public virtual ICollection<goods_issue_detail> goods_issue_detail { get; set; }        
    }
}
