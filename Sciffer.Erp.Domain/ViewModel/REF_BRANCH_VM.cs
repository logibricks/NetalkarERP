using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
   public class REF_BRANCH_VM
    {
        [Key]
        public int BRANCH_ID { get; set; }


        [Display(Name = "Branch")]
        [Required]
        public string BRANCH_NAME { get; set; }


        [Display(Name = "Branch Details")]
        [Required]
        public string BRANCH_DESCRIPTION { get; set; }

        [Display(Name = "Blocked")]
        public bool is_active { get; set; }

    }

    public class REF_BRANCHVM
    {
        public int BRANCH_ID { get; set; }
        public string BRANCH_NAME { get; set; }
        public string BRANCH_DESCRIPTION { get; set; }
        public bool is_active { get; set; }

    }

}
