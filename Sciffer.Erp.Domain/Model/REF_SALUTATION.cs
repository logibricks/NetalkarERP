using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class REF_SALUTATION
    {
        [Key]
        public int salutation_id { get; set; }
        [Display(Name ="Salutation")]
        public string salutation_name { get; set; }

    }
}
