using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class mfg_qc_rule
    {
        [Key]
        public int mfg_qc_rule_id { get;set;}

        public int mfg_qc_id { get; set; }
        [ForeignKey("mfg_qc_id")]
        public virtual mfg_qc mfg_qc { get; set; }
    }
}
