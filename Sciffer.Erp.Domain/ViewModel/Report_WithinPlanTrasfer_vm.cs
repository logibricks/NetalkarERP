using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_WithinPlanTrasfer_vm
    {
        public string Doc_Category { get; set; }
        public string Doc_Number { get; set; }
        public DateTime Doc_Date { get; set; }
        public string header_Remarks { get; set; }
        public string Sending_SLOC { get; set; }
        public string Receiving_SLOC { get; set; }
        public string Sending_Bucket { get; set; }
        public string Receiving_Bucket { get; set; }
        public string Item_Code { get; set; }
        public string Item_Description { get; set; }
        public string batch { get; set; }
        public double Qty { get; set; }
        public string uoM { get; set; }

    }
}
