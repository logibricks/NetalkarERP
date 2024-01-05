using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_BudgetActual_vm
    {
        public string Gl_Code { get; set; }
        public string Gl_Description { get; set; }
        public double Budget { get; set; }
        public string Actuals { get; set; }
        public string Difference { get; set; }
        public string Diffpercent { get; set; }
    }
}
