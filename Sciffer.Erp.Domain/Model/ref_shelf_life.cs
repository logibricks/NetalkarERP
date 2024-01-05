using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_shelf_life
    {
        [Key]
        public int shelf_life_id { get; set; }
        public string shelf_life_name { get; set; }
    }
}
