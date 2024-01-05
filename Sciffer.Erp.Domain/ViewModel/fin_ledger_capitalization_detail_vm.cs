using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
   public class fin_ledger_capitalization_detail_vm
    {
        public int fin_ledger_detail_id { get; set; }
        public int document_type_id { get; set; }
        public string document_type_name { get; set; }
        public string source_document_no { get; set; }
        public string ledger_date { get; set; }
        public double amount_local { get; set; }
        public double balance_local { get; set; }
        public long rowIndex { get; set; }
        public string emptyString { get; set; }
        public long rowIndex1 { get; set; }
        public string emptyString1 { get; set; }
        public string ref1 { get; set; }
        public string ref2 { get; set; }
        public string ref3 { get; set; }
        public string vendor_name { get; set; }
        public string po_no { get; set; }
        public int? po_id { get; set; }
        public int fin_ledger_capitalization_detail_id { get; set; }
        public int fin_ledger_capitalization_id { get; set; }   
        public decimal amount { get; set; }
        public string document_no { get; set; }
        public int sub_ledger_id { get; set; }
        public int gl_ledger_id { get; set; }

    }
}
