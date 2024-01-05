using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class REF_DEPARTMENT
    {
        [Key]
        public int DEPARTMENT_ID { get; set; }
        [Display(Name="Department Name")]
        public string DEPARTMENT_NAME { get; set; }
        [Display(Name ="Department Description")]
        public string DEPARTMENT_DESCRIPTION { get; set; }
    }
}
