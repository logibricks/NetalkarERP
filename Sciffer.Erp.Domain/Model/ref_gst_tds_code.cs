using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_gst_tds_code
    {
        [Key]
        public int gst_tds_code_id { get; set; }
        public string gst_tds_code { get; set; }
        public string gst_tds_code_description { get; set; }
        public int creditor_gl { get; set; }
        public int debtor_gl { get; set; }
        public bool? is_blocked { get; set; }
        public bool? is_active { get; set; }
        public virtual ICollection<ref_gst_tds_code_detail> ref_gst_tds_code_detail { get; set; }
    }
}
