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
    public class ref_user_management_role_VM
    {

        [Key]
        public int role_id { get; set; }
        public string role_name { get; set; }
        public string role_code { get; set; }
        public bool is_block { get; set; }
    }
}
