using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
   public class operator_change_request
    {
        [Key]
        public int operator_change_request_id { get; set; }
        public int category_id { get; set; }
        public string document_no { get; set; }
        public DateTime posting_date { get; set; }
        public int operator_id { get; set; }
        public int machine_id { get; set; }
        public int current_level_id { get; set; }
        public int new_level_id { get; set; }
        public bool is_active { get; set; }
        public int created_by { get; set; }
        public DateTime created_ts { get; set; }
        public int? modified_by { get; set; }
        public DateTime? modified_ts { get; set; }
        

    }
}
