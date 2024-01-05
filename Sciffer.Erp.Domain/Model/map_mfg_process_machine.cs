using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class map_mfg_process_machine
    {
        [Key]
        [Column(Order = 0)]
        public int process_id { get; set; }
        [ForeignKey("process_id")]
        public virtual ref_mfg_process ref_mfg_process { get; set; }
        [Key]
        [Column(Order = 1)]
        public int machine_id { get; set; }
        [ForeignKey("machine_id")]
        public virtual ref_machine ref_machine { get; set; }
    }
}
