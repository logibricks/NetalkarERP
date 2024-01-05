using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
    public  class ref_cost_center
    {
        [Key]
        public int cost_center_id { get; set; }
        [Display(Name ="Cost Center Code")]
        public string cost_center_code { get; set; }
        [Display(Name ="Cost Center Name")]
        public string cost_center_description { get; set; }
        public int cost_center_level { get; set; }
        //public int? parent_id { get; set; }
        public bool? is_active { get; set; }
        [Display(Name ="Blocked")]
        public bool is_blocked { get; set; }
        [Display(Name ="Parent Code")]
        public string parent_code { get; set; }
        public int head_parent { get; set; }
    }
    public class ref_cost_center_vm
    {
        public int cost_center_id { get; set; }
        public string cost_center_code { get; set; }
        public string cost_center_description { get; set; }
        public int cost_center_level { get; set; }
        public string parent_code { get; set; }
        public bool is_blocked { get; set; }
        public int head_parent { get; set; }
    }
    public class exportdataCostCenter
    {
        public int SrNo { get; set; }
        public string CostCenterCode { get; set; }
        public string CostCenterName { get; set; }
        public string HeadAccount { get; set; }
        public string ParentCode { get; set; }
    }
    public class TreeViewNodeCostCenter
    {
        public TreeViewNodeCostCenter()
        {
            ChildNode = new List<TreeViewNodeCostCenter>();
        }
        public int cost_center_id { get; set; }
        public string cost_center_code { get; set; }
        public string cost_center_description { get; set; }
        public int cost_center_level { get; set; }
        public string parent_code { get; set; }
        public int head_parent { get; set; }
        public string NodeName
        {
            get { return GetNodeName(); }
        }
        public IList<TreeViewNodeCostCenter> ChildNode { get; set; }

        public string GetNodeName()
        {
            return this.cost_center_code + " - " + this.cost_center_description;
        }
    }
}
