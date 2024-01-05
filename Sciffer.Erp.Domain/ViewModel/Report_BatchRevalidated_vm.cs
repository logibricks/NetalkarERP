using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_BatchRevalidated_vm
    {
        public string Doc_Number { get; set; }
        public DateTime Doc_Date { get; set; }
        public string Item_Code { get; set; }
        public string Item_Description { get; set; }
        public string PLANT { get; set; }
        public string BATCH { get; set; }
        public string sLOC { get; set; }
        public string Bucket { get; set; }
        public double Qty_in_STock { get; set; }
        public double Accepted_Qty { get; set; }
        public double Rejected_Qty { get; set; }
        public string oRIGINAL_sled { get; set; }
        public DateTime New_Expiry_Date { get; set; }

    }
}
