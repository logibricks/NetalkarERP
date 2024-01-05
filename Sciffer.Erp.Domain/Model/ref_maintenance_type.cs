using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_maintenance_type
    {
        [Key]
        public int maintenance_type_id { get; set; }
        public string maintenance_name { get; set; }
    }
}
