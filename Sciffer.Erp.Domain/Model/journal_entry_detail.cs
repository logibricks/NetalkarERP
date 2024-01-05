using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class journal_entry_detail
    {
        [Key]
        public int journal_entry_item_id { get; set; }
        public int journal_entry_item_description { get; set; }
        public double journal_entry_dr { get; set; }
        public double journal_entry_cr { get; set; }
        public string journal_entry_remarks { get; set; }
        public int journal_entry_id { get; set; }
        [ForeignKey("journal_entry_id")]
        public virtual journal_entry journal_entry { get; set; }
        public int party_type_id { get; set; }
        [ForeignKey("party_type_id")]
        public virtual ref_party_type ref_party_type { get; set; }
        public bool journal_entry_item_is_active { get; set; }
        public int journal_sr_no { get; set; }
    }
}
