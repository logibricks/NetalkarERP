using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class rejection_receipt
    {
        [Key]
        public int reject_receipt_id { get; set; }
        public string reject_doc_number { get; set; }
        public int category_id { get; set; }
        [ForeignKey("category_id")]
        public virtual ref_document_numbring ref_document_numbring { get; set; }
        //public DateTime posting_date { get; set; }
        //public DateTime document_date { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime posting_date { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime document_date { get; set; }
        public int plant_id { get; set; }
        [ForeignKey("plant_id")]
        public virtual REF_PLANT REF_PLANT { get; set; }
        public string header_remarks { get; set; }
        public bool is_active { get; set; }
        public string prod_order_id { get; set; }
        //[ForeignKey("prod_order_id")]
        //public virtual mfg_prod_order mfg_prod_order { get; set; }

        public int storage_location_id { get; set; }
        [ForeignKey("storage_location_id")]
        public virtual REF_STORAGE_LOCATION REF_STORAGE_LOCATION { get; set; }

        public virtual ICollection<rejection_receipt_detail> rejection_receipt_detail { get; set; }
    }
}
