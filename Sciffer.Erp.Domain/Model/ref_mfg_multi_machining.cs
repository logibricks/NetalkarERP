using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
   public class ref_mfg_multi_machining
    {
        [Key]
        public int multi_machining_id { get; set; }
        public int machine_group_id { get; set; }
        public string machine_id { get; set; }


    }
}
