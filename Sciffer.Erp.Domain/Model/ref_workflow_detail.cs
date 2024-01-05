using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Sciffer.Erp.Domain.Model
{
    public class ref_workflow_detail
    {
        [Key]
        public int workflow_detail_id { get; set; }
        public int approval_set_no { get; set; }
        public string approval_set_name { get; set; }
        public int workflow_id { get; set; }
        [ForeignKey("workflow_id")]
        public virtual ref_workflow ref_workflow { get; set; }
        public virtual ICollection<ref_workflow_approval> ref_workflow_approval { get; set; }
    }
}
