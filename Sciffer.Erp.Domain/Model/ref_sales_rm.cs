using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class ref_sales_rm
    {
        [Key]
        public int sales_rm_id { get; set; }
        public int employee_id { get; set; }
        [ForeignKey("employee_id")] 
        public virtual REF_EMPLOYEE REF_EMPLOYEE { get; set; }
        public bool is_blocked { get; set; }
        public bool is_active { get; set; }
    }

    public class Sales_RM_VM
    {
        public int sales_rm_id { get; set; }
        public int employee_id { get; set; }
        public string employee_code { get; set; }
        public string employee_name { get; set; }
        public bool is_blocked { get; set; }
        //public string emp_id { get; set; }
    }
}
