using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_fin_template_detail
    {
        [Key]
        public int template_detail_id { get; set; }
        
        public int template_id { get; set; }
        [ForeignKey("template_id")]
        public virtual ref_fin_template ref_fin_template { get; set; }
        public int bs_pl { get; set; }
        public int bs_pl_side { get; set; }
        public string group_id { get; set; }
        public int? group_no { get; set; }
        public string group_name { get; set; }
        public string parent_id { get; set; }
        public int? group_level { get; set; }
        public bool? is_active { get; set; }
        public bool? main_heading { get; set; }

    }
}
