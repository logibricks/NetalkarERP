using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_LedgerDetails_vm:Report_VendorLedgerDetails_vm
    {
        public double amount_local { get; set; }
        public double amount { get; set; }
    }
}
