using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class pur_qa_detail
    {
        [Key]
        public int? qa_detail_id { get; set; }
        public int qa_id { get; set; }
        [ForeignKey("qa_id")]
        public virtual pur_po pur_po { get; set; }
        public int parameter_id { get; set; }
        [ForeignKey("parameter_id")]
        public virtual ref_item_parameter ref_item_parameter { get; set; }
        public string parameter_range { get; set; }
        public string actual_value { get; set; }
        public string method_used{get; set;}
        public string checked_by{get; set;}
        public string document_reference{get; set;}
        public int pass_fail { get; set; }
        public bool is_active { get; set; }
    }
}
