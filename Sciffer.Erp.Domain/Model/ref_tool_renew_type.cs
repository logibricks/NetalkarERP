using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_tool_renew_type
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int tool_renew_type_id { get; set; }
        public string tool_renew_type_name { get; set; }
        public bool is_blocked { get; set; }
        public bool is_active { get; set; }
        public int created_by { get; set; }
        public DateTime created_on { get; set; }
    }
}
