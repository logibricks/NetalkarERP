using Syncfusion.XPS;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.UI.WebControls;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_general_ledger
    {
        [Key]
        public int gl_ledger_id { get; set; }
        [Display(Name = "GL Account Type")]
        public int gl_account_type { get; set; }
        [ForeignKey("gl_account_type")]
        public virtual ref_gl_acount_type ref_gl_acount_type { get; set; }
        [Display(Name = "GL Ledger Code")]
        public string gl_ledger_code { get; set; }
        [Required]
        [Display(Name = "General Ledger Name")]
        public string gl_ledger_name { get; set; }
        [Display(Name = "GL Head/Account")]
        public int gl_head_account { get; set; }
        [Display(Name = "Blocked")]
        public bool is_blocked { get; set; }
        [Display(Name = "Active")]
        public bool? is_active { get; set; }
        [Display(Name = "GL Parent Ledger Code")]
        public string gl_parent_ledger_code { get; set; }
        public int? gl_level { get; set; }

    }

    public class Node
    {
        public int gl_account_type { get; set; }
        public string gl_ledger_code { get; set; }
        public int gl_ledger_id { get; set; }
    }
    public class TreeViewNodeVM
    {
        public TreeViewNodeVM()
        {
            ChildNode = new List<TreeViewNodeVM>();
        }

        public int gl_ledger_id { get; set; }
        public string gl_ledger_name { get; set; }
        public string gl_ledger_code { get; set; }
        public int gl_head_account { get; set; }
        public string gl_parent_code { get; set; }
        public int? gl_level { get; set; }
        public string NodeName
        {
            get { return GetNodeName(); }
        }
        public IList<TreeViewNodeVM> ChildNode { get; set; }

        public string GetNodeName()
        {
            return this.gl_ledger_code + " - " + this.gl_ledger_name;
        }



    }
    public class CommomView
    {
        public ref_general_ledger ref_general_ledger { get; set; }
        public TreeViewNodeVM TreeViewNodeVM { get; set; }
    }


    public class ref_ledger_vm
    {
        public int gl_ledger_id { get; set; }
        [Display(Name = "GL Account Type")]
        public int gl_account_type { get; set; }
        public string gl_account_type_name { get; set; }
        [Display(Name = "GL Ledger Code")]
        public string gl_ledger_code { get; set; }
        [Display(Name = "General Ledger Name")]
        public string gl_ledger_name { get; set; }
        [Display(Name = "GL Head/Account")]
        public int gl_head_account { get; set; }
        public string gl_head_account_name { get; set; }
        [Display(Name = "GL Parent Ledger Code")]
        public string gl_parent_ledger_code { get; set; }
        public string gl_parent_ledger_name { get; set; }
        public int? gl_level { get; set; }
        [Display(Name = "Blocked")]
        public bool is_blocked { get; set; }
        public string gl_ledger_name_drop { get; set; }
    }

    public class exportdata
    {
        public int SrNo { get; set; }
        public string GeneralLedgerType { get; set; }
        public string GeneralLedgerCode { get; set; }
        public string GeneralLedgerName { get; set; }
        public string HeadAccount { get; set; }
        public string ParentLedgerCode { get; set; }
        public int? Level { get; set; }
    }
}
