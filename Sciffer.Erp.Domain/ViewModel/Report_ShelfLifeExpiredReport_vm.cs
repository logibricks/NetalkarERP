using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_ShelfLifeExpiredReport_vm
    {
        public string Item_Code { get; set; }
        public string Item_Description { get; set; }
        public string Plant { get; set; }
        public string Sloc { get; set; }
        public string Batch { get; set; }
        public string Bucket { get; set; }
        public DateTime Shelf_Life_Expiry_Date { get; set; }
        public DateTime Report_Date { get; set; }
        public string Delay { get; set; }

    }
}
