using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
    public class REF_BUSINESS_UNIT
    {
        [Key]
        public int BUSINESS_UNIT_ID { get; set; }
        [Display(Name ="Business Unit Name")]
        public string BUSINESS_UNIT_NAME { get; set; }
        [Display(Name = "Description")]
        public string BUSINESS_UNIT_DESCRIPTION { get; set; }
        public bool is_blocked { get; set; }
        public bool is_active { get; set; }
    }
    public class ref_business_unit_vm
    {
        public int business_unit_id { get; set; }
        public string business_unit_name { get; set; }
    }
}
