using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_role_dashboard_mapping
    {
        [Key]
        public int role_dashboard_mapping_id { get; set; }
        public int? role_id { get; set; }
        public int dashboard_id { get; set; }
        public bool is_active { get; set; }
    }
    public class ref_role_dashboard_mapping_vm
    {
        public int dashboard_id { get; set; }
        public string dashboard_name { get; set; }
        public int module_id { get; set; }
        public string module_name { get; set; }
        public int? role_id { get; set; }
        public bool is_active { get; set; }
        public List<string> dashboard_id1 { get; set; }
        public List<string> is_active1 { get; set; }
    }
}
