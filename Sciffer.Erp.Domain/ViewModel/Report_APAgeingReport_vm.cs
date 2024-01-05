using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_APAgeingReport_vm: Report_ARAGeingReport_vm
    {
        public string Vendor_Code { get; set; }
        public string vendor_Description { get; set; }
    }
}
