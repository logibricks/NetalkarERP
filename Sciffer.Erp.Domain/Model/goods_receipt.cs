using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class goods_receipt
    {
        [Key]
        public int? goods_receipt_id { get; set; }
        [Display(Name = "Goods Receipt Number")]
        public string goods_receipt_number { get; set; }        

        public int category_id { get; set; }
        [ForeignKey("category_id")]    
        public virtual ref_document_numbring ref_document_numbring { get; set; }
        public int? status_id { get; set; }


        [Display(Name = "Posting Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime posting_date { get; set; }

        public string ref1 { get; set; }

        [Display(Name = "Document Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime document_date { get; set; }

        public int plant_id { get; set; }
        [ForeignKey("plant_id")]
        public virtual REF_PLANT REF_PLANT { get; set; }    
        [Display(Name = "Remarks")]
        public string remarks { get; set; }

        [Display(Name = "Attachment")]
        public string attachment { get; set; }

        public bool is_active { get; set; }

        public virtual ICollection<goods_receipt_detail> goods_receipt_detail { get; set; }

        
        public int? cancellation_reason_id { get; set; }
        public string cancellation_remarks { get; set; }
        public DateTime? cancelled_date { get; set; }
        public int? cancelled_by { get; set; }

    }
}
