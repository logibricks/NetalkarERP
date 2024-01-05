using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sciffer.Erp.Domain.ViewModel
{
   public class ref_user_management_VM
    {
        [Key]
        public int user_id { get; set; }
        public string user_name { get; set; }
        public string user_code { get; set; }

        public int? employee_id { get; set; }
        [ForeignKey("employee_id")]
        public virtual REF_EMPLOYEE REF_EMPLOYEE { get; set; }

        public string email { get; set; }
        public string mobile_no { get; set; }

        public int? branch_id { get; set; }
        [ForeignKey("branch_id")]
        public virtual REF_BRANCH REF_BRANCH { get; set; }

        public int? department_id { get; set; }
        [ForeignKey("department_id")]
        public virtual REF_DEPARTMENT REF_DEPARTMENT { get; set; }

        public string password { get; set; }

        public string employee_name { get; set; }
        public string branch_name { get; set; }
        public string department_name { get; set; }
        public string role_name { get; set; }

        public bool is_block { get; set; }
        public bool is_authentication { get; set; }
        public string notes { get; set; }

        public string employeephoto { get; set; }
    }
}
