using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
   public class REF_CATEGORY
    {
        [Key]
        public int CATEGORY_ID { get; set; }
        [Display(Name ="Category")]
        [Required]
        public string CATEGORY_NAME { get; set; }
    }
}
