using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.ViewModel
{
   public class fin_credit_debit_transaction_vm
    {
        public string document_no { get; set; }
        public string category { get; set; }
        public string posting_date_st { get; set; }
        public string customer_name { get; set; }
        public string vendor_document_no { get; set; }
        public string vendor_document_date { get; set; }
        public double? taxable_value { get; set; }
        public string vendor_name { get; set; }
        public string module_form_code { get; set; }
        public int fin_credit_debit_note_transaction_id { get; set; }
        public int fin_credit_debit_note_id { get; set; }
        public string doc_type_code { get; set; }
        public int document_id { get; set; }
        public double? original_amount { get; set; }
        public double? dr_cr_amount { get; set; }
        public decimal? balance_net_value { get; set; }
        public decimal? balance_gross_value { get; set; }
        public double? net_value { get; set; }
        public double? gross_value { get; set; }
        public string ex_incl1 { get; set; }
        public string ex_incl { get; set; }
        public string exclusive_inclusive { get; set; }
    }
}
