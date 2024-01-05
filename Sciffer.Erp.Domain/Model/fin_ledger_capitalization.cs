using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
   public class fin_ledger_capitalization
    {
        [Key]
        public int fin_ledger_capitalization_id { get; set; }
        public int document_numbering_id { get; set; }
        public string document_no { get; set; }
        public DateTime posting_date { get; set; }
        public int asset_code_id { get; set; }
        public DateTime capitalization_date { get; set; }
        public int gl_ledger_id { get; set; }
        public int created_by { get; set; }
        public DateTime created_ts { get; set; }
        public bool is_active { get; set; }
        public virtual ICollection<fin_ledger_capitalization_detail> fin_ledger_capitalization_detail { get; set; }
    }
}
