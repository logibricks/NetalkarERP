using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class fin_internal_reconcile
    {
        [Key]
        public int internal_reconcile_id { get; set; }
        public int category_id { get; set; }
        [ForeignKey("category_id")]
        public virtual ref_document_numbring ref_document_numbring { get; set; }
        public string document_no { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MMMM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime posting_date { get; set; }
        public int entity_type_id { get; set; }
        [ForeignKey("entity_type_id")]
        public virtual REF_ENTITY_TYPE REF_ENTITY_TYPE { get; set; }
        public int entity_id { get; set; }
        public int created_by { get; set; }
        public DateTime created_ts { get; set; }
        public virtual ICollection<fin_internal_reconcile_detail> fin_internal_reconcile_detail { get; set; }
        public int? status_id { get; set; }
        public virtual ref_status ref_status { get; set; }
        public int? cancellation_reason_id { get; set; }
        public string cancellation_remarks { get; set; }
        public DateTime? cancelled_date { get; set; }
        public int? cancelled_by { get; set; }
    }
}
