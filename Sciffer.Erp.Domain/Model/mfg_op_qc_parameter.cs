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
    public class mfg_op_qc_parameter
    {
        [Key]
        public int mfg_op_qc_parameter_id { get; set; }

        public int machine_id { get; set; }
        [ForeignKey("machine_id")]
        public virtual ref_machine ref_machine { get; set; }

        public int item_id { get; set; }
        [ForeignKey("item_id")]
        public virtual REF_ITEM REF_ITEM { get; set; }

        public bool is_active { get; set; }
        public int created_by { get; set; }
        public DateTime create_ts { get; set; }
        public int modified_by { get; set; }
        public DateTime modify_ts { get; set; }
        //public string parameter_name { get; set; }
        //public string parameter_uom{ get; set; }
        //public string std_range_start{ get; set; }
        //public string std_range_end{ get; set; }
        //public bool is_numeric { get; set; }
        //public bool is_list { get; set; }

        public virtual ICollection<mfg_op_qc_parameter_list> mfg_op_qc_parameter_list { get; set; }

    }
}
