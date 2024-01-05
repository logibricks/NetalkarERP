using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class ref_vendor_balance_VM
    {
        public int? vendor_balance_id { get; set; }
        public int? offset_account_id { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime posting_date { get; set; }
        public string header_remark { get; set; }
        public bool is_active { get; set; }
        public string deleteids { get; set; }
        public string gl_ledger_code { get; set; }
        public string gl_ledger_name { get; set; }
        public int category_id { get; set; }
        public string doc_number { get; set; }
        public virtual IList<ref_vendor_balance_details> ref_vendor_balance_details { get; set; }
        public virtual IList<vendor_balance_detail> vendor_balance_detail { get; set; }
        public List<string> vendor_id { get; set; }
        public List<string> ref1 { get; set; }
        public List<string> ref2 { get; set; }
        public List<string> ref3 { get; set; }
        public List<DateTime> doc_date { get; set; }
        public List<DateTime> due_date { get; set; }
        public List<string> amount { get; set; }
        public List<string> amount_type_id { get; set; }
        public List<string> line_remarks { get; set; }
        public List<string> vendor_balance_detail_id { get; set; }
    }
    public class vendor_balance_detail
    {
        public string vendor { get; set; }
        public string ref1 { get; set; }
        public string ref2 { get; set; }
        public string ref3 { get; set; }
        public string doc_date { get; set; }
        public string due_date { get; set; }
        public string amount { get; set; }
        public string amount_type { get; set; }
        public string line_remark { get; set; }
    }
    public class ref_vendor_balanceVM
    {
        public int? vendor_balance_id { get; set; }
        public int? offset_account_id { get; set; }
        public DateTime posting_date { get; set; }
        public string header_remark { get; set; }
        public string gl_ledger_code { get; set; }
        public string gl_ledger_name { get; set; }
        public int category_id { get; set; }
        public string doc_number { get; set; }
        public string category_name { get; set; }
        public bool is_active { get; set; }
    }
    public class vendor_balance_VM
    {
        public int? vendor_balance_id { get; set; }
        public string offset_account { get; set; }
        public int offset_account_id { get; set; }
        public DateTime posting_date { get; set; }
        public string header_remark { get; set; }
        public int category_id { get; set; }
        public string doc_number { get; set; }
    }
    public class vendor_balance_detail_VM
    {
        public int? vendor_balance_detail_id { get; set; }
        public int? vendor_balance_id { get; set; }
        public int vendor_id { get; set; }
        public DateTime due_date { get; set; }
        public string ref1 { get; set; }
        public string ref2 { get; set; }
        public string ref3 { get; set; }
        public DateTime doc_date { get; set; }
        public double amount { get; set; }
        public int amount_type_id { get; set; }
        public string line_remarks { get; set; }
        public string offset_account { get; set; }
        public int? control_dawn_payment_account { get; set; }
        //public bool is_active { get; set; }
    }
}
