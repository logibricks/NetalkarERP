using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_GoodsIssue_vm
    {
        public string Doc_Category { get; set; }
        public string Doc_Number { get; set; }
        public DateTime Doc_Date { get; set; }
        public string header_Remarks { get; set; }
        public string Item_Code { get; set; }
        public string Item_Description { get; set; }
        public string Plant { get; set; }
        public string Sloc { get; set; }
        public string Bucket { get; set; }
        public string batch { get; set; }
        public string Reason { get; set; }
        public double Qty { get; set; }
        public string uoM { get; set; }
        public double rate { get; set; }
        public double value { get; set; }
        public string machine { get; set; }
        public string Cost_Center { get; set; }
        public string Order_No { get; set; }
        public string Gl_Account { get; set; }

    }
}
