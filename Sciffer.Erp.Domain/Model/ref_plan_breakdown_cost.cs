using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_plan_breakdown_cost
    {
        [Key]
        public int plan_breakdown_cost_id { get; set; }
        public int item_id { get; set; }
        [ForeignKey("item_id")]
        public virtual REF_ITEM REF_ITEM { get; set; }
        public int parameter_cost_id { get; set; }
        [ForeignKey("parameter_cost_id")]
        public virtual ref_parameter_list ref_parameter_list { get; set; }
        public double required_qty { get; set; }
        public double actual_qty { get; set; }
        public int? sloc_id { get; set; }
        [ForeignKey("sloc_id")]
        public virtual REF_STORAGE_LOCATION REF_STORAGE_LOCATION { get; set; }
        public int bucket_id { get; set; }
        [ForeignKey("bucket_id")]
        public virtual ref_bucket ref_bucket { get; set; }
        public double? value { get; set; }
        public int goods_issue_id { get; set; }
        [ForeignKey("goods_issue_id")]
        public virtual goods_issue goods_issue { get; set; }
        public string issue_doc_number { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? issue_posting_date { get; set; }
        public int plan_breakdown_order_id { get; set; }
        [ForeignKey("plan_breakdown_order_id")]
        public virtual ref_plan_breakdown_order ref_plan_breakdown_order { get; set; }
        public bool is_active { get; set; }
        public double? balance_qty { get; set; }
        public int uom_id { get; set; }
        [ForeignKey("uom_id")]
        public virtual REF_UOM REF_UOM { get; set; }
    }
}
