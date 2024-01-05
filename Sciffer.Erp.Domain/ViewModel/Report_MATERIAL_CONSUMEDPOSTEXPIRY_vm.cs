using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_MATERIAL_CONSUMEDPOSTEXPIRY_vm
    {
        public string Item_Code { get; set; }
        public string Item_Description { get; set; }
        public string Plant { get; set; }
        public string Sloc { get; set; }
        public string Batch { get; set; }
        public string Bucket { get; set; }
        public string Shelf_Life_Expiry_Date { get; set; }
        public double Qty { get; set; }
        public string Source_Doc { get; set; }
        public string material_Doc_Number { get; set; }
        public string material_Doc_Date { get; set; }
        public string Reason_Code { get; set; }

    }
}
