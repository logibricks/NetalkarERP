using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class mfg_tag_numbering
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int tag_numbering_id { get; set; }
        public string from_number { get; set; }
        public string to_number { get; set; }
        public int? current_number { get; set; }
        public int year { get; set; }
        public int month { get; set; }
        public string prefix { get; set; }
        public bool is_active { get; set; }
        public bool is_blocked { get; set; }
        public int machine_id { get; set; }
        [ForeignKey("machine_id")]
        public virtual ref_machine ref_machine { get; set; }
        public int item_id { get; set; }
        [ForeignKey("item_id")]
        public virtual REF_ITEM REF_ITEM { get; set; }

    }   
}
