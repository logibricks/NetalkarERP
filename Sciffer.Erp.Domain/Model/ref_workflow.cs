using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_workflow
    {
        [Key]
        public int workflow_id { get; set; }
        public int document_type_id { get; set; }
        [ForeignKey("document_type_id")]
        public virtual ref_document_type ref_document_type { get; set; }
        public int category_id { get; set; }
        [ForeignKey("category_id")]
        public virtual REF_CATEGORY REF_CATEGORY { get; set; }
        public bool has_value { get; set; }
        public double value_from { get; set; }
        public double value_to { get; set; }
        public int no_of_approval { get; set; }
        public bool is_active { get; set; }
        public virtual ICollection<ref_workflow_detail> ref_workflow_detail { get; set; }
        public virtual ICollection<ref_workflow_approval> ref_workflow_approval { get; set; }
    }
}
