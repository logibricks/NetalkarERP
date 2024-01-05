using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
   public class REF_MARITAL_STATUS
    {
        [Key]
        public int MARITAL_STATUS_ID { get; set; }
        [Display(Name ="Marital Status")]
        [Required]
        public string MARITAL_STATUS_NAME { get; set; }
    }
}
