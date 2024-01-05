using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Sciffer.Erp.Domain.ViewModel
{
    public class ref_customer_balance_VM
    { 
        public int? customer_balance_id { get; set; }
        public int? offset_account_id { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime posting_date { get; set; }
        public string header_remarks { get; set; }
        public bool is_active { get; set; }
        public string deleteids { get; set; }
        public string gl_ledger_code { get; set; }
        public string gl_ledger_name { get; set; }
        public int category_id { get; set; }
        public string doc_number { get; set; }
        public virtual IList<ref_customer_balance_details> ref_customer_balance_details { get; set; }
        public virtual IList<customer_balance_detail> customer_balance_detail { get; set; }
        public List<string> customer_id { get; set; }
        public List<string> ref1 { get; set; }
        public List<string> ref2 { get; set; }
        public List<string> ref3 { get; set; }
        public List<DateTime> doc_date { get; set; }
        public List<DateTime> due_date { get; set; }
        public List<string> amount { get; set; }
        public List<string> amount_type_id { get; set; }
        public List<string> line_remarks { get; set; }
        public List<string> customer_balance_detail_id { get; set; }
    }
    public class customer_balance_detail
    {
        public string customer_code { get; set; }
        public string customer_name { get; set; }
        public string ref1 { get; set; }
        public string ref2 { get; set; }
        public string ref3 { get; set; }
        public string doc_date { get; set; }
        public string due_date { get; set; }
        public string amount { get; set; }
        public string amount_type { get; set; }
        public string line_remark { get; set; }
    }
    public class ref_customer_balanceVM
    {
        public int customer_balance_id { get; set; }
        public int? offset_account_id { get; set; }
        public string offset_account { get; set; }
        public string gl_ledger_code { get; set; }
        public string gl_ledger_name { get; set; }
        public DateTime posting_date { get; set; }
        public string header_remarks { get; set; }
        public int category_id { get; set; }
        public string doc_number { get; set; }
        public string category_name { get; set; }
        public int create_user { get; set; }
    }
    public class customer_balance_VM
    {
        public int? customer_balance_id { get; set; }
        public int offset_account_id { get; set; }
        public string offset_account { get; set; }
        public DateTime posting_date { get; set; }
        public string header_remarks { get; set; }
        public bool is_active { get; set; }
        public string doc_number { get; set; }
        public int category_id { get; set; }
    }
    public class customer_balance_detail_VM
    {
        public int? customer_balance_detail_id { get; set; }
        public int customer_balance_id { get; set; }
        public int customer_id { get; set; }
        public string ref1 { get; set; }
        public string ref2 { get; set; }
        public string ref3 { get; set; }
        public DateTime doc_date { get; set; }
        public double amount { get; set; }
        public int amount_type_id { get; set; }
        public string line_remark { get; set; }
        public DateTime due_date { get; set; }
        public string offset_account { get; set; }
        //public bool is_active { get; set; }
    }
}
