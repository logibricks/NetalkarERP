using System;
using System.ComponentModel.DataAnnotations;

namespace Sciffer.Erp.Domain.Model
{
     public class ref_gst_tds_code_detail
    {
        [Key]
        public int gst_tds_code_detail_id { get; set; }
        public int gst_tds_code_id { get; set; }
        public DateTime effective_from { get; set; }
        public int rate { get; set; }
        public bool is_active { get; set; }

    }
}
