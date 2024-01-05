using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class temp
    {
        [Key]
        public int temp_id { get; set; }
        public DateTime date { get; set; }
        public string pp { get; set; }
    }
}
