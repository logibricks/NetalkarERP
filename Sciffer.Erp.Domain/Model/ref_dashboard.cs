using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_dashboard
    {
        [Key]
        public int dashboard_id { get; set; }
        public string dashboard_name { get; set; }
        public string dashboard_code { get; set; }
        public int module_id { get; set; }
    }
    public class ref_dashboard_vm
    {
        public string dashboard_code { get; set; }
    }
}
