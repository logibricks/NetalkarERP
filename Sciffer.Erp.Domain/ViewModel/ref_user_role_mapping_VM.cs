using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class ref_user_role_mapping_VM
    {
        public int role_mapping_id { get; set; }
        public int user_id { get; set; }
        public string user_name { get; set; }
        public string role_id { get; set; }
        public string role_name { get; set; }
        public bool is_block { get; set; }
        public string deleteids { get; set; }
        public string role_ids { get; set; }
        //public string[] Role_IDs { get; set; }
        //public string[] Role_Names { get; set; }
    }
}
