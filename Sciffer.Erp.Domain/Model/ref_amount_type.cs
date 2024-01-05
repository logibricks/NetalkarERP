using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_amount_type
    {
        [Key]
        public int amount_type_id { get; set; }
        public string amount_type { get; set; }
    }
}
