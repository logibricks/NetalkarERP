using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_level
    {
        [Key]
        public int level_id { get; set; }
        public string level_code { get; set; }
        public string level_desc { get; set; }
        public string color_code { get; set; }
        public decimal percentage { get; set; }
        public bool is_active { get; set; }
        public int created_by { get; set; }
        public DateTime created_ts { get; set; }
        public int? modified_by { get; set; }
        public DateTime? modified_ts { get; set; }

    }
}
