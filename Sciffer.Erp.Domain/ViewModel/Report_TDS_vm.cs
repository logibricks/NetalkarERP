using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_TDS_vm: Report_TDSPayable_vm
    {
        public string Customer_Name { get; set; }
        public string Customer_PAN { get; set; }
    }
}
