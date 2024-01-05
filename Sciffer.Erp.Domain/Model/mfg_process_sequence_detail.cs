using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class mfg_process_sequence_detail
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int map_process_sequence_id { get; set; }
        public int process_sequence_id { get; set; }
        [ForeignKey("process_sequence_id")]
        mfg_process_sequence mfg_process_squence { get; set; }

        public int machine_id { get; set; }
        [ForeignKey("process_sequence_id")]
        public virtual ref_machine ref_machine { get; set; }

        public int process_id { get; set; }
        [ForeignKey("process_id")]
        public virtual ref_mfg_process ref_mfg_process { get; set; }

        public Double item_cost { get; set; }
    }
}
