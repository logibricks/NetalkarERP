using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_user_role_rights
    {
        [Key]
        public int role_right_id { get; set; }

        public int role_id { get; set; }
        [ForeignKey("role_id")]
        public virtual ref_user_management_role ref_user_management_role { get; set; }

        public int module_form_id { get; set; }
        [ForeignKey("module_form_id")]
        public virtual ref_module_form ref_module_form { get; set; }

        public bool view_rights { get; set; }
        public bool create_rights { get; set; }
        public bool edit_rights { get; set; }
    }
}
