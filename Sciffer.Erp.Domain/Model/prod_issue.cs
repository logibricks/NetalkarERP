using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class prod_issue
    {
        [Key]
        public int prod_issue_id { get; set; }
        public string prod_issue_number { get; set; }
        public int category_id { get; set; }
        [ForeignKey("category_id")]
        public virtual ref_document_numbring ref_document_numbring { get; set; }
        public DateTime posting_date { get; set; }
        public DateTime document_date { get; set; }
        public int plant_id { get; set; }
        [ForeignKey("plant_id")]
        public virtual REF_PLANT REF_PLANT { get; set; }
        public string remarks { get; set; }
        public bool is_active { get; set; }
        public int prod_order_id { get; set; }
        [ForeignKey("prod_order_id")]
        public virtual mfg_prod_order mfg_prod_order { get; set; }
        public virtual ICollection<prod_issue_detail> prod_issue_detail { get; set; }
        public virtual ICollection<prod_issue_detail_tag> prod_issue_detail_tag { get; set; } 
    }
}
