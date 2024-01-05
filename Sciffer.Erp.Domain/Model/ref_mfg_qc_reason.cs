using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_mfg_qc_reason
    {
        [Key]
        public int mfg_qc_reason_id { get; set; }
        public string mfg_qc_reason_code { get; set; }
        public string mfg_qc_reason_desc { get; set; }

    }
}
