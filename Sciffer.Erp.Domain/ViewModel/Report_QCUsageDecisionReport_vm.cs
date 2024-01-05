using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
   public class Report_QCUsageDecisionReport_vm: Report_QCLOTShelfLifeReport_vm
    {
        public string Sample_Size_Checked { get; set; }
        public string Sample_Size_Accepted { get; set; }
        public string Sample_Size_Rejected { get; set; }
        public double Total_Accepted_QTy { get; set; }
        public double Total_Rejected_Qty { get; set; }

    }
}
