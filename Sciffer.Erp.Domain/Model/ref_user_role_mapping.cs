using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_user_role_mapping
    {
        [Key]
        public int role_mapping_id { get; set; }

        public int role_id { get; set; }
        [ForeignKey("role_id")]
        public virtual ref_user_management_role ref_user_management_role { get; set; }

        public int user_id { get; set; }
        [ForeignKey("user_id")]
        public virtual ref_user_management ref_user_management { get; set; }

        public bool is_block { get; set; }
        public bool is_active { get; set; }
    }
}
