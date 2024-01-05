using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_machine_category
    {
        [Key]
        public int machine_category_id { get; set; }
        public string machine_category_code { get; set; }
        public string machine_category_description { get; set; }
        public bool is_blocked { get; set; }
        public bool is_active { get; set; }

    }
}
