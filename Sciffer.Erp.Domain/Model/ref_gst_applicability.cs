using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
    public  class ref_gst_applicability
    {
        [Key]
        public int gst_applicability_id { get; set; }
        public string gst_applicability_name { get; set; }
        public bool is_active { get; set; }
    }
}
