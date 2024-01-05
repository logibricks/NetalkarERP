using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_TDSPayable_vm
    {
        public string TDS_Code { get; set; }
        public string TDS_Description { get; set; }
        public DateTime Posting_Date { get; set; }
        public string Form { get; set; }
        public string Doc_Category { get; set; }
        public string Doc_Number { get; set; }
        public string Supplier_Name { get; set; }
        public string Supplier_PAN { get; set; }
        public string Entity_Type { get; set; }
        public double Base_AMount { get; set; }
        public double TDS_AMount { get; set; }
    }
}
