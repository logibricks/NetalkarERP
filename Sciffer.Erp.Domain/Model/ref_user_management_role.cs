using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_user_management_role
    {
        [Key]
        public int role_id { get; set; }
        public string role_name { get; set; }
        public string role_code { get; set; }
        public bool is_block { get; set; }

    }
}
