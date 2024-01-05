using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_operator_level_mapping
    {
        [Key]
        public int operator_level_mapping_id { get; set; }
        public int operator_id { get; set; }
        public int level_id { get; set; }
        public int machine_id { get; set; }
        public DateTime effective_from { get; set; }
        public DateTime effective_to { get; set; }
        public bool is_active { get; set; }
        public int created_by { get; set; }
        public DateTime created_ts { get; set; }
        public int? modified_by { get; set; }
        public DateTime? modified_ts { get; set; }
        public string machine_id_list { get; set; }
        public int mapping_id { get; set; }
        public bool is_block { get; set; }


    }
}
