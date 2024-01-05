using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class REF_BRANCH
    {
        [Key]
        public int BRANCH_ID { get; set; }


        [Display(Name ="Branch")]
        [Required]
        public string BRANCH_NAME { get; set; }


        [Display(Name ="Branch Details")]
        [Required]
        public string BRANCH_DESCRIPTION { get; set; }

        [Display(Name = "Blocked")]
        public bool is_active { get; set; }

    }
}
