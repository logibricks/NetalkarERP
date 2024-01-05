using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class mfg_operator_shift
    {
        [Key]
        public int operator_shift_id { get; set; }

        public int operator_id { get; set; }
        [ForeignKey("operator_id")]
        public virtual ref_user_management ref_user_management { get; set; }

        public int shift_id { get; set; }
        [ForeignKey("shift_id")]
        public virtual ref_shifts ref_shifts { get; set; }

        public int machine_id { get; set; }
        [ForeignKey("machine_id")]
        public virtual ref_machine ref_machine { get; set; }

        public DateTime operator_shift_ts { get; set; }
        public bool is_logged_in { get; set; }
    }
}
