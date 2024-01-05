using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
  public class REF_USER
    {
        [Key]
        public int USER_ID { get; set; }
        [Display(Name = "User Name")]
        public String USER_NAME { get; set; }
    }
}
