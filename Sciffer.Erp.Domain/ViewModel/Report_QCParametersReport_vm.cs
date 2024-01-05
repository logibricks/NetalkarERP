using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_QCParametersReport_vm: Report_QCLOTShelfLifeReport_vm
    {
        public string Paramater_Name { get; set; }
        public string Parameter_Range { get; set; }
        public double Actual_Value { get; set; }
        public string Method_used { get; set; }
        public string Checked_by { get; set; }
        public string Document_Ref { get; set; }
        public string Pass_Fail {get;set;}

}
}
