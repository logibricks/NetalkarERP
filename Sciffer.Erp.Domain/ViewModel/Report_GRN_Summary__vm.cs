using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_GRN_Summary__vm
    {
        public string vendor_Code { get; set; }
        public string Vendor_Description { get; set; }
        public string Parent_code { get; set; }
        public string parent_description { get; set; }
        public double Nett_value { get; set; }
    }
}
