using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_QCLOTShelfLifeReport_vm
    {
        public string Doc_Category { get; set; }
        public string Doc_Number { get; set; }
        public DateTime Posting_Date { get; set; }
        public string Item_Code { get; set; }
        public string Item_Description { get; set; }
        public string Batch { get; set; }
        public double Qty { get; set; }
        public string Plant { get; set; }
        public string Base_DOcument { get; set; }
        public string Doc_Category1 { get; set; }
        public string Doc_Number1 { get; set; }
        public DateTime Posting_Date1 { get; set; }
        public string Item_Shelf_Life { get; set; }
        public string Shelf_Life_Base_Date_Category { get; set; }
        public DateTime Shelf_Life_Base_Date { get; set; }
        public string Shelf_Life { get; set; }

    }
}
