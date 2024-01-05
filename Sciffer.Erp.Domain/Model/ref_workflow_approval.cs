using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_workflow_approval
    {
        [Key]
        public int workflow_approval_id { get; set; }
        public int user_id { get; set; }
        [ForeignKey("user_id")]
        public virtual REF_USER REF_USER { get; set; }
        public int workflow_detail_id { get; set; }
        [ForeignKey("workflow_detail_id")]
        public virtual ref_workflow_detail ref_workflow_detail { get; set; }
    }
}
