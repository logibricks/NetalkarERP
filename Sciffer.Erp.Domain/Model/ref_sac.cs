using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_sac
    {
        [Key]
        public int sac_id { get; set; }
        public string sac_code { get; set; }
        public string sac_description { get; set; }
    }
    public class ref_sac_vm
    {
        public int sac_id { get; set; }
        public string sac_code { get; set; }
    }
}
