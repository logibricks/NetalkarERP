using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class mfg_qc_qc_parameter_list
    {
        [Key]
        public int mfg_qc_qc_parameter_list_id { get; set; }
        public int mfg_qc_qc_parameter_id { get; set; }
        [ForeignKey("mfg_qc_qc_parameter_id")]
        public virtual mfg_qc_qc_parameter mfg_qc_qc_parameter { get; set; }
        public string parameter_name { get; set; }
        public string parameter_uom { get; set; }
        public string std_range_start { get; set; }
        public string std_range_end { get; set; }
        public bool is_numeric { get; set; }
        public bool? is_active { get; set; }

    }
}
