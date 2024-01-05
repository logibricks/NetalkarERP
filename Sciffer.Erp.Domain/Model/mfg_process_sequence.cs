using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class mfg_process_sequence
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int process_sequence_id { get; set; }
        public string process_sequence_name { get; set; }
        public int ITEM_ID { get; set; }
        [ForeignKey("ITEM_ID")]
        public virtual REF_ITEM REF_ITEM { get; set; }
        public bool is_blocked { get; set; }
    }
}
