using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_BatchRevalidationCount_vm
    {
        public string Item_Code { get; set; }
        public string Item_Description { get; set; }
        public string Plant { get; set; }
        public string Batch { get; set; }
        public string Revalidation_Doc_Category { get; set; }
        public string Revalidation_Doc_Number { get; set; }
        public DateTime Revalidation_Doc_Date { get; set; }
        public string Count { get; set; }

    }
}
