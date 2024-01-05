using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class mfg_qc_parameter
    {
        [Key]
        public int mfg_qc_parameter_id { get; set; }
        public int mfg_qc_id { get; set; }
        [ForeignKey("mfg_qc_id")]
        mfg_qc mfg_qc { get; set; }
        public string parameter_name { get; set; }
        public string parameter_uom{ get; set; }
        public string std_range_start{ get; set; }
        public string std_range_end{ get; set; }
        public bool is_numeric { get; set; }
    }
}
