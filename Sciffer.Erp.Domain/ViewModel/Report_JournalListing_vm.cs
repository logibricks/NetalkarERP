using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_JournalListing_vm
    {
        public string Category { get; set; }
        public string Doc_Number { get; set; }
        public DateTime Posting_Date { get; set; }
        public string GL_Code { get; set; }
        public string GL_Description { get; set; }
        public double Dr_Amount_LC { get; set; }
        public double Cr_Amount_LC { get; set; }
    }
}
