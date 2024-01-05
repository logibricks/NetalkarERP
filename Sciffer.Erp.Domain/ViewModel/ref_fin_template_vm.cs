using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class ref_fin_template_vm
    {

        public int template_detail_id { get; set; }
        public int? bs_pl { get; set; }
        public int? bs_pl_side { get; set; }
        public int? group_no { get; set; }
        public string group_name { get; set; }
        public string parent_id { get; set; }
        public int? group_level { get; set; }
        public int? is_active { get; set; }
        public int template_id { get; set; }
        public string template_name { get; set; }
        public int created_by { get; set; }
        public DateTime? created_ts { get; set; }
        public bool is_blocked { get; set; }
        public bool t_format { get; set; }
        public string t_format2 { get; set; }
        public bool main_heading { get; set; }
        public string template_name1 { get; set; }
        public bool? t_format1 { get; set; }
        public List<int> template_detail_id1 { get; set; }
        public List<int> bs_pl1 { get; set; }
        public List<int> bs_pl_side1 { get; set; }
        public List<int> group_no1 { get; set; }
        public List<string> group_name1 { get; set; }
        public List<int> group_level1 { get; set; }
        public List<int> main_heading1 { get; set; }
        public List<string> parent_id1 { get; set; }
        public List<int> is_active1 { get; set; }
        public List<string> group_id1 { get; set; }
        public virtual IList<ref_fin_template_detail> ref_fin_template_detail { get; set; }
        public virtual IList<TreeView_Node_VM> TreeView_Node_VM { get; set; }
    }
    public class TreeView_Node_VM
    {
        public TreeView_Node_VM()
        {
            ChildNode = new List<TreeView_Node_VM>();
        }

        public int? template_detail_id;
        public string group_name;
        public string parent_id;
        public int? group_level;

        public string NodeName
        {
            get { return GetNodeName(); }
        }

        public IList<TreeView_Node_VM> ChildNode { get; set; }

        public string GetNodeName()
        {
            return this.group_name;
        }
        public bool? t_format { get; set; }
        public int? bs_pl_side { get; set; }
        public int? group_no { get; set; }
        public bool? is_active { get; set; }
        public bool main_heading { get; set; }
        public string template_name { get; set; }
        public string template_name1 { get; set; }
        public bool? t_format1 { get; set; }
        public int? bs_pl { get; set; }
        public List<int> bs_pl_side1 { get; set; }
        public List<string> group_name1 { get; set; }
        public List<bool> main_heading1 { get; set; }
        public List<int> parent_id1 { get; set; }
        public int template_id { get; set; }

        
    }
}
