using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
     public class fin_internal_reconcile_detail
    {
        [Key]
        public int internal_reconcile_detail_id { get; set; }
        public int internal_reconcile_id { get; set; }
        [ForeignKey("internal_reconcile_id")]
        public virtual fin_internal_reconcile fin_internal_reconcile { get; set; }
        public int doc_type_id { get; set; }
        [ForeignKey("doc_type_id")]
        public virtual ref_document_type ref_document_type { get; set; }
        //public string doc_type_code { get; set; }
        public int doc_category_id { get; set; }
        public string doc_no { get; set; }
        public string doc_posting_date { get; set; }
        public double amount { get; set; }
        public double balance_amount { get; set; }
        public double reconcile_amount { get; set; }
    }
}
