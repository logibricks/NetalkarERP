using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_tds_code_detail
    {
        [Key]
        public int tds_code_detail_id { get; set; }
        public DateTime effective_from { get; set; }      
        public int rate { get; set; }
        public bool is_active { get; set; }
        public int tds_code_id { get; set; }
        [ForeignKey("tds_code_id")]
        public virtual ref_tds_code ref_tds_code { get; set; }
    }
}
