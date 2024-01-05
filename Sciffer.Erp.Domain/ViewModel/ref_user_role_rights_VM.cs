using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sciffer.Erp.Domain.ViewModel
{
    public class ref_user_role_rights_VM
    {
        public int role_right_id { get; set; }

        public int role_id { get; set; }
        public string role_name { get; set; }
        public int module_form_id { get; set; }

        public string module_form_name { get; set; }

        public List<string> form_id_name { get; set; }
        public List<string> create_rights_name { get; set; }
        public List<string> view_rights_name { get; set; }
        public List<string> edit_rights_name { get; set; }

        public bool view_rights { get; set; }
        public bool create_rights { get; set; }
        public bool edit_rights { get; set; }


    }
}
