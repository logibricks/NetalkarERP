using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_tool
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int tool_id { get; set; }
        public string tool_name { get; set; }
        public int item_id { get; set; }
        [ForeignKey("item_id")]
        public virtual REF_ITEM REF_ITEM { get; set; }
        public bool is_blocked { get; set; }
        public bool is_active { get; set; }
        public int created_by { get; set; }
        public DateTime created_on { get; set; }
    }

    public class ref_tool_VM
    {
        public int tool_id { get; set; }
        public string tool_name { get; set; }
        public int item_id { get; set; }
        public string item_name { get; set; }
        public bool is_blocked { get; set; }
        public bool is_active { get; set; }
        public int created_by { get; set; }
        public DateTime created_on { get; set; }
    }
}
