using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class Report_GeneralLedgerDetails_vm
    {
        public string document_category { get; set; }
        public string doc_number { get; set; }
        public DateTime? posting_date { get; set; }
        public DateTime? document_date { get; set; }
        public DateTime? due_date { get; set; }
        public string ref1 { get; set; }
        public string ref2 { get; set; }
        public string ref3 { get; set; }
        public string source_doc_type { get; set; }
        public string source_doc_no { get; set; }
        public string currency { get; set; }
        public string description { get; set; }
        public double? dr_lc { get; set; }
        public double? cr_lc { get; set; }
        public double? dr_fc { get; set; }
        public double? cr_fc { get; set; }
        public double? balance { get; set; }
        public string cost_center { get; set; }
        public string line_remarks { get; set; }

    }
}
